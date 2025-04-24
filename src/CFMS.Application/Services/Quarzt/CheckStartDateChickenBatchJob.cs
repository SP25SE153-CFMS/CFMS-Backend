using CFMS.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Quartz;

namespace CFMS.Application.Services.Quartz
{
    public class CheckStartDateChickenBatchJob : IJob
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CheckStartDateChickenBatchJob> _logger;

        public CheckStartDateChickenBatchJob(
            ILogger<CheckStartDateChickenBatchJob> logger,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Running job: {JobName}", nameof(CheckStartDateChickenBatchJob));

            try
            {
                var today = DateTime.Now.ToLocalTime().Date;

                var chickenBatches = _unitOfWork.ChickenBatchRepository.Get(
                    filter: cb => cb.IsDeleted == false
                               && cb.StartDate != null
                               && cb.StartDate.Value.Date <= today
                               && cb.Status != 1
                );

                foreach (var batch in chickenBatches)
                {
                    batch.Status = 1;
                    _unitOfWork.ChickenBatchRepository.UpdateWithoutDetach(batch);
                }

                _unitOfWork.Save();

                _logger.LogInformation("Finished job: {JobName}", nameof(CheckStartDateChickenBatchJob));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while executing job: {JobName}", nameof(CheckStartDateChickenBatchJob));
            }
        }
    }
}
