using CFMS.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Quartz;

namespace CFMS.Application.Services.Quartz
{
    public class UpdateChickenbatchCurrentStageJob : IJob
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateChickenbatchCurrentStageJob> _logger;
        private readonly ICurrentUserService _currentUserService;

        public UpdateChickenbatchCurrentStageJob(
            IUnitOfWork unitOfWork,
            ILogger<UpdateChickenbatchCurrentStageJob> logger,
            ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Running job: {JobName}", nameof(UpdateChickenbatchCurrentStageJob));

            try
            {
                var systemId = _unitOfWork.UserRepository.Get(filter: u => u.SystemRole == -1).FirstOrDefault()?.UserId;
                _currentUserService.SetSystemId(systemId.Value);

                var today = DateTime.UtcNow.ToLocalTime().AddHours(7).Date;

                var chickenBatches = _unitOfWork.ChickenBatchRepository.Get(
                    filter: cb => !cb.IsDeleted && cb.StartDate != null,
                    includeProperties: "CurrentStage,GrowthBatches,GrowthBatches.GrowthStage"
                );

                foreach (var batch in chickenBatches)
                {
                    var weekAge = (today - batch.StartDate!.Value.Date).Days / 7;

                    var targetGrowthBatch = batch.GrowthBatches
                        .FirstOrDefault(gb =>
                            gb.GrowthStage != null &&
                            gb.GrowthStage.MinAgeWeek <= weekAge &&
                            gb.GrowthStage.MaxAgeWeek >= weekAge
                        );

                    if (targetGrowthBatch != null &&
                        batch.CurrentStageId != targetGrowthBatch.GrowthStageId)
                    {
                        batch.CurrentStageId = targetGrowthBatch.GrowthStageId;
                        _unitOfWork.ChickenBatchRepository.UpdateWithoutDetach(batch);
                    }
                }

                await _unitOfWork.SaveChangesAsync();
                //_unitOfWork.Save();

                _logger.LogInformation("Finished job: {JobName}", nameof(UpdateChickenbatchCurrentStageJob));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred in job: {JobName}", nameof(UpdateChickenbatchCurrentStageJob));
            }
        }
    }
}
