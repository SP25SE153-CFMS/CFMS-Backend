using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CFMS.Application.Services.Impl
{
    public class RedisService : IRedisService
    {
        private readonly IDistributedCache _cache;

        public RedisService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task SetOtpAsync(string userId, string otp, TimeSpan expiry)
        {
            var otpData = new
            {
                Otp = otp,
                CreatedAt = DateTime.Now.ToLocalTime()
            };

            var options = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(expiry);

            await _cache.SetStringAsync($"otp:{userId}", JsonConvert.SerializeObject(otpData), options);
        }


        public async Task<string> GetOtpAsync(string userId)
        {
            return await _cache.GetStringAsync($"otp:{userId}");
        }

        public async Task RemoveOtpAsync(string userId)
        {
            await _cache.RemoveAsync($"otp:{userId}");
        }

        public async Task<bool> VerifyOtpAsync(string userId, string inputOtp)
        {
            var otpDataString = await _cache.GetStringAsync($"otp:{userId}");

            if (string.IsNullOrEmpty(otpDataString))
            {
                return false;
            }

            var otpData = JsonConvert.DeserializeObject<dynamic>(otpDataString);

            if (otpData.Otp != inputOtp)
            {
                return false;
            }

            var createdAt = DateTime.Parse(otpData.CreatedAt.ToString());
            var expiryTime = createdAt.AddMinutes(5);
            if (DateTime.Now.ToLocalTime() > expiryTime)
            {
                await RemoveOtpAsync(userId);
                return false;
            }

            return true;
        }

    }

}
