using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.GrowthStageFeat.GetStages
{
    public class GetStagesQueryHandler : IRequestHandler<GetStagesQuery, BaseResponse<IEnumerable<GrowthStage>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetStagesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<IEnumerable<GrowthStage>>> Handle(GetStagesQuery request, CancellationToken cancellationToken)
        {
            var stages = _unitOfWork.GrowthStageRepository.Get(filter: s => s.IsDeleted == false && s.FarmId.Equals(request.FarmId), includeProperties: [g => g.NutritionPlan]);
            return BaseResponse<IEnumerable<GrowthStage>>.SuccessResponse(data: stages);
        }
    }
}
