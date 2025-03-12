using CFMS.Application.Features.FarmFeat.Create;
using CFMS.Application.Features.FarmFeat.Delete;
using CFMS.Application.Features.FarmFeat.GetFarm;
using CFMS.Application.Features.FarmFeat.GetFarms;
using CFMS.Application.Features.FarmFeat.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CFMS.Api.Controllers
{
    public class FarmController : BaseController
    {
        public FarmController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await Send(new GetFarmsQuery());
            return result;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await Send(new GetFarmQuery(id));
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateFarmCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateFarmCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await Send(new DeleteFarmCommand(id));
            return result;
        }
    }
}
