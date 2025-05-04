using CFMS.Application.Features.AssignmentFeat.AssignEmployee;
using CFMS.Application.Features.AssignmentFeat.Delete;
using CFMS.Application.Features.AssignmentFeat.GetAssignment;
using CFMS.Application.Features.AssignmentFeat.GetAssignmentByFarmId;
using CFMS.Application.Features.AssignmentFeat.GetAssignments;
using CFMS.Application.Features.AssignmentFeat.Update;
using CFMS.Application.Features.StockReceipt.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CFMS.Api.Controllers
{
    public class StockReceiptController : BaseController
    {
        public StockReceiptController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStockReceipt(Guid id)
        {
            var result = await Send(new GetAssignmentQuery(id));
            return result;
        }

        [HttpGet("Farm/{farmId}")]
        public async Task<IActionResult> GetStockReceiptByFarmId(Guid farmId)
        {
            var result = await Send(new GetAssignmentByFarmIdQuery(farmId));
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateStockReceiptCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateAssignmentCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteAssignmentCommand command)
        {
            var result = await Send(command);
            return result;
        }
    }
}
