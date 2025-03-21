using CFMS.Application.Features.ChickenBatchFeat.AddGrowthStage;
using CFMS.Application.Features.ChickenBatchFeat.Create;
using CFMS.Application.Features.ChickenBatchFeat.Delete;
using CFMS.Application.Features.ChickenBatchFeat.GetBatch;
using CFMS.Application.Features.ChickenBatchFeat.GetBatchs;
using CFMS.Application.Features.ChickenBatchFeat.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CFMS.Api.Controllers
{
    public class ChickenBatchController : BaseController
    {
        public ChickenBatchController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await Send(new GetBatchsQuery());
            return result;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await Send(new GetBatchQuery(id));
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateChickenBatchCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateChickenBatchCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await Send(new DeleteChickenBatchCommand(id));
            return result;
        }

        [HttpPost("add-growthstage")]
        public async Task<IActionResult> AddGrowthStage(AddGrowthStageCommand command)
        {
            var result = await Send(command);
            return result;
        }
    }
}
