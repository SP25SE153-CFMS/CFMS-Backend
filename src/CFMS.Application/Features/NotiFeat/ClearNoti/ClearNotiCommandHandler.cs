using CFMS.Application.Common;
using CFMS.Application.Features.NotiFeat.ReadNoti;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.NotiFeat.ClearNoti
{
    public class ClearNotiCommandHandler : IRequestHandler<ClearNotiCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClearNotiCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(ClearNotiCommand request, CancellationToken cancellationToken)
        {
            var existNoti = _unitOfWork.NotificationRepository.Get(filter: n => n.NotificationId.Equals(request.NotificationId)).FirstOrDefault();
            if (existNoti == null)
            {
                return BaseResponse<bool>.SuccessResponse(message: "Thông báo không tồn tại");
            }

            try
            {
                _unitOfWork.NotificationRepository.Delete(existNoti);
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Xoá thành công");
                }
                return BaseResponse<bool>.SuccessResponse(message: "Xoá không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra");
            }
        }
    }
}
