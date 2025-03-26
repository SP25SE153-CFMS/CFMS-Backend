using CFMS.Application.Features.FoodFeat.Create;
using CFMS.Application.Features.FoodFeat.Delete;
using CFMS.Application.Features.FoodFeat.GetFood;
using CFMS.Application.Features.FoodFeat.GetFoods;
using CFMS.Application.Features.FoodFeat.Update;
using CFMS.Application.Features.ShiftFeat.Create;
using CFMS.Application.Features.ShiftFeat.Delete;
using CFMS.Application.Features.ShiftFeat.GetShift;
using CFMS.Application.Features.ShiftFeat.GetShifts;
using CFMS.Application.Features.ShiftFeat.Update;
using CFMS.Application.Features.WarehouseFeat.Delete;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CFMS.Api.Controllers
{
    public class ShiftController : BaseController
    {
        public ShiftController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetShifts()
        {
            var result = await Send(new GetShiftsQuery());
            return result;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetShift(Guid id)
        {
            var result = await Send(new GetShiftQuery(id));
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateShiftCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateShiftCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await Send(new DeleteShiftCommand(id));
            return result;
        }
    }
}
