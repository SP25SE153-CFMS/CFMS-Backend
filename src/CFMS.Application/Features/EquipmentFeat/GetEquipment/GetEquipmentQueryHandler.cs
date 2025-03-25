using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.EquipmentFeat.GetEquipment
{
    public class GetEquipmentQueryHandler : IRequestHandler<GetEquipmentQuery, BaseResponse<Equipment>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetEquipmentQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<Equipment>> Handle(GetEquipmentQuery request, CancellationToken cancellationToken)
        {
            var existEquip = _unitOfWork.EquipmentRepository.Get(filter: e => e.EquipmentId.Equals(request.EquipmentId) && e.IsDeleted == false).FirstOrDefault();
            if (existEquip == null)
            {
                return BaseResponse<Equipment>.FailureResponse(message: "Trang thiết bị không tồn tại");
            }
            return BaseResponse<Equipment>.SuccessResponse(data: existEquip);
        }
    }
}
