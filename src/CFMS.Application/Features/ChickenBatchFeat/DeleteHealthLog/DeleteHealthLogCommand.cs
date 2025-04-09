using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.ChickenBatchFeat.DeleteHealthLog
{
    public class DeleteHealthLogCommand : IRequest<BaseResponse<bool>>
    {
        public DeleteHealthLogCommand(Guid healthLogId, Guid batchId)
        {
            HealthLogId = healthLogId;
            BatchId = batchId;
        }

        public Guid HealthLogId { get; set; }

        public Guid BatchId { get; set; }
    }
}
