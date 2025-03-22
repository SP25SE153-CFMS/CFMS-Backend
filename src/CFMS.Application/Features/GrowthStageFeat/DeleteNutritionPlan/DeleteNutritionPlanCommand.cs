using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.GrowthStageFeat.DeleteNutritionPlan
{
    public class DeleteNutritionPlanCommand : IRequest<BaseResponse<bool>>
    {
        public DeleteNutritionPlanCommand(Guid growthNutritionId, Guid growthStageId)
        {
            GrowthNutritionId = growthNutritionId;
            GrowthStageId = growthStageId;
        }

        public Guid GrowthNutritionId { get; set; }

        public Guid GrowthStageId { get; set; }
    }
}
