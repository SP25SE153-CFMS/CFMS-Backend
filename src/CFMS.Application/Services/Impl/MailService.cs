using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Services.Impl
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

        public async Task SendOtpAsync(string toEmail, string otp)
        {
            var host = _configuration["Smtp:Host"];
            var port = int.Parse(_configuration["Smtp:Port"]);
            var username = _configuration["Smtp:Username"];
            var password = _configuration["Smtp:Password"];
            var fromEmail = _configuration["Smtp:FromEmail"];
            var enableSsl = bool.Parse(_configuration["Smtp:EnableSSL"]);

            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(fromEmail));
            message.To.Add(MailboxAddress.Parse(toEmail));
            message.Subject = "Quên mật khẩu";

            var bodyBuilder = new BodyBuilder();

            var htmlContent = $@"
            <!DOCTYPE html>
            <html lang=""en"">
            <head>
                <meta charset=""UTF-8"">
                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                <style>
                    body {{
                        font-family: Arial, sans-serif;
                        color: #333;
                        background-color: #f4f4f4;
                        margin: 0;
                        padding: 20px;
                    }}

                    .container {{
                        width: 100%;
                        max-width: 600px;
                        margin: 0 auto;
                        background-color: #ffffff;
                        padding: 20px;
                        border-radius: 10px;
                        box-shadow: 0px 4px 10px rgba(0, 0, 0, 0.1);
                    }}

                    .header {{
                        text-align: center;
                        font-size: 24px;
                        color: #2d87f0;
                    }}

                    .otp-code {{
                        font-size: 36px;
                        font-weight: bold;
                        text-align: center;
                        color: #2d87f0;
                        margin-top: 20px;
                    }}

                    .message {{
                        font-size: 16px;
                        text-align: center;
                        margin-top: 20px;
                    }}

                    .footer {{
                        text-align: center;
                        font-size: 12px;
                        color: #777;
                        margin-top: 30px;
                    }}

                    .button {{
                        display: inline-block;
                        padding: 10px 20px;
                        background-color: #2d87f0;
                        color: #ffffff;
                        text-decoration: none;
                        border-radius: 5px;
                        margin-top: 20px;
                        text-align: center;
                    }}

                    .button:hover {{
                        background-color: #1a63c3;
                    }}

                    .button-container {{
                        text-align: center;
                        margin-top: 30px;
                    }}
                </style>
            </head>
                <body style=""font-family: Arial, sans-serif; background-color: #f4f4f4; padding: 20px;"">
                  <div style=""max-width: 600px; margin: auto; background-color: #fff; padding: 20px; border-radius: 10px;"">
                    <h2 style=""text-align: center; color: #2d87f0;"">Quên mật khẩu</h2>
                    <p style=""text-align: center; font-size: 32px; color: #2d87f0; font-weight: bold;"">{otp}</p>
                    <p style=""text-align: center;"">Mã OTP có hiệu lực trong vòng 60 giây.</p>
                    <div style=""text-align: center; margin: 30px 0;"">
                      <a href=""https://cfms.site/forgot-password/input-OTP?otp={otp}"" style=""padding: 12px 24px; background-color: #2d87f0; color: white; text-decoration: none; border-radius: 5px;"">Xác minh ngay</a>
                    </div>
                    <p style=""text-align: center; font-size: 12px; color: #777;"">Nếu bạn không yêu cầu OTP, bạn có thể bỏ qua email này.</p>
                  </div>
                </body>
            </html>
        ";

            bodyBuilder.HtmlBody = htmlContent;
            message.Body = bodyBuilder.ToMessageBody();

            using (var smtp = new SmtpClient())
            {
                await smtp.ConnectAsync(host, port, enableSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.Auto);
                await smtp.AuthenticateAsync(username, password);
                await smtp.SendAsync(message);
                await smtp.DisconnectAsync(true);
            }
        }
    }
}
