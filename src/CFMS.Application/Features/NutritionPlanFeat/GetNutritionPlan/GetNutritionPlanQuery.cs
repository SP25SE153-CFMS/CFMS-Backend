using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;

namespace CFMS.Application.Features.NutritionPlanFeat.GetNutritionPlan
{
    public class GetNutritionPlanQuery : IRequest<BaseResponse<NutritionPlan>>
    {
        public GetNutritionPlanQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
