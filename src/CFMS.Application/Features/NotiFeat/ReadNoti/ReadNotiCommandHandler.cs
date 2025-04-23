using CFMS.Application.Common;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.NotiFeat.ReadNoti
{
    public class ReadNotiCommandHandler : IRequestHandler<ReadNotiCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReadNotiCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(ReadNotiCommand request, CancellationToken cancellationToken)
        {
            var existNoti = _unitOfWork.NotificationRepository.Get(filter: n => n.NotificationId.Equals(request.NotificationId)).FirstOrDefault();
            if (existNoti == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Thông báo không tồn tại");
            }

            try
            {
                existNoti.IsRead = 1;

                _unitOfWork.NotificationRepository.Update(existNoti);
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
