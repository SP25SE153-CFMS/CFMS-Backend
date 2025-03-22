using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.ChickenBatchFeat.AddChicken
{
    public class AddChickenCommand : IRequest<BaseResponse<bool>>
    {
        public AddChickenCommand(Guid chickenId, Guid chickenBatchId)
        {
            ChickenId = chickenId;
            ChickenBatchId = chickenBatchId;
        }

        public Guid ChickenId { get; set; }

        public Guid ChickenBatchId { get; set; }
    }
}
