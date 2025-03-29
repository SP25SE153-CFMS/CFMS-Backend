using CFMS.Application.Features.TaskFeat.Create;
using CFMS.Application.Features.TaskFeat.Delete;
using CFMS.Application.Features.TaskFeat.GetTask;
using CFMS.Application.Features.TaskFeat.GetTasks;
using CFMS.Application.Features.TaskFeat.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CFMS.Api.Controllers
{
    public class TaskController : BaseController
    {
        public TaskController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            var result = await Send(new GetTasksQuery());
            return result;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask(Guid id)
        {
            var result = await Send(new GetTaskQuery(id));
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTaskCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateTaskCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await Send(new DeleteTaskCommand(id));
            return result;
        }
    }
}
