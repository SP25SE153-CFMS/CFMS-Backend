using CFMS.Application.Common;
using CFMS.Application.DTOs.HealthLogDetail;
using MediatR;

namespace CFMS.Application.Features.ChickenBatchFeat.AddHealthLog
{
    public class AddHealthLogCommand : IRequest<BaseResponse<bool>>
    {
        public AddHealthLogCommand(DateTime? startDate, DateTime? endDate, string? notes, Guid? chickenBatchId, Guid? taskId, IEnumerable<HealthLogDetailRequest> healthLogDetails)
        {
            StartDate = startDate;
            EndDate = endDate;
            Notes = notes;
            ChickenBatchId = chickenBatchId;
            TaskId = taskId;
            HealthLogDetails = healthLogDetails;
        }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string? Notes { get; set; }

        public Guid? ChickenBatchId { get; set; }

        public Guid? TaskId { get; set; }

        public IEnumerable<HealthLogDetailRequest> HealthLogDetails { get; set; }
    }
}
