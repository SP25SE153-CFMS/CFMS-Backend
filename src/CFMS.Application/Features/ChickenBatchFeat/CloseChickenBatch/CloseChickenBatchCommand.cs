using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.ChickenBatchFeat.CloseChickenBatch
{
    public class CloseChickenBatchCommand : IRequest<BaseResponse<bool>>
    {
        public CloseChickenBatchCommand(int status, Guid chickenBatchId)
        {
            Status = status;
            ChickenBatchId = chickenBatchId;
        }

        public int Status { get; set; }
        public Guid ChickenBatchId { get; set; }
    }
}
