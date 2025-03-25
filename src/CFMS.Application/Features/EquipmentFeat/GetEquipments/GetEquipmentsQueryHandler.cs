using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.EquipmentFeat.GetEquipments
{
    public class GetEquipmentsQueryHandler : IRequestHandler<GetEquipmentsQuery, BaseResponse<IEnumerable<Equipment>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetEquipmentsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<IEnumerable<Equipment>>> Handle(GetEquipmentsQuery request, CancellationToken cancellationToken)
        {
            var equips = _unitOfWork.EquipmentRepository.Get(filter: e => e.IsDeleted == false);
            return BaseResponse<IEnumerable<Equipment>>.SuccessResponse(data: equips);
        }
    }
}
