using CFMS.Application.Features.BreedingAreaFeat.Create;
using CFMS.Application.Features.BreedingAreaFeat.Delete;
using CFMS.Application.Features.BreedingAreaFeat.GetBreedingArea;
using CFMS.Application.Features.BreedingAreaFeat.GetBreedingAreas;
using CFMS.Application.Features.BreedingAreaFeat.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CFMS.Api.Controllers
{
    public class BreedingAreaController : BaseController
    {
        public BreedingAreaController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await Send(new GetBreedingAreasQuery());
            return result;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await Send(new GetBreedingAreaQuery(id));
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBreedingAreaCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateBreedingAreaCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await Send(new DeleteBreedingAreaCommand(id));
            return result;
        }
    }
}
