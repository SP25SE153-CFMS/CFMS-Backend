using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;

namespace CFMS.Application.Features.NutritionPlanFeat.GetNutritionPlans
{
    public class GetNutritionPlansQuery : IRequest<BaseResponse<IEnumerable<NutritionPlan>>>
    {
    }
}
