using CFMS.Application.Features.NutritionPlanFeat.AddFeedSession;
using CFMS.Application.Features.NutritionPlanFeat.Create;
using CFMS.Application.Features.NutritionPlanFeat.Delete;
using CFMS.Application.Features.NutritionPlanFeat.DeleteFeedSession;
using CFMS.Application.Features.NutritionPlanFeat.GetNutritionPlan;
using CFMS.Application.Features.NutritionPlanFeat.GetNutritionPlans;
using CFMS.Application.Features.NutritionPlanFeat.Update;
using CFMS.Application.Features.NutritionPlanFeat.UpdateFeedSession;
using CFMS.Application.Features.NutritionPlanFeat.UpdateNutritionPlanDetail;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CFMS.Api.Controllers
{
    public class NutritionPlanController : BaseController
    {
        public NutritionPlanController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await Send(new GetNutritionPlansQuery());
            return result;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await Send(new GetNutritionPlanQuery(id));
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateNutritionPlanCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateNutritionPlanCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await Send(new DeleteNutritionPlanCommand(id));
            return result;
        }

        [HttpPost("add-feedsession")]
        public async Task<IActionResult> AddFeedSession(AddFeedSessionCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpPut("update-feedsession")]
        public async Task<IActionResult> UpdateFeedSession(UpdateFeedSessionCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpPut("delete-feedsession")]
        public async Task<IActionResult> DeleteFeedSession(DeleteFeedSessionCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpPut("update-nutritionplandetail")]
        public async Task<IActionResult> UpdateNutritionPlanDetail(UpdateNutritionPlanDetailCommand command)
        {
            var result = await Send(command);
            return result;
        }
    }
}
