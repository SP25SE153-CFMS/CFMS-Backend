using CFMS.Application.Common;
using CFMS.Application.DTOs.NutritionPlan;
using CFMS.Domain.Entities;
using MediatR;

namespace CFMS.Application.Features.NutritionPlanFeat.Update
{
    public class UpdateNutritionPlanCommand : IRequest<BaseResponse<bool>>
    {
        public UpdateNutritionPlanCommand(string? name, string? description, List<Guid>? chickenList, List<NutritionPlanDetailDto> nutritionPlanDetails, Guid nutritionPlanId)
        {

            Name = name;
            Description = description;
            ChickenList = chickenList;
            NutritionPlanDetails = nutritionPlanDetails;
            NutritionPlanId = nutritionPlanId;
        }

        public Guid NutritionPlanId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<Guid>? ChickenList { get; set; }
        public List<NutritionPlanDetailDto>? NutritionPlanDetails { get; set; }
    }
}
