using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.BreedingAreaFeat.GetBreedingAreas
{
    public class GetBreedingAreasQueryHandler : IRequestHandler<GetBreedingAreasQuery, BaseResponse<IEnumerable<BreedingArea>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetBreedingAreasQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<IEnumerable<BreedingArea>>> Handle(GetBreedingAreasQuery request, CancellationToken cancellationToken)
        {
            var existFarm = _unitOfWork.FarmRepository.GetByID(request.FarmId);
            if (existFarm == null)
            {
                return BaseResponse<IEnumerable<BreedingArea>>.FailureResponse(message: "Trang trại không tồn tại");
            }

            var breedingAreas = _unitOfWork.BreedingAreaRepository.Get(filter: ba => ba.IsDeleted == false && ba.FarmId.Equals(request.FarmId), includeProperties: "ChickenCoops");
            return BaseResponse<IEnumerable<BreedingArea>>.SuccessResponse(data: breedingAreas);
        }   
    }
}
