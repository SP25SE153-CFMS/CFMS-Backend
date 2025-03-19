using CFMS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using CFMS.Application.Features.UserFeat.Auth.SignUp;
using CFMS.Application.Features.UserFeat.Auth.SignIn;
using CFMS.Application.Features.UserFeat.Auth.RefreshToken;
using CFMS.Application.Features.UserFeat.Auth.CurrentUser;
using CFMS.Application.Features.UserFeat.Auth.SignOut;

namespace CFMS.Api.Controllers
{
    public class AuthController : BaseController
    {
        public AuthController(IMediator mediator, ICurrentUserService currentUserService) : base(mediator)
        {
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

        [HttpPost("google-signin")]
        public async Task<IActionResult> LoginWithGoogle([FromBody] SignInWithGoogleCommand command)
        {
            var response = await Send(command);
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
