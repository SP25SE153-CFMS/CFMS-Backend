using CFMS.Application.Features.GrowthStageFeat.AddNutritionPlan;
using CFMS.Application.Features.GrowthStageFeat.Create;
using CFMS.Application.Features.GrowthStageFeat.Delete;
using CFMS.Application.Features.GrowthStageFeat.DeleteNutritionPlan;
using CFMS.Application.Features.GrowthStageFeat.GetStage;
using CFMS.Application.Features.GrowthStageFeat.GetStages;
using CFMS.Application.Features.GrowthStageFeat.Update;
using CFMS.Application.Features.NutritionPlanFeat.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CFMS.Api.Controllers
{
    public class GrowthStageController : BaseController
    {
        public GrowthStageController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await Send(new GetStagesQuery());
            return result;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await Send(new GetStageQuery(id));
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateStageCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateStageCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await Send(new DeleteStageCommand(id));
            return result;
        }

        [HttpPost("add-nutritionplan")]
        public async Task<IActionResult> AddNutritionPlan(AddNutritionPlanCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpPost("delete-nutritionplan")]
        public async Task<IActionResult> DeleteNutritionPlan(DeleteNutritionPlanCommand command)
        {
            var result = await Send(command);
            return result;
        }
    }
}
