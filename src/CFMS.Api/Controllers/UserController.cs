using CFMS.Application.Common;
using CFMS.Application.Features.UserFeat.Delete;
using CFMS.Application.Features.UserFeat.GetUser;
using CFMS.Application.Features.UserFeat.GetUserByCCCDByPhoneByEmail;
using CFMS.Application.Features.UserFeat.GetUsers;
using CFMS.Application.Features.UserFeat.Update;
using Google.Apis.Drive.v3;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CFMS.Api.Controllers
{
    public class UserController : BaseController
    {
        public UserController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await Send(new GetUsersQuery());
            return result;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await Send(new GetUserQuery(id));
            return result;
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateUserCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await Send(new DeleteUserCommand(id));
            return result;
        }

        [HttpPost("upload-image")]
        public async Task<IActionResult> UploadImage(UploadImageCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpGet("search/{searchTemp}")]
        public async Task<IActionResult> UploadImage(string searchTemp)
        {
            var result = await Send(new GetUserByCCCDByPhoneByEmailQuery(searchTemp));
            return result;
        }
    }
}
