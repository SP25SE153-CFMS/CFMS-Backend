using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.ChickenBatchFeat.UpdateGrowthBatch
{
    public class UpdateGrowthBatchCommand : IRequest<BaseResponse<bool>>
    {
        public UpdateGrowthBatchCommand(Guid growthBatchId, DateTime? startDate, DateTime? endDate, decimal? avgWeight, decimal? mortalityRate, decimal? feedConsumption, string? note, bool? status)
        {
            GrowthBatchId = growthBatchId;
            StartDate = startDate;
            EndDate = endDate;
            AvgWeight = avgWeight;
            MortalityRate = mortalityRate;
            FeedConsumption = feedConsumption;
            Note = note;
            Status = status;
        }

        public Guid GrowthBatchId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public decimal? AvgWeight { get; set; }

        public decimal? MortalityRate { get; set; }

        public decimal? FeedConsumption { get; set; }

        public string? Note { get; set; }

        public bool? Status { get; set; }
    }
}
