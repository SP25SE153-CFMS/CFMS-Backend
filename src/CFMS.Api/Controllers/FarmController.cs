using CFMS.Application.Commands.FarmFeat.Create;
using CFMS.Application.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CFMS.Api.Controllers
{
    public class FarmController : BaseController
    {
        public FarmController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public string Get()
        {
            return "Hello World";
        }

        [HttpGet("{id}")]
        public string Get(string id)
        {
            return "Hello World";
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateFarmCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpPut("{id}")]
        public string Update(string id)
        {
            return "Hello World";
        }

        [HttpDelete("{id}")]
        public string Delete(string id)
        {
            return "Hello World";
        }
    }
}
