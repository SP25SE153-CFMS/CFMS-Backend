using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio.Types;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace CFMS.Application.Services.Impl
{
    public class SmsService : ISmsService
    {
        private readonly IConfiguration _config;

        public SmsService(IConfiguration config)
        {
            _config = config;
            TwilioClient.Init(_config["Twilio:AccountSid"], _config["Twilio:AuthToken"]);
        }

        public async Task SendAsync(string phoneNumber, string message)
        {
            await MessageResource.CreateAsync(
                body: message,
                from: new PhoneNumber(_config["Twilio:FromPhone"]),
                to: new PhoneNumber(phoneNumber)
            );
        }
    }

}
