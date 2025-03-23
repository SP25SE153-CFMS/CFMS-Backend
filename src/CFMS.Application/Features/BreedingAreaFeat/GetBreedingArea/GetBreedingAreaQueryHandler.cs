using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.BreedingAreaFeat.GetBreedingArea
{
    public class GetBreedingAreaQueryHandler : IRequestHandler<GetBreedingAreaQuery, BaseResponse<BreedingArea>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetBreedingAreaQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<BreedingArea>> Handle(GetBreedingAreaQuery request, CancellationToken cancellationToken)
        {
            var existBreedingArea = _unitOfWork.BreedingAreaRepository.Get(filter: ba => ba.BreedingAreaId.Equals(request.Id) && ba.IsDeleted == false, includeProperties: "ChickenCoops").FirstOrDefault();
            if (existBreedingArea == null)
            {
                return BaseResponse<BreedingArea>.FailureResponse(message: "Farm không tồn tại");
            }
            return BaseResponse<BreedingArea>.SuccessResponse(data: existBreedingArea);
        }
    }
}
