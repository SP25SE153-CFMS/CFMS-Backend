using CFMS.Application.Features.NotiFeat.ClearNoti;
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

        [HttpPut("read")]
        public async Task<IActionResult> Read(ReadNotiCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpPut("readAll")]
        public async Task<IActionResult> ReadAll(ReadAllNotiCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpPut("clearAll")]
        public async Task<IActionResult> ClearAll(ClearAllNotiCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpPut("clear")]
        public async Task<IActionResult> Clear(ClearNotiCommand command)
        {
            var result = await Send(command);
            return result;
        }
    }
}
