using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

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
            var existStage = _unitOfWork.GrowthStageRepository.Get(filter: s => s.GrowthStageId.Equals(request.StageId) && s.IsDeleted == false, includeProperties: [g => g.NutritionPlan]).FirstOrDefault();
            if (existStage == null)
            {
                return BaseResponse<GrowthStage>.SuccessResponse(message: "Giai đoạn phát triển không tồn tại");
            }
            return BaseResponse<GrowthStage>.SuccessResponse(data: existStage);
        }
    }
}
