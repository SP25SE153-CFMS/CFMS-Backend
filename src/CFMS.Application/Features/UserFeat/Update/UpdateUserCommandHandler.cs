using CFMS.Application.Common;
using CFMS.Domain.Interfaces;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.UserFeat.Update
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public UpdateUserCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<BaseResponse<bool>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var existUser = _unitOfWork.UserRepository.GetByID(request.UserId);
            if (existUser == null)
            {
                return BaseResponse<bool>.FailureResponse("Người dùng không tồn tại");
            }

            return await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                if ((existUser.StartDate != null) && (!_currentUserService.IsOwner() ?? true))
                {
                    return BaseResponse<bool>.FailureResponse("Bạn không có quyền cập nhật thông tin người dùng");
                }

                if (!string.IsNullOrEmpty(request.Password) && existUser.HashedPassword.Equals(BCrypt.Net.BCrypt.HashPassword(request.Password)))
                {
                    return BaseResponse<bool>.FailureResponse("Mật khẩu mới không được trùng với mật khẩu cũ");
                }

                existUser.FullName = request.FullName ?? existUser.FullName;
                existUser.PhoneNumber = request.PhoneNumber ?? existUser.PhoneNumber;
                existUser.Mail = request.Mail ?? existUser.Mail;
                existUser.Avatar = request.Avatar ?? existUser.Avatar;
                existUser.DateOfBirth = request.DateOfBirth ?? existUser.DateOfBirth;
                existUser.Address = request.Address ?? existUser.Address;
                existUser.Cccd = request.Cccd ?? existUser.Cccd;
                existUser.Status = !string.IsNullOrEmpty(request.Status) ? request.Status : existUser.Status;
                existUser.Status = !string.IsNullOrEmpty(request.RoleName) ? request.RoleName : existUser.RoleName;
                existUser.HashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

                _unitOfWork.UserRepository.Update(existUser);

                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0
                    ? BaseResponse<bool>.SuccessResponse(message: "Cập nhật thành công")
                    : BaseResponse<bool>.FailureResponse(message: "Cập nhật không thành công");
            });
        }
    }
}
