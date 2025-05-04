using CFMS.Application.Services.SignalR;
using CFMS.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Quartz;

namespace CFMS.Application.Services.Quarzt
{
    public class CheckMaintenanceScheduleJob : IJob
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CheckMaintenanceScheduleJob> _logger;
        private readonly NotiHub _hubContext;
        private readonly ICurrentUserService _currentUserService;

        public CheckMaintenanceScheduleJob(IUnitOfWork unitOfWork, ILogger<CheckMaintenanceScheduleJob> logger, NotiHub hubContext, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _hubContext = hubContext;
            _currentUserService = currentUserService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Running job: {JobName}", nameof(CheckMaintenanceScheduleJob));

            try
            {
                var systemId = _unitOfWork.UserRepository.Get(filter: u => u.SystemRole == -1).FirstOrDefault()?.UserId;
                _currentUserService.SetSystemId(systemId.Value);

                var threshold = _unitOfWork.SystemConfigRepository.Get(filter: s => s.SettingName.Equals("MaintenanceThreshold") && !s.IsDeleted && s.EffectedDateTo > DateTime.Now.ToLocalTime()).FirstOrDefault(); // s

                var farms = _unitOfWork.FarmRepository.Get(
                    filter: f => !f.IsDeleted,
                    includeProperties: "FarmEmployees.User,BreedingAreas.ChickenCoops.CoopEquipments"
                    );

                foreach (var farm in farms)
                {
                    var recipients = farm.FarmEmployees
                    .Where(fe => fe.User.Status == 1
                              && !fe.IsDeleted
                              && (fe.FarmRole == 4 || fe.FarmRole == 5))
                    .Select(fe => fe.User);

                    foreach (var breedingArea in farm.BreedingAreas)
                    {
                        foreach (var coop in breedingArea.ChickenCoops)
                        {
                            foreach (var coopEquipment in coop.CoopEquipments)
                            {
                                var equipmentWarnings = new List<string>();
                                var nextDate = coopEquipment.NextMaintenanceDate;

                                if (nextDate.HasValue && nextDate.Value <= DateTime.Today.AddDays((double)threshold.SettingValue)) // sắp bảo trì trong 3 ngày
                                {
                                    var equipmentName = coopEquipment.Equipment?.EquipmentName ?? "Thiết bị không rõ";
                                    var formattedDate = nextDate.Value.ToString("dd/MM/yyyy");

                                    equipmentWarnings.Add($"- {equipmentName}: {formattedDate}");
                                }

                                if (equipmentWarnings.Count > 0)
                                {
                                    var coopName = coop.ChickenCoopName ?? "Chuồng không rõ";
                                    var areaName = breedingArea.BreedingAreaName ?? "Khu nuôi không rõ";

                                    var message = $"Các thiết bị sau trong chuồng **{coopName}** (khu **{areaName}**) sắp đến hạn bảo trì:\n{string.Join("\n", equipmentWarnings)}";

                                    foreach (var recipient in recipients)
                                    {
                                        if (recipient.FarmEmployees.Any(r => r.FarmId.Equals(farm.FarmId)))
                                        {
                                            var notiSend = new Domain.Entities.Notification
                                            {
                                                UserId = recipient!.UserId,
                                                NotificationName = "Sắp đến hạn bảo trì trang thiết bị",
                                                NotificationType = "MAINTENANCE_WARNING",
                                                Content = message,
                                                IsRead = 0,
                                            };

                                            _unitOfWork.NotificationRepository.Insert(notiSend);
                                            await _hubContext.SendMessageToUser(recipient.UserId.ToString(), message);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                await _unitOfWork.SaveChangesAsync();
                _logger.LogInformation("Finished job: {JobName}", nameof(CheckMaintenanceScheduleJob));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while executing job: {JobName}", nameof(CheckMaintenanceScheduleJob));
            }
        }
    }
}
