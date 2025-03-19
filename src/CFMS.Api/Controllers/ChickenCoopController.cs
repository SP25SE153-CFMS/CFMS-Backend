using CFMS.Application.Features.ChickenCoopFeat.Create;
using CFMS.Application.Features.ChickenCoopFeat.Delete;
using CFMS.Application.Features.ChickenCoopFeat.GetCoop;
using CFMS.Application.Features.ChickenCoopFeat.GetCoops;
using CFMS.Application.Features.ChickenCoopFeat.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CFMS.Api.Controllers
{
    public class ChickenCoopController : BaseController
    {
        public ChickenCoopController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await Send(new GetCoopsQuery());
            return result;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await Send(new GetCoopQuery(id));
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCoopCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCoopCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await Send(new DeleteCoopCommand(id));
            return result;
        }
    }
}
