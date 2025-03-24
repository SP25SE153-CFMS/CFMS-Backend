using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.ChickenBatchFeat.DeleteGrowthBatch
{
    public class DeleteGrowthBatchCommand : IRequest<BaseResponse<bool>>
    {
        public DeleteGrowthBatchCommand(Guid chickenBatchId, Guid growthBatchId)
        {
            ChickenBatchId = chickenBatchId;
            GrowthBatchId = growthBatchId;
        }

        public Guid ChickenBatchId { get; set; }

        public Guid GrowthBatchId { get; set; }
    }
}
