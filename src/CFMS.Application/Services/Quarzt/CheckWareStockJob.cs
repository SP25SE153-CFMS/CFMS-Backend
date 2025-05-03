using CFMS.Application.Services.SignalR;
using CFMS.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Quartz;
using StackExchange.Redis;

namespace CFMS.Application.Services.Quarzt
{
    public class CheckWareStockJob : IJob
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CheckWareStockJob> _logger;
        private readonly NotiHub _hubContext;
        private readonly ICurrentUserService _currentUserService;

        public CheckWareStockJob(IUnitOfWork unitOfWork, ILogger<CheckWareStockJob> logger, NotiHub hubContext, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _hubContext = hubContext;
            _currentUserService = currentUserService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Running job: {JobName}", nameof(CheckWareStockJob));

            try
            {
                var systemId = _unitOfWork.UserRepository.Get(filter: u => u.SystemRole == -1).FirstOrDefault()?.UserId;
                _currentUserService.SetSystemId(systemId.Value);

                var today = DateTime.Now.Date;
                var thresholdMin = _unitOfWork.SystemConfigRepository.Get(filter: s => s.SettingName.Equals("ThresholdMinWareHouse") && !s.IsDeleted && s.EffectedDateTo > DateTime.Now.ToLocalTime()).FirstOrDefault(); // số lượng dưới mức này là cảnh báo sắp hết
                var thresholdMax = _unitOfWork.SystemConfigRepository.Get(filter: s => s.SettingName.Equals("ThresholdMaxWareHouse") && !s.IsDeleted && s.EffectedDateTo > DateTime.Now.ToLocalTime()).FirstOrDefault(); // số lượng dưới mức này là cảnh báo sắp hết

                var farms = _unitOfWork.FarmRepository.Get(
                    filter: f => !f.IsDeleted,
                    includeProperties: "FarmEmployees,FarmEmployees.User,Warehouses,Warehouses.WareStocks"
                    );

                //var wares = _unitOfWork.WarehouseRepository.Get(
                //    filter: w => w.IsDeleted == false,
                //    includeProperties: "WareStocks"
                //    );

                foreach (var farm in farms)
                {
                    var recipients = farm.FarmEmployees
                    .Where(fe => fe.User.Status == 1
                              && !fe.IsDeleted
                              && (fe.FarmRole == 4 || fe.FarmRole == 5))
                    .Select(fe => fe.User);

                    foreach (var ware in farm.Warehouses)
                    {
                        var totalStock = ware.WareStocks.Select(s => s.Quantity).Sum();
                        var maxCapacity = ware.MaxQuantity;

                        if (maxCapacity > 0)
                        {
                            if (totalStock >= maxCapacity * thresholdMax.SettingValue)
                            {
                                foreach (var recipient in recipients)
                                {
                                    var notiSend = new Domain.Entities.Notification
                                    {
                                        UserId = recipient!.UserId,
                                        NotificationName = "Cảnh báo kho sắp đầy",
                                        NotificationType = "WARESTOCK_WARNING",
                                        Content = $"Kho {ware.WarehouseName} gần đầy ({totalStock}/{maxCapacity})",
                                        IsRead = 0,
                                    };

                                    _unitOfWork.NotificationRepository.Insert(notiSend);
                                    await _hubContext.SendMessageToUser(recipient!.UserId.ToString(), notiSend);
                                }

                            }

                            if (totalStock <= maxCapacity * thresholdMin.SettingValue)
                            {
                                foreach (var recipient in recipients)
                                {
                                    var notiSend = new Domain.Entities.Notification
                                    {
                                        UserId = recipient!.UserId,
                                        NotificationName = "Cảnh báo kho sắp hết",
                                        NotificationType = "WARESTOCK_WARNING",
                                        Content = $"Kho {ware.WarehouseName} sắp hết ({totalStock}/{maxCapacity})",
                                        IsRead = 0,
                                    };

                                    _unitOfWork.NotificationRepository.Insert(notiSend);
                                    await _hubContext.SendMessageToUser(recipient!.UserId.ToString(), notiSend);
                                }
                            }
                        }
                    }
                }

                await _unitOfWork.SaveChangesAsync();
                _logger.LogInformation("Finished job: {JobName}", nameof(CheckWareStockJob));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while executing job: {JobName}", nameof(CheckWareStockJob));
            }
        }
    }
}
