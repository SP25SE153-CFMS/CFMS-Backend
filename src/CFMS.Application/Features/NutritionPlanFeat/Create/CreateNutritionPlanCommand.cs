using CFMS.Application.Common;
using CFMS.Application.DTOs.NutritionPlan;
using MediatR;

namespace CFMS.Application.Features.NutritionPlanFeat.Create
{
    public class CreateNutritionPlanCommand : IRequest<BaseResponse<bool>>
    {
        public CreateNutritionPlanCommand(string? name, string? description, List<Guid>? chickenList, List<NutritionPlanDetailDto> nutritionPlanDetails)
        {
            Name = name;
            Description = description;
            ChickenList = chickenList;
            NutritionPlanDetails = nutritionPlanDetails;
        }

        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<Guid>? ChickenList { get; set; }
        public List<NutritionPlanDetailDto>? NutritionPlanDetails { get; set; }
    }
}
