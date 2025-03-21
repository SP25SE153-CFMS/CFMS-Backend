using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.ChickenBatchFeat.AddGrowthStage
{
    public class AddGrowthStageCommand : IRequest<BaseResponse<bool>>
    {
        public AddGrowthStageCommand(Guid? chickenBatchId, Guid? growthStageId, DateTime? startDate, DateTime? endDate, decimal? avgWeight, decimal? mortalityRate, decimal? feedConsumption, string? note)
        {
            ChickenBatchId = chickenBatchId;
            GrowthStageId = growthStageId;
            StartDate = startDate;
            EndDate = endDate;
            AvgWeight = avgWeight;
            MortalityRate = mortalityRate;
            FeedConsumption = feedConsumption;
            Note = note;
        }

        public Guid? ChickenBatchId { get; set; }

        public Guid? GrowthStageId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public decimal? AvgWeight { get; set; }

        public decimal? MortalityRate { get; set; }

        public decimal? FeedConsumption { get; set; }

        public string? Note { get; set; }
    }
}
