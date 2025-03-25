using CFMS.Application.Features.ResourceFeat.GetResource;
using CFMS.Application.Features.ResourceFeat.GetResources;
using CFMS.Application.Features.UserFeat.Delete;
using CFMS.Application.Features.UserFeat.GetUser;
using CFMS.Application.Features.UserFeat.GetUsers;
using CFMS.Application.Features.UserFeat.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CFMS.Api.Controllers
{
    public class ResourceController : BaseController
    {
        public ResourceController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await Send(new GetResourcesQuery());
            return result;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await Send(new GetResourceQuery(id));
            return result;
        }

        //[HttpPut]
        //public async Task<IActionResult> Update(UpdateUserCommand command)
        //{
        //    var result = await Send(command);
        //    return result;
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(Guid id)
        //{
        //    var result = await Send(new DeleteUserCommand(id));
        //    return result;
        //}
    }
}
