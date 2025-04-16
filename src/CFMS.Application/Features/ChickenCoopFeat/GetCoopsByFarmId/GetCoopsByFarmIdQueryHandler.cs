using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.ChickenCoopFeat.GetCoopsByFarmId
{
    public class GetCoopsByFarmIdQueryHandler : IRequestHandler<GetCoopsByFarmIdQuery, BaseResponse<IEnumerable<ChickenCoop>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCoopsByFarmIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<BaseResponse<IEnumerable<ChickenCoop>>> Handle(GetCoopsByFarmIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
