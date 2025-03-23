using CFMS.Application.Features.FeedLogFeat.Create;
using CFMS.Application.Features.FeedLogFeat.Delete;
using CFMS.Application.Features.FeedLogFeat.GetFeedLog;
using CFMS.Application.Features.FeedLogFeat.GetFeedLogs;
using CFMS.Application.Features.FeedLogFeat.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CFMS.Api.Controllers
{
    public class FeedLogController : BaseController
    {
        public FeedLogController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await Send(new GetFeedLogsQuery());
            return result;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await Send(new GetFeedLogQuery(id));
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateFeedLogCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateFeedLogCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await Send(new DeleteFeedLogCommand(id));
            return result;
        }
    }
}
