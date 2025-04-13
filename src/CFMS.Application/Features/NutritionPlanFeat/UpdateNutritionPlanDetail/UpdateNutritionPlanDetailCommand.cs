using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;

namespace CFMS.Application.Features.NutritionPlanFeat.UpdateNutritionPlanDetail
{
    public class UpdateNutritionPlanDetailCommand : IRequest<BaseResponse<bool>>
    {
        public UpdateNutritionPlanDetailCommand(Guid nutritionPlanDetailId, Guid? foodId, Guid? unitId, decimal? foodWeight)
        {
            NutritionPlanDetailId = nutritionPlanDetailId;
            FoodId = foodId;
            UnitId = unitId;
            FoodWeight = foodWeight;
        }

        public Guid NutritionPlanDetailId { get; set; }

        public Guid? FoodId { get; set; }

        public Guid? UnitId { get; set; }

        public decimal? FoodWeight { get; set; }
    }
}
