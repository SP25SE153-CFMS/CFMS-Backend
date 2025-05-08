using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Services
{
    public interface IMailService
    {
        Task SendAsync(string to, string subject, string body);
        Task SendOtpAsync(string toEmail, string otp);
    }
}
