using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.ChickenBatchFeat.CloseChickenBatch
{
    public class CloseChickenBatchCommand : IRequest<BaseResponse<bool>>
    {
        public CloseChickenBatchCommand(Guid chickenBatchId)
        {
            ChickenBatchId = chickenBatchId;
        }

        public Guid ChickenBatchId { get; set; }
    }
}
