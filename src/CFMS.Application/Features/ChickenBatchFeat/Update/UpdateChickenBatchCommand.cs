using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.ChickenBatchFeat.Update
{
    public class UpdateChickenBatchCommand : IRequest<BaseResponse<bool>>
    {
        public UpdateChickenBatchCommand(Guid id, string? chickenBatchName, DateTime? startDate, DateTime? endDate, string? note, int? status)
        {
            Id = id;
            ChickenBatchName = chickenBatchName;
            StartDate = startDate;
            EndDate = endDate;
            Note = note;
            Status = status;
        }

        public Guid Id { get; set; }

        public string? ChickenBatchName { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string? Note { get; set; }

        public int? Status { get; set; }
    }
}
