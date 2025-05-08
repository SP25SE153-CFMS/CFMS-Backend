using CFMS.Application.Features.ShiftFeat.Create;
using CFMS.Application.Features.ShiftFeat.Delete;
using CFMS.Application.Features.ShiftFeat.GetShift;
using CFMS.Application.Features.ShiftFeat.GetShiftByFarmId;
using CFMS.Application.Features.ShiftFeat.GetShifts;
using CFMS.Application.Features.ShiftFeat.Update;
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
        
        [HttpGet("byFarmId/{farmId}")]
        public async Task<IActionResult> GetShiftByFarmId(Guid farmId)
        {
            var result = await Send(new GetShiftByFarmIdQuery(farmId));
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
