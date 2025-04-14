namespace CFMS.Application.DTOs.NutritionPlan
{
    public class NutritionPlanDetailUpdateDto
    {
        public Guid NutritionPlanDetailId { get; set; }

        public Guid? FoodId { get; set; }

        public Guid? UnitId { get; set; }

        public decimal? FoodWeight { get; set; }
    }
}
