using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.NutritionPlanFeat.AddFeedSession
{
    public class AddFeedSessionCommand : IRequest<BaseResponse<bool>>
    {
        public AddFeedSessionCommand(Guid? nutritionPlanId, TimeOnly? startTime, TimeOnly? endTime, decimal? feedAmount, Guid? unitId, string? note)
        {
            NutritionPlanId = nutritionPlanId;
            StartTime = startTime;
            EndTime = endTime;
            FeedAmount = feedAmount;
            UnitId = unitId;
            Note = note;
        }

        public Guid? NutritionPlanId { get; set; }

        public TimeOnly? StartTime { get; set; }

        public TimeOnly? EndTime { get; set; }

        public decimal? FeedAmount { get; set; }

        public Guid? UnitId { get; set; }

        public string? Note { get; set; }
    }
}
