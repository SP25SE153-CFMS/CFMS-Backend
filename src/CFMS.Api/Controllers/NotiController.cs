using CFMS.Application.Features.NotiFeat.GetNotiByUser;
using CFMS.Application.Features.NotiFeat.ReadNoti;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CFMS.Api.Controllers
{
    public class NotiController : BaseController
    {
        public NotiController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetNotiByUserId()
        {
            var result = await Send(new GetNotiByUserQuery());
            return result;
        }

        [HttpPut]
        public async Task<IActionResult> ReadNoti(ReadNotiCommand command)
        {
            var result = await Send(command);
            return result;
        }
    }
}
