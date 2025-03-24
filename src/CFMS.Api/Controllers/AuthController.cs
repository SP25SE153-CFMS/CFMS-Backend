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

namespace CFMS.Api.Controllers
{
    public class AuthController : BaseController
    {
        private readonly ITokenService _tokenService;

        public AuthController(IMediator mediator, ICurrentUserService currentUserService, ITokenService tokenService) : base(mediator)
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

        [HttpGet("google-callback")]
        public async Task<IActionResult> SignInWithGoogle([FromQuery] string code)
        {
            var response = await Send(new SignInWithGoogleCommand { AuthorizationCode = code });
            return response;
        }

        [HttpPost("signout")]
        public async Task<IActionResult> SignOut()
        {
            var response = await Send(new SignOutQuery());
            return response;
        }
    }
}
