using CFMS.Application.Common;
using CFMS.Application.Features.FoodFeat.Update;
using CFMS.Domain.Enums.Types;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.SystemConfigFeat.Update
{
    public class UpdateConfigCommandHandler : IRequestHandler<UpdateConfigCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateConfigCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(UpdateConfigCommand request, CancellationToken cancellationToken)
        {
            var existConfig = _unitOfWork.SystemConfigRepository.Get(filter: s => s.SettingName.Equals(request.SettingName) && s.IsDeleted == false).FirstOrDefault();
            if (existConfig == null)
            {
                return BaseResponse<bool>.FailureResponse("Tên cấu hình đã tồn tại");
            }

            switch (request.EntityType)
            {
                case nameof(EntityType.COOP_TYPE):
                    var coop = _unitOfWork.ChickenCoopRepository
                        .Get(e => e.ChickenCoopId.Equals(request.EntityId) && !e.IsDeleted)
                        .FirstOrDefault();
                    if (coop == null)
                    {
                        return BaseResponse<bool>.FailureResponse("Chuồng gà không tồn tại");
                    }
                    break;

                case nameof(EntityType.WARE_TYPE):
                    var warehouse = _unitOfWork.WarehouseRepository
                        .Get(e => e.WareId.Equals(request.EntityId) && !e.IsDeleted)
                        .FirstOrDefault();
                    if (warehouse == null)
                    {
                        return BaseResponse<bool>.FailureResponse("Kho không tồn tại");
                    }
                    break;

                default:
                    return BaseResponse<bool>.FailureResponse("Đối tượng cấu hình không hợp lệ");
            }

            try
            {
                existConfig.SettingName = request.SettingName;
                existConfig.SettingValue = request.SettingValue;
                existConfig.Description = request.Description;
                existConfig.EffectedDateFrom = request.EffectedDateFrom;
                existConfig.EffectedDateTo = request.EffectedDateTo;
                existConfig.EntityType = request.EntityType;
                existConfig.EntityId = request.EntityId;
                existConfig.Status = request.Status;

                _unitOfWork.SystemConfigRepository.Update(existConfig);
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Cập nhật thành công");
                }
                return BaseResponse<bool>.FailureResponse(message: "Cập nhật không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra:" + ex.Message);
            }
        }
    }
}
