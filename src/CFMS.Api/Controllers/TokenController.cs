using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CFMS.Api.Controllers
{
    [Authorize]
    public class TokenController : BaseController
    {
        public TokenController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("admin")]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult AdminOnly()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Ok(new { Message = "Admin Accessed!", UserId = userId });
        }

        [HttpGet("user")]
        [Authorize(Policy = "UserOrAdmin")]
        public IActionResult UserOnly()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Ok(new { Message = "User Accessed!", UserId = userId });
        }
    }
}
