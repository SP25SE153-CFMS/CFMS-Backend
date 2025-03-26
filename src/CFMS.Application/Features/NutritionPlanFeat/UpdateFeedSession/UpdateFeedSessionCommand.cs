using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.NutritionPlanFeat.UpdateFeedSession
{
    public class UpdateFeedSessionCommand : IRequest<BaseResponse<bool>>
    {
        public UpdateFeedSessionCommand(Guid feedSessionId, DateTime? feedingTime, decimal? feedAmount, Guid? unitId, string? note)
        {
            FeedSessionId = feedSessionId;
            FeedingTime = feedingTime;
            FeedAmount = feedAmount;
            UnitId = unitId;
            Note = note;
        }

        public Guid FeedSessionId { get; set; }

        public DateTime? FeedingTime { get; set; }

        public decimal? FeedAmount { get; set; }

        public Guid? UnitId { get; set; }

        public string? Note { get; set; }
    }
}
