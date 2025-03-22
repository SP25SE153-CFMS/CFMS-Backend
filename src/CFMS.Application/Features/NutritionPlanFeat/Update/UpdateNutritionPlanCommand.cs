using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;

namespace CFMS.Application.Features.NutritionPlanFeat.Update
{
    public class UpdateNutritionPlanCommand : IRequest<BaseResponse<bool>>
    {
        public UpdateNutritionPlanCommand(Guid id, string? name, string? description, List<string> chickensList)
        {
            Id = id;
            Name = name;
            Description = description;
            ChickensList = chickensList;
        }

        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public List<string> ChickensList { get; set; }
    }
}
