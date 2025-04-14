using CFMS.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Quartz;

namespace CFMS.Application.Services.Quarzt
{
    public class UpdateChickenbatchCurrentStageJob : IJob
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateChickenbatchCurrentStageJob> _logger;

        public UpdateChickenbatchCurrentStageJob(IUnitOfWork unitOfWork, ILogger<UpdateChickenbatchCurrentStageJob> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                _logger.LogInformation(message: "Running job UpdateChickenbatchCurrentStageJob...");
                var today = DateTime.Now.ToLocalTime().Date;

                var chickenBatches = _unitOfWork.ChickenBatchRepository.Get(
                    filter: cb => cb.IsDeleted == false,
                    includeProperties: "CurrentStage,GrowthBatches,GrowthBatches.GrowthStage"
                );

                foreach (var chickenBatch in chickenBatches)
                {
                    if (chickenBatch.StartDate == null) continue;

                    var daysSinceStart = (today - chickenBatch.StartDate.Value.Date).Days;
                    var weekAge = daysSinceStart / 7;

                    var matchedGrowthBatch = chickenBatch.GrowthBatches
                        .FirstOrDefault(gb =>
                            gb.GrowthStage != null &&
                            gb.GrowthStage.MinAgeWeek <= weekAge &&
                            gb.GrowthStage.MaxAgeWeek >= weekAge
                        );

                    if (matchedGrowthBatch != null && chickenBatch.CurrentStageId != matchedGrowthBatch.GrowthStageId)
                    {
                        chickenBatch.CurrentStageId = matchedGrowthBatch.GrowthStageId;
                        _unitOfWork.ChickenBatchRepository.Update(chickenBatch);
                    }
                }

                await _unitOfWork.SaveChangesAsync();
                _logger.LogInformation(message: "Finished job UpdateChickenbatchCurrentStageJob...");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
            }
        }
    }
}
