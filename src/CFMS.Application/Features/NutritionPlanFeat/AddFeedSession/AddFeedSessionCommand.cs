using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.NutritionPlanFeat.AddFeedSession
{
    public class AddFeedSessionCommand : IRequest<BaseResponse<bool>>
    {
        public AddFeedSessionCommand(Guid? nutritionPlanId, DateTime? feedingTime, decimal? feedAmount, Guid? unitId, string? note)
        {
            NutritionPlanId = nutritionPlanId;
            FeedingTime = feedingTime;
            FeedAmount = feedAmount;
            UnitId = unitId;
            Note = note;
        }

        public Guid? NutritionPlanId { get; set; }

        public DateTime? FeedingTime { get; set; }

        public decimal? FeedAmount { get; set; }

        public Guid? UnitId { get; set; }

        public string? Note { get; set; }
    }
}
