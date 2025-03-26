using CFMS.Application.Features.EquipmentFeat.Create;
using CFMS.Application.Features.EquipmentFeat.GetEquipment;
using CFMS.Application.Features.EquipmentFeat.GetEquipments;
using CFMS.Application.Features.EquipmentFeat.Update;
using CFMS.Application.Features.FoodFeat.Create;
using CFMS.Application.Features.FoodFeat.GetFood;
using CFMS.Application.Features.FoodFeat.GetFoods;
using CFMS.Application.Features.FoodFeat.Update;
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
        public async Task<IActionResult> GetEquipments()
        {
            var result = await Send(new GetEquipmentsQuery());
            return result;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEquipment(Guid id)
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
    }
}
