using CFMS.Application.Features.SystemConfigFeat.Create;
using CFMS.Application.Features.SystemConfigFeat.Delete;
using CFMS.Application.Features.SystemConfigFeat.GetConfig;
using CFMS.Application.Features.SystemConfigFeat.GetConfigs;
using CFMS.Application.Features.SystemConfigFeat.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CFMS.Api.Controllers
{
    public class ConfigController : BaseController
    {
        public ConfigController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Gets()
        {
            var result = await Send(new GetConfigsQuery());
            return result;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await Send(new GetConfigQuery(id));
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateConfigCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateConfigCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await Send(new DeleteConfigCommand(id));
            return result;
        }
    }
}
