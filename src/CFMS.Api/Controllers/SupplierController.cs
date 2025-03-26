using CFMS.Application.Features.SupplierFeat.Create;
using CFMS.Application.Features.SupplierFeat.Delete;
using CFMS.Application.Features.SupplierFeat.GetSupplier;
using CFMS.Application.Features.SupplierFeat.GetSuppliers;
using CFMS.Application.Features.SupplierFeat.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CFMS.Api.Controllers
{
    public class SupplierController : BaseController
    {
        public SupplierController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetSuppliers()
        {
            var result = await Send(new GetSuppliersQuery());
            return result;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSupplier(Guid id)
        {
            var result = await Send(new GetSupplierQuery(id));
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSupplierCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateSupplierCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await Send(new DeleteSupplierCommand(id));
            return result;
        }
    }
}
