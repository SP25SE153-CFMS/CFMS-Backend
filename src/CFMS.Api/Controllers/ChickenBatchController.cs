using CFMS.Application.Features.ChickenBatchFeat.AddChicken;
using CFMS.Application.Features.ChickenBatchFeat.AddHealthLog;
using CFMS.Application.Features.ChickenBatchFeat.AddQuantityLog;
using CFMS.Application.Features.ChickenBatchFeat.CloseChickenBatch;
using CFMS.Application.Features.ChickenBatchFeat.Create;
using CFMS.Application.Features.ChickenBatchFeat.DashboardChickenBatch;
using CFMS.Application.Features.ChickenBatchFeat.Delete;
using CFMS.Application.Features.ChickenBatchFeat.DeleteHealthLog;
using CFMS.Application.Features.ChickenBatchFeat.DeleteQuantityLog;
using CFMS.Application.Features.ChickenBatchFeat.FeedLogChart;
using CFMS.Application.Features.ChickenBatchFeat.GetBatch;
using CFMS.Application.Features.ChickenBatchFeat.GetBatchs;
using CFMS.Application.Features.ChickenBatchFeat.OpenChickenBatch;
using CFMS.Application.Features.ChickenBatchFeat.QuantityLogDetail;
using CFMS.Application.Features.ChickenBatchFeat.SplitChickenBatch;
using CFMS.Application.Features.ChickenBatchFeat.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CFMS.Api.Controllers
{
    public class ChickenBatchController : BaseController
    {
        public ChickenBatchController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("Coop/{coopId}")]
        public async Task<IActionResult> GetBatchs(Guid coopId)
        {
            var result = await Send(new GetBatchsQuery(coopId));
            return result;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBatch(Guid id)
        {
            var result = await Send(new GetBatchQuery(id));
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateChickenBatchCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateChickenBatchCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await Send(new DeleteChickenBatchCommand(id));
            return result;
        }

        //[HttpPost("add-growthstage")]
        //public async Task<IActionResult> AddGrowthStage(AddGrowthStageCommand command)
        //{
        //    var result = await Send(command);
        //    return result;
        //}

        //[HttpPut("update-growthstage")]
        //public async Task<IActionResult> UpdateGrowthStage(UpdateGrowthBatchCommand command)
        //{
        //    var result = await Send(command);
        //    return result;
        //}

        //[HttpDelete("delete-growthstage")]
        //public async Task<IActionResult> DeleteGrowthStage(DeleteGrowthBatchCommand command)
        //{
        //    var result = await Send(command);
        //    return result;
        //}

        [HttpPost("add-chicken")]
        public async Task<IActionResult> AddChicken(AddChickenCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpPost("add-quantitylog")]
        public async Task<IActionResult> AddQuantityLog(AddQuantityLogCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpDelete("delete-quantitylog")]
        public async Task<IActionResult> DeleteQuantityLog(DeleteQuantityLogCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpPut("close-chickenbatch")]
        public async Task<IActionResult> CloseChickenBatch(CloseChickenBatchCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpPost("open-chickenbatch")]
        public async Task<IActionResult> OpenChickenBatch(OpenChickenBatchCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpGet("{batchId}/dashboard-chickenbatch")]
        public async Task<IActionResult> DashboardChickenBatch(Guid batchId)
        {
            var result = await Send(new DashboardChickenBatchQuery(batchId));
            return result;
        }

        [HttpPut("add-healthLog")]
        public async Task<IActionResult> AddHealthLog(AddHealthLogCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpPut("delete-healthLog")]
        public async Task<IActionResult> DeleteHealthLog(DeleteHealthLogCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpPut("split-chickenbatch")]
        public async Task<IActionResult> SplitChickenBatch(SplitChickenBatchCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpGet("{batchId}/chart-data")]
        public async Task<IActionResult> ChartData(Guid batchId)
        {
            var result = await Send(new FeedLogChartQuery(batchId));
            return result;
        }

        [HttpGet("QuantityLog/{id}")]
        public async Task<IActionResult> QuantityLogDetail(Guid id)
        {
            var result = await Send(new QuantityLogDetailQuery(id));
            return result;
        }
    }
}
