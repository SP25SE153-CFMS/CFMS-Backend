using CFMS.Application.Common;
using CFMS.Application.Features.FoodFeat.Delete;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.ShiftFeat.Delete
{
    public class DeleteShiftCommandHandler : IRequestHandler<DeleteShiftCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteShiftCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(DeleteShiftCommand request, CancellationToken cancellationToken)
        {
            var existShift = _unitOfWork.ShiftRepository.Get(filter: f => f.ShiftId.Equals(request.Id) && f.IsDeleted == false).FirstOrDefault();
            if (existShift == null)
            {
                return BaseResponse<bool>.SuccessResponse(message: "Ca làm không tồn tại");
            }

            try
            {
                _unitOfWork.ShiftRepository.Delete(existShift);
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Xóa thành công");
                }
                return BaseResponse<bool>.SuccessResponse(message: "Xoá không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra:" + ex.Message);
            }
        }
    }
}
