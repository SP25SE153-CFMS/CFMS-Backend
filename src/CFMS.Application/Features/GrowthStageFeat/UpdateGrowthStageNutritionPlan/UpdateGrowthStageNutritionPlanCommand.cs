using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.GrowthStageFeat.UpdateNutritionPlan
{
    public class UpdateGrowthStageNutritionPlanCommand : IRequest<BaseResponse<bool>>
    {
        public UpdateGrowthStageNutritionPlanCommand(Guid growthStageId, Guid nutritionPlanId)
        {
            GrowthStageId = growthStageId;
            NutritionPlanId = nutritionPlanId;
        }

        public Guid GrowthStageId { get; set; }

        public Guid NutritionPlanId { get; set; }
    }
}
