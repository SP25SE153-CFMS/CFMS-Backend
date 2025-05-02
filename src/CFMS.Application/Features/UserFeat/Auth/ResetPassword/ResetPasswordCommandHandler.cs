using CFMS.Application.Common;
using CFMS.Application.Services;
using CFMS.Domain.Interfaces;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.UserFeat.Auth.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;
        private readonly IRedisService _redis;

        public ResetPasswordCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService, IRedisService redis)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
            _redis = redis;
        }

        public async Task<BaseResponse<bool>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = _unitOfWork.UserRepository.Get(filter: x => x.Mail == request.Email && x.GoogleId == null).FirstOrDefault();
            if (user == null)
            {
                return BaseResponse<bool>.FailureResponse("Không có người dùng nào có gmail này");
            }

            if (string.IsNullOrEmpty(request.NewPassword) || string.IsNullOrEmpty(request.ConfirmPassword))
            {
                return BaseResponse<bool>.FailureResponse("Mật khẩu không được để trống");
            }

            if (request.NewPassword != request.ConfirmPassword)
            {
                return BaseResponse<bool>.FailureResponse("Mật khẩu không khớp");
            }

            var otpJson = await _redis.GetOtpAsync(user.UserId.ToString());
            if (string.IsNullOrEmpty(otpJson))
            {
                return BaseResponse<bool>.FailureResponse("Mã xác thực không tồn tại hoặc đã hết hạn");
            }

            dynamic otpData = JsonConvert.DeserializeObject(otpJson);
            string cachedOtp = otpData.Otp;
            DateTime createdAt = otpData.CreatedAt;

            if (cachedOtp != request.Otp)
            {
                return BaseResponse<bool>.FailureResponse("Mã xác thực không đúng");
            }

            if (createdAt.AddMinutes(1) < DateTime.Now.ToLocalTime().AddHours(7))
            {
                return BaseResponse<bool>.FailureResponse("Mã xác thực đã hết hạn");
            }

            user.HashedPassword = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);

            _unitOfWork.UserRepository.Update(user);
            var result = await _unitOfWork.SaveChangesAsync();

            if (result > 0)
            {
                await _redis.RemoveOtpAsync(user.UserId.ToString());

                return BaseResponse<bool>.SuccessResponse("Đặt lại mật khẩu thành công");
            }
            else
            {
                return BaseResponse<bool>.FailureResponse("Đặt lại mật khẩu không thành công");
            }
        }

    }
}
