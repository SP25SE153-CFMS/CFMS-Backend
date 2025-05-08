using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;

namespace CFMS.Application.Features.NutritionPlanFeat.GetNutritionPlanByFarmId
{
    public class GetNutritionPlanByFarmIdQuery : IRequest<BaseResponse<IEnumerable<NutritionPlan>>>
    {
        public GetNutritionPlanByFarmIdQuery(Guid farmId)
        {
            FarmId = farmId;
        }

        public Guid FarmId { get; set; }
    }
}
