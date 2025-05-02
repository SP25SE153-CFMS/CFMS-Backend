using CFMS.Application.Common;
using CFMS.Application.Services;
using CFMS.Application.Services.Impl;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.UserFeat.Auth.ForgotPassword
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMailService _mailService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUtilityService _utilityService;
        private readonly IRedisService _redis;

        public ForgotPasswordCommandHandler(IUnitOfWork unitOfWork, IMailService mailService, ICurrentUserService currentUserService, IUtilityService utilityService, IRedisService redis)
        {
            _unitOfWork = unitOfWork;
            _mailService = mailService;
            _currentUserService = currentUserService;
            _utilityService = utilityService;
            _redis = redis;
        }

        public async Task<BaseResponse<bool>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = _unitOfWork.UserRepository.Get(filter: x => x.Mail == request.Email && x.GoogleId == null).FirstOrDefault();
            if (user == null)
            {
                return BaseResponse<bool>.FailureResponse("Không có người dùng nào có gmail này");
            }

            try
            {
                await _redis.RemoveOtpAsync(user.UserId.ToString());

                var otp = _utilityService.GenerateOTP();

                await _redis.SetOtpAsync(user.UserId.ToString(), otp, TimeSpan.FromMinutes(5));

                await _mailService.SendOtpAsync(user.Mail, otp);
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse("Lỗi khi gửi mail: " + ex.Message);
            }

            return BaseResponse<bool>.SuccessResponse("Gửi mã xác thực thành công");
        }

    }

}
