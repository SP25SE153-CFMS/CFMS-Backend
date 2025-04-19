using CFMS.Application.Features.WarehouseFeat.Create;
using CFMS.Application.Features.WarehouseFeat.Delete;
using CFMS.Application.Features.WarehouseFeat.GetWare;
using CFMS.Application.Features.WarehouseFeat.GetWares;
using CFMS.Application.Features.WarehouseFeat.GetWaresByFarmId;
using CFMS.Application.Features.WarehouseFeat.GetWareStock;
using CFMS.Application.Features.WarehouseFeat.GetWareStocks;
using CFMS.Application.Features.WarehouseFeat.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CFMS.Api.Controllers
{
    public class WareController : BaseController
    {
        public WareController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Gets()
        {
            var result = await Send(new GetWaresQuery());
            return result;
        }

        [HttpGet("warestocks/{wareId}/{resourceTypeId}")]
        public async Task<IActionResult> GetWareStocks(Guid wareId, Guid resourceTypeId)
        {
            var result = await Send(new GetWareStocksQuery(wareId, resourceTypeId));
            return result;
        }

        [HttpGet("warestock/{resourceId}")]
        public async Task<IActionResult> GetWareStock(Guid resourceId)
        {
            var result = await Send(new GetWareStockQuery(resourceId));
            return result;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await Send(new GetWareQuery(id));
            return result;
        }

        [HttpGet("farmId/{farmId}")]
        public async Task<IActionResult> GetWareByFarmId(Guid farmId)
        {
            var result = await Send(new GetWaresByFarmIdQuery(farmId));
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateWareCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateWareCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await Send(new DeleteWareCommand(id));
            return result;
        }
    }
}
