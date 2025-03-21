using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.ChickenCoopFeat.GetCoops
{
    public class GetCoopsQueryHandler : IRequestHandler<GetCoopsQuery, BaseResponse<IEnumerable<ChickenCoop>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCoopsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<IEnumerable<ChickenCoop>>> Handle(GetCoopsQuery request, CancellationToken cancellationToken)
        {
            var coops = _unitOfWork.ChickenCoopRepository.Get(filter: c => c.IsDeleted == false && c.BreedingAreaId.Equals(request.BreedingAreaId));
            return BaseResponse<IEnumerable<ChickenCoop>>.SuccessResponse(data: coops);
        }
    }
}
