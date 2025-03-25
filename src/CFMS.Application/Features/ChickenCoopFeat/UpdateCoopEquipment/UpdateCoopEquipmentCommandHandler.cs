using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.ChickenCoopFeat.UpdateCoopEquipment
{
    public class UpdateCoopEquipmentCommandHandler : IRequestHandler<UpdateCoopEquipmentCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCoopEquipmentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(UpdateCoopEquipmentCommand request, CancellationToken cancellationToken)
        {
            var existCoop = _unitOfWork.ChickenCoopRepository.Get(filter: c => c.ChickenCoopId.Equals(request.ChickenCoopId) && c.IsDeleted == false).FirstOrDefault();
            if (existCoop == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Chuồng không tồn tại");
            }

            var existEquip = _unitOfWork.EquipmentRepository.Get(filter: e => e.EquipmentId.Equals(request.EquipmentId) && e.IsDeleted == false).FirstOrDefault();
            if (existEquip == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Trang thiết bị không tồn tại");
            }

            var existCoopEquip = _unitOfWork.CoopEquipmentRepository.Get(ce => ce.CoopEquipmentId.Equals(request.CoopEquipmentId) && ce.IsDeleted == false).FirstOrDefault();
            if (existCoopEquip == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Trang thiết bị trong chuồng không tồn tại");
            }

            try
            {
                existCoopEquip.ChickenCoopId = request.ChickenCoopId;
                existCoopEquip.EquipmentId = request.EquipmentId;
                existCoopEquip.Quantity = request.Quantity;
                existCoopEquip.AssignedDate = request.AssignedDate;
                existCoopEquip.LastMaintenanceDate = request.LastMaintenanceDate;
                existCoopEquip.NextMaintenanceDate = request.NextMaintenanceDate;
                existCoopEquip.MaintenanceInterval = request.MaintenanceInterval;
                existCoopEquip.Status = request.Status;
                existCoopEquip.Note = request.Note;

                _unitOfWork.CoopEquipmentRepository.Update(existCoopEquip);
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Cập nhật thành công");
                }
                return BaseResponse<bool>.FailureResponse(message: "Cập nhật không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra");
            }
        }
    }
}
