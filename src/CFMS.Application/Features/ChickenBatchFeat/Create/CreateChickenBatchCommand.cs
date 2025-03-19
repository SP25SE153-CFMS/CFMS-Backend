using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.ChickenBatchFeat.Create
{
    public class CreateChickenBatchCommand : IRequest<BaseResponse<bool>>
    {
        public CreateChickenBatchCommand(Guid? chickenCoopId, string? chickenBatchName, DateTime? startDate, DateTime? endDate, string? note, int? status)
        {
            ChickenCoopId = chickenCoopId;
            ChickenBatchName = chickenBatchName;
            StartDate = startDate;
            EndDate = endDate;
            Note = note;
            Status = status;
        }

        public Guid? ChickenCoopId { get; set; }

        public string? ChickenBatchName { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string? Note { get; set; }

        public int? Status { get; set; }
    }
}
