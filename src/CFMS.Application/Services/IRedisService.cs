using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Services
{
    public interface IRedisService
    {
        Task SetOtpAsync(string userId, string otp, TimeSpan expiry);
        Task<string> GetOtpAsync(string userId);
        Task RemoveOtpAsync(string userId);
        Task<bool> VerifyOtpAsync(string userId, string inputOtp);
    }

}
