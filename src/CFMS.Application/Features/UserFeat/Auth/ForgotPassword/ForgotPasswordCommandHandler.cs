//using CFMS.Application.Common;
//using CFMS.Domain.Interfaces;
//using MediatR;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Cryptography;
//using System.Text;
//using System.Threading.Tasks;

//namespace CFMS.Application.Features.UserFeat.Auth.ForgotPassword
//{
//    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, BaseResponse<bool>>
//    {
//        private readonly IUnitOfWork _unitOfWork;
//        private readonly IEmailService _emailService;
//        private readonly ISmsService _smsService;
//        private readonly ICacheService _cacheService;

//        public ForgotPasswordCommandHandler(IUnitOfWork unitOfWork)
//        {
//            _unitOfWork = unitOfWork;
//        }

//        public async Task<BaseResponse<bool>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
//        {
//            var user = await _userRepository.GetByEmailOrPhoneAsync(request.EmailOrPhone);
//            if (user == null) return false;

//            if (request.Method == "OTP_SMS" || request.Method == "OTP_EMAIL")
//            {
//                int otp = RandomNumberGenerator.GetInt32(100000, 999999);
//                await _cacheService.SetAsync($"otp_{user.EmailOrPhone}", otp.ToString(), TimeSpan.FromMinutes(5));

//                if (request.Method == "OTP_SMS")
//                    await _smsService.SendAsync(user.Phone, $"Your OTP is: {otp}");
//                else
//                    await _emailService.SendAsync(user.Email, "Your OTP Code", $"Your OTP is: {otp}");
//            }
//            else if (request.Method == "LINK_EMAIL")
//            {
//                string token = Guid.NewGuid().ToString();
//                await _cacheService.SetAsync($"reset_{user.Email}", token, TimeSpan.FromHours(1));

//                string resetLink = $"https://yourapp.com/reset-password?email={user.Email}&token={token}";
//                await _emailService.SendAsync(user.Email, "Reset Password", $"Click here: {resetLink}");
//            }

//            return true;
//        }
//    }

//}
