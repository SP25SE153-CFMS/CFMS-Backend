using CFMS.Application.Features.AssignmentFeat.Delete;
using CFMS.Application.Features.AssignmentFeat.Update;
using CFMS.Application.Features.StockReceipt.Create;
using CFMS.Application.Features.StockReceipt.Delete;
using CFMS.Application.Features.StockReceipt.GetStockReceipt;
using CFMS.Application.Features.StockReceipt.GetStockReceipts;
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
            var result = await Send(new GetStockReceiptQuery(id));
            return result;
        }

        [HttpGet("Farm/{farmId}")]
        public async Task<IActionResult> GetStockReceiptByFarmId(Guid farmId)
        {
            var result = await Send(new GetStockReceiptsQuery(farmId));
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateStockReceiptCommand command)
        {
            var result = await Send(command);
            return result;
        }

        //[HttpPut]
        //public async Task<IActionResult> Update(UpdateAssignmentCommand command)
        //{
        //    var result = await Send(command);
        //    return result;
        //}

        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteStockReceiptCommand command)
        {
            var result = await Send(command);
            return result;
        }
    }
}
