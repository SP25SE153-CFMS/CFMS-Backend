using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.NutritionPlanFeat.Create
{
    public class CreateNutritionPlanCommand : IRequest<BaseResponse<bool>>
    {
        public CreateNutritionPlanCommand(string? name, string? description, string? target)
        {
            Name = name;
            Description = description;
            Target = target;
        }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? Target { get; set; }
    }
}
