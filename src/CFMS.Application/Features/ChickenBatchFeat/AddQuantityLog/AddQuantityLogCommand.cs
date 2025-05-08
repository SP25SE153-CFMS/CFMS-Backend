using CFMS.Application.Common;
using CFMS.Application.DTOs.QuantityLog;
using MediatR;

namespace CFMS.Application.Features.ChickenBatchFeat.AddQuantityLog
{
    public class AddQuantityLogCommand : IRequest<BaseResponse<bool>>
    {
        public AddQuantityLogCommand(Guid? chickenBatchId, DateTime? logDate, string? notes, IEnumerable<QuantityLogDetailRequest> quantityLogDetails, int? logType)
        {
            ChickenBatchId = chickenBatchId;
            LogDate = logDate;
            Notes = notes;
            QuantityLogDetails = quantityLogDetails;
            LogType = logType;
        }

        public Guid? ChickenBatchId { get; set; }

        public DateTime? LogDate { get; set; }

        public string? Notes { get; set; }

        public IEnumerable<QuantityLogDetailRequest> QuantityLogDetails { get; set; }

        public int? LogType { get; set; }

        public string ImageUrl { get; set; }
    }
}
