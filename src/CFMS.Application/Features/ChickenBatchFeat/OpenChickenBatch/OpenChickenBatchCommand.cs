using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.ChickenBatchFeat.OpenChickenBatch
{
    public class OpenChickenBatchCommand : IRequest<BaseResponse<bool>>
    {
        public OpenChickenBatchCommand(Guid chickenId, string? chickenBatchName, string? stageCode, DateTime? startDate, Guid? chickenCoopId)
        {
            ChickenId = chickenId;
            ChickenBatchName = chickenBatchName;
            StageCode = stageCode;
            StartDate = startDate;
            ChickenCoopId = chickenCoopId;
        }

        public Guid? ChickenCoopId { get; set; }

        public Guid ChickenId { get; set; }

        public string? ChickenBatchName { get; set; }

        public string? StageCode { get; set; }

        public DateTime? StartDate { get; set; }
    }
}
