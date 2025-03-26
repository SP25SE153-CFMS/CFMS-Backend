using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.NutritionPlanFeat.DeleteFeedSession
{
    public class DeleteFeedSessionCommand : IRequest<BaseResponse<bool>>
    {
        public DeleteFeedSessionCommand(Guid feedSessionId, Guid nutritionPlanId)
        {
            FeedSessionId = feedSessionId;
            NutritionPlanId = nutritionPlanId;
        }

        public Guid NutritionPlanId { get; set; }

        public Guid FeedSessionId { get; set; }
    }
}
