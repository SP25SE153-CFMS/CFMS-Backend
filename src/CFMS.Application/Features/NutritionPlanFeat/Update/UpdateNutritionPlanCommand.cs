using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.NutritionPlanFeat.Update
{
    public class UpdateNutritionPlanCommand : IRequest<BaseResponse<bool>>
    {
        public UpdateNutritionPlanCommand(Guid id, string? name, string? description, string? target)
        {
            Id = id;
            Name = name;
            Description = description;
            Target = target;
        }

        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? Target { get; set; }
    }
}
