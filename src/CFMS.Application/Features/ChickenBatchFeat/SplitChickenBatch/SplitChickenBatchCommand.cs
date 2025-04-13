using CFMS.Application.Common;
using CFMS.Application.DTOs.ChickenDetail;
using MediatR;

namespace CFMS.Application.Features.ChickenBatchFeat.SplitChickenBatch
{
    public class SplitChickenBatchCommand : IRequest<BaseResponse<bool>>
    {
        public SplitChickenBatchCommand(Guid? parentBatchId, string? chickenBatchName, Guid? chickenCoopId, Guid chickenId, IEnumerable<ChickenDetailRequest> chickenDetailRequests, string? stageCode, DateTime? startDate, int minGrowDays, int maxGrowDays, string notes)
        {
            ParentBatchId = parentBatchId;
            ChickenBatchName = chickenBatchName;
            ChickenCoopId = chickenCoopId;
            ChickenId = chickenId;
            ChickenDetailRequests = chickenDetailRequests;
            StageCode = stageCode;
            StartDate = startDate;
            MinGrowDays = minGrowDays;
            MaxGrowDays = maxGrowDays;
            Notes = notes;
        }

        public Guid? ParentBatchId { get; set; }

        public string? ChickenBatchName { get; set; }

        public Guid? ChickenCoopId { get; set; }

        public Guid ChickenId { get; set; }

        public IEnumerable<ChickenDetailRequest> ChickenDetailRequests { get; set; }

        public string? StageCode { get; set; }

        public DateTime? StartDate { get; set; }

        public int MinGrowDays { get; set; }

        public int MaxGrowDays { get; set; }

        public string Notes { get; set; }
    }
}
