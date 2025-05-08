using CFMS.Application.Common;
using CFMS.Application.DTOs.ChickenDetail;
using MediatR;

namespace CFMS.Application.Features.ChickenBatchFeat.OpenChickenBatch
{
    public class OpenChickenBatchCommand : IRequest<BaseResponse<bool>>
    {
        public OpenChickenBatchCommand(Guid? chickenCoopId, Guid chickenId, IEnumerable<ChickenDetailRequest> chickenDetailRequests, string? chickenBatchName, string? stageCode, DateTime? startDate, int minGrowDays, int maxGrowDays)
        {
            ChickenCoopId = chickenCoopId;
            ChickenId = chickenId;
            ChickenDetailRequests = chickenDetailRequests;
            ChickenBatchName = chickenBatchName;
            StageCode = stageCode;
            StartDate = startDate;
            MinGrowDays = minGrowDays;
            MaxGrowDays = maxGrowDays;
        }

        public Guid? ChickenCoopId { get; set; }

        public Guid ChickenId { get; set; }

        public IEnumerable<ChickenDetailRequest> ChickenDetailRequests { get; set; }

        public string? ChickenBatchName { get; set; }

        public string? StageCode { get; set; }

        public DateTime? StartDate { get; set; }

        public int MinGrowDays { get; set; }

        public int MaxGrowDays { get; set; }
    }
}
