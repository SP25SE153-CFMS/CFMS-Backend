using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.GrowthStageFeat.DeleteNutritionPlan
{
    public class DeleteGrowthStageNutritionPlanCommand : IRequest<BaseResponse<bool>>
    {
        public DeleteGrowthStageNutritionPlanCommand(Guid nutritionPlanId, Guid growthStageId)
        {
            NutritionPlanId = nutritionPlanId;
            GrowthStageId = growthStageId;
        }

        public Guid NutritionPlanId { get; set; }

        public Guid GrowthStageId { get; set; }
    }
}
