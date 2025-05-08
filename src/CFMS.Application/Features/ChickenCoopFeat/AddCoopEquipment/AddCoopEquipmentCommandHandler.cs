using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.ChickenCoopFeat.AddCoopEquipment
{
    public class AddCoopEquipmentCommandHandler : IRequestHandler<AddCoopEquipmentCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddCoopEquipmentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(AddCoopEquipmentCommand request, CancellationToken cancellationToken)
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

            try
            {
                existCoop.CoopEquipments.Add(new CoopEquipment
                {
                    ChickenCoopId = request.ChickenCoopId,
                    EquipmentId = request.EquipmentId,
                    Quantity = request.Quantity,
                    AssignedDate = request.AssignedDate,
                    LastMaintenanceDate = request.AssignedDate,
                    NextMaintenanceDate = request.AssignedDate.Value.AddDays(request.MaintenanceInterval),
                    MaintenanceInterval = request.MaintenanceInterval,
                    Status = request.Status,
                    Note = request.Note,
                });

                _unitOfWork.ChickenCoopRepository.Update(existCoop);
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Thêm thành công");
                }
                return BaseResponse<bool>.FailureResponse(message: "Thêm không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra");
            }
        }
    }
}
