using CFMS.Application.Common;
using CFMS.Application.DTOs.ChickenBatch;
using MediatR;

namespace CFMS.Application.Features.ChickenBatchFeat.DashboardChickenBatch
{
    public class DashboardChickenBatchQuery : IRequest<BaseResponse<DashboardChickenBatchResponse>>
    {
        public DashboardChickenBatchQuery(Guid batchId)
        {
            BatchId = batchId;
        }

        public Guid BatchId { get; set; }
    }
}
