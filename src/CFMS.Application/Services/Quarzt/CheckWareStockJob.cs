using CFMS.Application.Services.SignalR;
using CFMS.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Quartz;

namespace CFMS.Application.Services.Quarzt
{
    public class CheckWareStockJob : IJob
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CheckWareStockJob> _logger;
        private readonly NotiHub _hubContext;

        public CheckWareStockJob(IUnitOfWork unitOfWork, ILogger<CheckWareStockJob> logger, NotiHub hubContext)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _hubContext = hubContext;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Running job: {JobName}", nameof(CheckWareStockJob));

            try
            {
                var today = DateTime.Now.Date;
                var thresholdMin = _unitOfWork.SystemConfigRepository.Get(filter: s => s.SettingName.Equals("ThresholdMinWareHouse") && !s.IsDeleted && s.EffectedDateTo > DateTime.Now.ToLocalTime().AddHours(7)).FirstOrDefault(); // số lượng dưới mức này là cảnh báo sắp hết
                var thresholdMax = _unitOfWork.SystemConfigRepository.Get(filter: s => s.SettingName.Equals("ThresholdMaxWareHouse") && !s.IsDeleted && s.EffectedDateTo > DateTime.Now.ToLocalTime().AddHours(7)).FirstOrDefault(); // số lượng dưới mức này là cảnh báo sắp hết

                var wares = _unitOfWork.WarehouseRepository.Get(
                    filter: w => w.IsDeleted == false,
                    includeProperties: "WareStocks"
                    );

                foreach (var ware in wares)
                {
                    var totalStock = ware.WareStocks.Select(s => s.Quantity).Sum();
                    var maxCapacity = ware.MaxQuantity;

                    if (maxCapacity > 0)
                    {
                        if (totalStock >= maxCapacity * thresholdMax.SettingValue)
                        {
                            await _hubContext.SendMessage($"⚠️ Kho {ware.WarehouseName} gần đầy ({totalStock}/{maxCapacity})");
                        }

                        if (totalStock <= maxCapacity * thresholdMin.SettingValue)
                        {
                            await _hubContext.SendMessage($"⚠️ Kho {ware.WarehouseName} sắp hết ({totalStock}/{maxCapacity})");
                        }
                    }
                }

                _logger.LogInformation("Finished job: {JobName}", nameof(CheckWareStockJob));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while executing job: {JobName}", nameof(CheckWareStockJob));
            }
        }
    }
}
