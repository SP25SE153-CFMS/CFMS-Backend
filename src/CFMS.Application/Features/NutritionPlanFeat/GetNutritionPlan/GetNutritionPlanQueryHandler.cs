using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.NutritionPlanFeat.GetNutritionPlan
{
    public class GetNutritionPlanQueryHandler : IRequestHandler<GetNutritionPlanQuery, BaseResponse<NutritionPlan>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetNutritionPlanQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<NutritionPlan>> Handle(GetNutritionPlanQuery request, CancellationToken cancellationToken)
        {
            var existPlan = _unitOfWork.NutritionPlanRepository.Get(filter: p => p.NutritionPlanId.Equals(request.Id) && p.IsDeleted == false).FirstOrDefault();
            if (existPlan == null)
            {
                return BaseResponse<NutritionPlan>.FailureResponse(message: "Chế độ dinh dưỡng không tồn tại");
            }
            return BaseResponse<NutritionPlan>.SuccessResponse(data: existPlan);
        }
    }
}
