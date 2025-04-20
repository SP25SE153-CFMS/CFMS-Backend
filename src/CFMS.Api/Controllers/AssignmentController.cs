using CFMS.Application.Features.AssignmentFeat.AssignEmployee;
using CFMS.Application.Features.AssignmentFeat.Delete;
using CFMS.Application.Features.AssignmentFeat.GetAssignment;
using CFMS.Application.Features.AssignmentFeat.GetAssignmentByFarmId;
using CFMS.Application.Features.AssignmentFeat.GetAssignments;
using CFMS.Application.Features.AssignmentFeat.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CFMS.Api.Controllers
{
    public class AssignmentController : BaseController
    {
        public AssignmentController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetAssignments()
        {
            var result = await Send(new GetAssignmentsQuery());
            return result;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAssignment(Guid id)
        {
            var result = await Send(new GetAssignmentQuery(id));
            return result;
        }

        [HttpGet("Farm/{farmId}")]
        public async Task<IActionResult> GetAssignmentByFarmId(Guid farmId)
        {
            var result = await Send(new GetAssignmentByFarmIdQuery(farmId));
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Create(AssignEmployeeCommand command)
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
