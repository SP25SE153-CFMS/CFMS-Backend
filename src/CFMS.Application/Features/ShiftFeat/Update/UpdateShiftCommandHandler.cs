using CFMS.Application.Common;
using CFMS.Application.Features.FoodFeat.Update;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.ShiftFeat.Update
{
    public class UpdateShiftCommandHandler : IRequestHandler<UpdateShiftCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateShiftCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(UpdateShiftCommand request, CancellationToken cancellationToken)
        {
            var existShift = _unitOfWork.ShiftRepository.Get(filter: f => f.ShiftId.Equals(request.ShiftId) && f.IsDeleted == false).FirstOrDefault();
            if (existShift == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Ca làm không tồn tại");
            }

            var existName = _unitOfWork.ShiftRepository.Get(filter: s => s.ShiftName.Equals(request.ShiftName) && s.FarmId.Equals(existShift.FarmId) && s.IsDeleted == false && s.ShiftId != request.ShiftId).FirstOrDefault();
            if (existName == null)
            {
                return BaseResponse<bool>.FailureResponse("Tên ca làm đã tồn tại");
            }

            try
            {
                existShift.ShiftName = request.ShiftName;
                existShift.StartTime = request.StartTime;
                existShift.EndTime = request.EndTime;

                _unitOfWork.ShiftRepository.Update(existShift);
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
