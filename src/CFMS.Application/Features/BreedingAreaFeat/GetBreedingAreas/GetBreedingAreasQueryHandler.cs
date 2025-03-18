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
            var breedingAreas = _unitOfWork.BreedingAreaRepository.Get();
            return BaseResponse<IEnumerable<BreedingArea>>.SuccessResponse(data: breedingAreas);
        }
    }
}
