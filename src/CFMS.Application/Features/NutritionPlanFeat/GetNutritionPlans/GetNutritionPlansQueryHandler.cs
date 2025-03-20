using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.NutritionPlanFeat.GetNutritionPlans
{
    public class GetNutritionPlansQueryHandler : IRequestHandler<GetNutritionPlansQuery, BaseResponse<IEnumerable<NutritionPlan>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetNutritionPlansQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<IEnumerable<NutritionPlan>>> Handle(GetNutritionPlansQuery request, CancellationToken cancellationToken)
        {
            var plans = _unitOfWork.NutritionPlanRepository.Get(filter: p => p.IsDeleted == false);
            return BaseResponse<IEnumerable<NutritionPlan>>.SuccessResponse(data: plans);
        }
    }
}
