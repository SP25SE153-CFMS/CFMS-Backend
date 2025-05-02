using CFMS.Application.Common;
using CFMS.Application.DTOs.FeedLog;
using MediatR;

namespace CFMS.Application.Features.ChickenBatchFeat.FeedLogChart
{
    public class FeedLogChartQuery : IRequest<BaseResponse<IEnumerable<ChartDataResponse>>>
    {
        public FeedLogChartQuery(Guid batchId)
        {
            BatchId = batchId;
        }

        public Guid BatchId { get; set; }
    }
}
