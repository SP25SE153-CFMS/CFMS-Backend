using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CFMS.Application.Features.GrowthStageFeat.GetStage
{
    public class GetStageQueryHandler : IRequestHandler<GetStageQuery, BaseResponse<GrowthStage>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetStageQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<GrowthStage>> Handle(GetStageQuery request, CancellationToken cancellationToken)
        {
            var existStage = _unitOfWork.GrowthStageRepository.GetIncludeMultiLayer(filter: s => s.GrowthStageId.Equals(request.StageId) && s.IsDeleted == false,
                include: x => x
                .Include(t => t.NutritionPlan)
                    .ThenInclude(t => t.NutritionPlanDetails)
                ).FirstOrDefault();
            if (existStage == null)
            {
                return BaseResponse<GrowthStage>.FailureResponse(message: "Giai đoạn phát triển không tồn tại");
            }
            return BaseResponse<GrowthStage>.SuccessResponse(data: existStage);
        }
    }
}
