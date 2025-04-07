using CFMS.Application.Features.NotiFeat.GetNotiByUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CFMS.Api.Controllers
{
    public class NotiController : BaseController
    {
        public NotiController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("User/{userId}")]
        public async Task<IActionResult> GetNotiByUserId(Guid userId)
        {
            var result = await Send(new GetNotiByUserQuery(userId));
            return result;
        }
    }
}
