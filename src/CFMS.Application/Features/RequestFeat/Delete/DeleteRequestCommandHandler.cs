using CFMS.Application.Common;
using CFMS.Application.Features.ShiftFeat.Delete;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.RequestFeat.Delete
{
    public class DeleteRequestCommandHandler : IRequestHandler<DeleteRequestCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRequestCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(DeleteRequestCommand request, CancellationToken cancellationToken)
        {
            var existRequest = _unitOfWork.RequestRepository.Get(filter: f => f.RequestId.Equals(request.Id) && f.IsDeleted == false).FirstOrDefault();
            if (existRequest == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Phiếu yêu cầu không tồn tại");
            }

            try
            {
                _unitOfWork.RequestRepository.Delete(existRequest);
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Xóa thành công");
                }
                return BaseResponse<bool>.FailureResponse(message: "Xoá không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra:" + ex.Message);
            }
        }
    }
}
