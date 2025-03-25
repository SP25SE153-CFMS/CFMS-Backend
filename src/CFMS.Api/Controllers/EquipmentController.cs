using CFMS.Application.Features.EquipmentFeat.Create;
using CFMS.Application.Features.EquipmentFeat.Delete;
using CFMS.Application.Features.EquipmentFeat.GetEquipment;
using CFMS.Application.Features.EquipmentFeat.GetEquipments;
using CFMS.Application.Features.EquipmentFeat.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CFMS.Api.Controllers
{
    public class EquipmentController : BaseController
    {
        public EquipmentController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await Send(new GetEquipmentsQuery());
            return result;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await Send(new GetEquipmentQuery(id));
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEquipmentCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateEquipmentCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await Send(new DeleteEquipmentCommand(id));
            return result;
        }
    }
}
