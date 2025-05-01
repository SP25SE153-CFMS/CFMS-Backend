using CFMS.Application.Common;
using CFMS.Application.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.UserFeat.Auth.SentOtp
{
    public class SendOtpCommandHandler : IRequestHandler<SendOtpCommand, BaseResponse<Unit>>
    {
        private readonly IRedisService _redis;
        private readonly ISmsService _sms;

        public SendOtpCommandHandler(IRedisService redis, ISmsService sms)
        {
            _redis = redis;
            _sms = sms;
        }

        public async Task<BaseResponse<Unit>> Handle(SendOtpCommand request, CancellationToken ct)
        {
            var otp = new Random().Next(100000, 999999).ToString();
            await _redis.SetOtpAsync(request.PhoneNumber, otp, TimeSpan.FromMinutes(5));
            await _sms.SendAsync(request.PhoneNumber, $"Your OTP is {otp}");
            return BaseResponse<Unit>.SuccessResponse(Unit.Value);
        }
    }
}
