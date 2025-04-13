using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.GrowthStageFeat.GetStagesByFarmId
{
    public class GetStagesByFarmIdQueryHandler : IRequestHandler<GetStagesByFarmIdQuery, BaseResponse<IEnumerable<GrowthStage>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetStagesByFarmIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<IEnumerable<GrowthStage>>> Handle(GetStagesByFarmIdQuery request, CancellationToken cancellationToken)
        {
            var stages = _unitOfWork.GrowthStageRepository.Get(filter: s => s.IsDeleted == false && s.FarmId.Equals(request.FarmId), includeProperties: [g => g.NutritionPlan]);
            return BaseResponse<IEnumerable<GrowthStage>>.SuccessResponse(data: stages);
        }
    }
}
