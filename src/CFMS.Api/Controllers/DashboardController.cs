using CFMS.Application.Features.BreedingAreaFeat.GetBreedingAreas;
using CFMS.Application.Features.DashboardFeat.GetDashboard;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CFMS.Api.Controllers
{
    public class DashboardController : BaseController
    {
        public DashboardController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("Farm/{farmId}")]
        public async Task<IActionResult> GetDashboard(Guid farmId)
        {
            var result = await Send(new GetDashboardQuery(farmId));
            return result;
        }
    }
}
