using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.NutritionPlanFeat.AddFood
{
    public class AddFoodCommand : IRequest<BaseResponse<bool>>
    {
        public AddFoodCommand(Guid foodId, Guid? nutritionPlanId, decimal? foodWeight, Guid unitId)
        {
            FoodId = foodId;
            NutritionPlanId = nutritionPlanId;
            FoodWeight = foodWeight;
            UnitId = unitId;
        }

        public Guid FoodId { get; set; }

        public Guid UnitId { get; set; }

        public Guid? NutritionPlanId { get; set; }

        public decimal? FoodWeight { get; set; }
    }
}
