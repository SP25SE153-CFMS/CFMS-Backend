using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.ChickenFeat.Update
{
    public class UpdateChickenCommand : IRequest<BaseResponse<bool>>
    {
        public UpdateChickenCommand(Guid id, string? chickenCode, string? chickenName, int? totalQuantity, string? description, int? status, Guid? chickenBatchId)
        {
            Id = id;
            ChickenCode = chickenCode;
            ChickenName = chickenName;
            TotalQuantity = totalQuantity;
            Description = description;
            Status = status;
            ChickenBatchId = chickenBatchId;
        }

        public Guid Id { get; set; }

        public string? ChickenCode { get; set; }

        public string? ChickenName { get; set; }

        public int? TotalQuantity { get; set; }

        public string? Description { get; set; }

        public int? Status { get; set; }

        public Guid? ChickenBatchId { get; set; }
    }
}
