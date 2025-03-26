using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.ChickenBatchFeat.AddQuantityLog
{
    public class AddQuantityLogCommand : IRequest<BaseResponse<bool>>
    {
        public AddQuantityLogCommand(Guid? chickenBatchId, DateTime? logDate, string? notes, int? quantity, int? logType)
        {
            ChickenBatchId = chickenBatchId;
            LogDate = logDate;
            Notes = notes;
            Quantity = quantity;
            LogType = logType;
        }

        public Guid? ChickenBatchId { get; set; }

        public DateTime? LogDate { get; set; }

        public string? Notes { get; set; }

        public int? Quantity { get; set; }

        public int? LogType { get; set; }
    }
}
