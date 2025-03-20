using CFMS.Application.Features.GrowthStageFeat.Create;
using CFMS.Application.Features.GrowthStageFeat.Delete;
using CFMS.Application.Features.GrowthStageFeat.GetStage;
using CFMS.Application.Features.GrowthStageFeat.GetStages;
using CFMS.Application.Features.GrowthStageFeat.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CFMS.Api.Controllers
{
    public class GrowthStageController : BaseController
    {
        public GrowthStageController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await Send(new GetStagesQuery());
            return result;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await Send(new GetStageQuery(id));
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateStageCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateStageCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await Send(new DeleteStageCommand(id));
            return result;
        }
    }
}
