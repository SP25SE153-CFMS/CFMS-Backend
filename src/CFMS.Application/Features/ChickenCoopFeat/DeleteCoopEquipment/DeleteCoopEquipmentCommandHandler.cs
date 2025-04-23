using CFMS.Application.Common;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.ChickenCoopFeat.DeleteCoopEquipment
{
    public class DeleteCoopEquipmentCommandHandler : IRequestHandler<DeleteCoopEquipmentCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCoopEquipmentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(DeleteCoopEquipmentCommand request, CancellationToken cancellationToken)
        {
            var existCoop = _unitOfWork.ChickenCoopRepository.Get(filter: c => c.ChickenCoopId.Equals(request.CoopId) && c.IsDeleted == false, includeProperties: [x => x.CoopEquipments]).FirstOrDefault();
            if (existCoop == null)
            {
                return BaseResponse<bool>.SuccessResponse(message: "Chuồng không tồn tại");
            }

            var existCoopEquip = _unitOfWork.CoopEquipmentRepository.Get(ce => ce.CoopEquipmentId.Equals(request.CoopEquipId) && ce.IsDeleted == false).FirstOrDefault();
            if (existCoopEquip == null)
            {
                return BaseResponse<bool>.SuccessResponse(message: "Trang thiết bị chưa có trong chuồng");
            }

            try
            {
                var temp = existCoop.CoopEquipments.ToList();
                temp.RemoveAll(ce => ce.CoopEquipmentId.Equals(existCoopEquip.CoopEquipmentId));
                existCoop.CoopEquipments = temp;

                _unitOfWork.ChickenCoopRepository.Update(existCoop);
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Xóa thành công");
                }
                return BaseResponse<bool>.SuccessResponse(message: "Xóa không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra");
            }
        }
    }
}
