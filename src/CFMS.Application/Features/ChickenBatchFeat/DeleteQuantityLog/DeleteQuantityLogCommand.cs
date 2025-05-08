using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.ChickenBatchFeat.DeleteQuantityLog
{
    public class DeleteQuantityLogCommand : IRequest<BaseResponse<bool>>
    {
        public DeleteQuantityLogCommand(Guid batchId, Guid quantityLogId)
        {
            BatchId = batchId;
            QuantityLogId = quantityLogId;
        }

        public Guid BatchId { get; set; }

        public Guid QuantityLogId { get; set; }
    }
}
