using CFMS.Application.Common;
using CFMS.Application.Features.NutritionPlanFeat.GetNutritionPlans;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.NutritionPlanFeat.GetNutritionPlanByFarmId
{
    public class GetNutritionPlanByFarmIdQueryHandler : IRequestHandler<GetNutritionPlanByFarmIdQuery, BaseResponse<IEnumerable<NutritionPlan>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetNutritionPlanByFarmIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<IEnumerable<NutritionPlan>>> Handle(GetNutritionPlanByFarmIdQuery request, CancellationToken cancellationToken)
        {
            var plans = _unitOfWork.NutritionPlanRepository.Get(filter: p => p.FarmId.Equals(request.FarmId) && p.IsDeleted == false, includeProperties: "FeedSessions,NutritionPlanDetails,NutritionPlanDetails.Food");
            return BaseResponse<IEnumerable<NutritionPlan>>.SuccessResponse(data: plans);
        }
    }
}
