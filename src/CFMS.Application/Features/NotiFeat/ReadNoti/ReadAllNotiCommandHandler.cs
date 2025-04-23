using CFMS.Application.Common;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.NotiFeat.ReadNoti
{
    public class ReadAllNotiCommandHandler : IRequestHandler<ReadAllNotiCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public ReadAllNotiCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<BaseResponse<bool>> Handle(ReadAllNotiCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _currentUserService.GetUserId();
            var existUser = _unitOfWork.UserRepository.Get(filter: u => u.UserId.Equals(Guid.Parse(currentUser))).FirstOrDefault();
            if (existUser == null)
            {
                return BaseResponse<bool>.SuccessResponse(message: "Người dùng không tồn tại");
            }
            var notiList = _unitOfWork.NotificationRepository.Get(filter: u => u.UserId.Equals(Guid.Parse(currentUser))).ToList();
            if (notiList == null)
            {
                return BaseResponse<bool>.SuccessResponse(message: "Người dùng không có thông báo nào");
            }

            try
            {
                foreach (var item in notiList)
                {
                    item.IsRead = 1;
                }

                _unitOfWork.NotificationRepository.UpdateRange(notiList);
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Cập nhật thành công");
                }
                return BaseResponse<bool>.SuccessResponse(message: "Cập nhật không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra");
            }
        }
    }
}
