using CFMS.Application.Features.HarvestProductFeat.Create;
using CFMS.Application.Features.HarvestProductFeat.Delete;
using CFMS.Application.Features.HarvestProductFeat.GetHarvestProduct;
using CFMS.Application.Features.HarvestProductFeat.GetHarvestProducts;
using CFMS.Application.Features.HarvestProductFeat.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CFMS.Api.Controllers
{
    public class HarvestProductController : BaseController
    {
        public HarvestProductController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetHarvestProducts()
        {
            var result = await Send(new GetHarvestProductsQuery());
            return result;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHarvestProduct(Guid id)
        {
            var result = await Send(new GetHarvestProductQuery(id));
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateHarvestProductCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateHarvestProductCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await Send(new DeleteHarvestProductCommand(id));
            return result;
        }
    }
}
