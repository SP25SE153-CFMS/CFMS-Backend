using CFMS.Application.Features.ChickenCoopFeat.GetCoop;
using CFMS.Application.Features.ChickenFeat.Create;
using CFMS.Application.Features.ChickenFeat.Delete;
using CFMS.Application.Features.ChickenFeat.GetChickens;
using CFMS.Application.Features.ChickenFeat.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CFMS.Api.Controllers
{
    public class ChickenController : BaseController
    {
        public ChickenController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("Batch/{batchId}")]
        public async Task<IActionResult> GetChickens(Guid batchId)
        {
            var result = await Send(new GetChickensQuery(batchId));
            return result;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetChicken(Guid id)
        {
            var result = await Send(new GetCoopQuery(id));
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateChickenCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateChickenCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await Send(new DeleteChickenCommand(id));
            return result;
        }
    }
}
