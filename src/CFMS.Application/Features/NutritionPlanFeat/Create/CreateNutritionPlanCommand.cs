using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;

namespace CFMS.Application.Features.NutritionPlanFeat.Create
{
    public class CreateNutritionPlanCommand : IRequest<BaseResponse<bool>>
    {
        public CreateNutritionPlanCommand(string? name, string? description, List<string>? chickenList)
        {
            Name = name;
            Description = description;
            ChickenList = chickenList;
        }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public List<string>? ChickenList { get; set; }
    }
}
