using CFMS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using CFMS.Application.Features.UserFeat.Auth.SignUp;
using CFMS.Application.Features.UserFeat.Auth.SignIn;
using CFMS.Application.Features.UserFeat.Auth.RefreshToken;
using CFMS.Application.Features.UserFeat.Auth.CurrentUser;
using CFMS.Application.Features.UserFeat.Auth.SignOut;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using CFMS.Application.Services;
using Microsoft.AspNetCore.Identity;
using CFMS.Application.DTOs.Auth;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using CFMS.Application.Features.UserFeat.Auth.VerifyPassword;
using CFMS.Application.Features.UserFeat.Auth.SentOtp;
using CFMS.Application.Features.UserFeat.Auth.ForgotPassword;
using CFMS.Application.Features.UserFeat.Auth.ResetPassword;

namespace CFMS.Api.Controllers
{
    public class AuthController : BaseController
    {
        private readonly ITokenService _tokenService;

        public AuthController(IMediator mediator, ITokenService tokenService) : base(mediator)
        {
            _tokenService = tokenService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpCommand command)
        {
            var response = await Send(command);
            return response;
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInCommand command)
        {
            var response = await Send(command);
            return response;
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command)
        {
            var response = await Send(command);
            return response;
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var response = await Send(new GetCurrentUserQuery());
            return response;
        }

        [HttpGet("google-signin")]
        public async Task<IActionResult> GoogleLogin()
        {
            var googleAuthUrl = await _mediator.Send(new GoogleAuthProfileQuery());
            return Redirect(googleAuthUrl.Data);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("google-callback")]
        public async Task<IActionResult> SignInWithGoogle([FromQuery] string code, [FromQuery] string state)
        {
            var response = await _mediator.Send(new SignInWithGoogleCommand
            {
                AuthorizationCode = code,
                State = state
            });
            //return response;

            if (!response.Success)
            {
                return Redirect($"https://cfms.site/auth/error?message={Uri.EscapeDataString(response.Message)}");
            }

            var token = response.Data;
            //Response.Cookies.Append("accessToken", token?.AccessToken ?? "", new CookieOptions
            //{
            //    HttpOnly = true,   
            //    Secure = true,       
            //    SameSite = SameSiteMode.Lax, 
            //    Path = "/",
            //    Expires = DateTimeOffset.UtcNow.AddHours(1),
            //    Domain = "cfms.site"
            //});

            //Response.Cookies.Append("refreshToken", token?.RefreshToken ?? "", new CookieOptions
            //{
            //    HttpOnly = true,
            //    Secure = true,
            //    SameSite = SameSiteMode.Strict,
            //    Path = "/",
            //    Expires = DateTimeOffset.UtcNow.AddDays(30),
            //    Domain = "cfms.site"
            //});

            //return Redirect("https://cfms.site");
            return Redirect($"https://cfms.site/check-login?token={Uri.EscapeDataString(token?.AccessToken)}&refreshToken={Uri.EscapeDataString(token?.RefreshToken)}");
        }

        [HttpPost("google-signin-mobile")]
        public async Task<IActionResult> SignInWithGoogleMobile([FromBody] GoogleSignInMobileCommand command)
        {
            var response = await Send(command);
            return response;
        }

        [HttpGet("google-callback-mobile")]
        public IActionResult GoogleCallback(string code, string state)
        {
            var redirectUrl = $"cfms://auth?code={code}&state={state}";
            return Redirect(redirectUrl);
        }

        [HttpPost("signout")]
        public async Task<IActionResult> SignOut()
        {
            var response = await Send(new SignOutQuery());
            return response;
        }

        [HttpGet("verify-password/{password}")]
        public async Task<IActionResult> VerifyPassword(string password)
        {
            var response = await Send(new VerifyPasswordQuery(password));
            return response;
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordCommand command)
        {
            var response = await Send(command);
            return response;
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordCommand command)
        {
            var response = await Send(command);
            return response;
        }

        [HttpGet("sms-otp/{phoneNumber}")]
        public async Task<IActionResult> SendSmsOtp(string phoneNumber)
        {
            var response = await Send(new SendOtpCommand(phoneNumber));
            return response;
        }
    }
}
