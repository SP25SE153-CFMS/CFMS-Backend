using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Services
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendAsync(string to, string subject, string body)
        {
            //var email = new MimeMessage();
            //email.From.Add(new MailboxAddress("Your App", _configuration["Email:Sender"]));
            //email.To.Add(new MailboxAddress("", to));
            //email.Subject = subject;

            //email.Body = new TextPart(TextFormat.Html) { Text = body };

            //using var smtp = new SmtpClient();
            //await smtp.ConnectAsync(_configuration["Email:SmtpServer"], 587, SecureSocketOptions.StartTls);
            //await smtp.AuthenticateAsync(_configuration["Email:Username"], _configuration["Email:Password"]);
            //await smtp.SendAsync(email);
            //await smtp.DisconnectAsync(true);
        }
    }
}
