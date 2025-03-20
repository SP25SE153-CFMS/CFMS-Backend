using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.NutritionPlanFeat.Delete
{
    public class DeleteNutritionPlanCommand : IRequest<BaseResponse<bool>>
    {
        public DeleteNutritionPlanCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
