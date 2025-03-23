using CFMS.Application.Common;
using CFMS.Application.DTOs.Chicken;
using MediatR;

namespace CFMS.Application.Features.ChickenFeat.Update
{
    public class UpdateChickenCommand : IRequest<BaseResponse<bool>>
    {
        public UpdateChickenCommand(Guid id, string? chickenCode, string? chickenName, int? totalQuantity, string? description, int? status, Guid? chickenBatchId, Guid? chickenTypeId, List<ChickenDetailDto>? chickenDetails)
        {
            Id = id;
            ChickenCode = chickenCode;
            ChickenName = chickenName;
            TotalQuantity = totalQuantity;
            Description = description;
            Status = status;
            ChickenBatchId = chickenBatchId;
            ChickenTypeId = chickenTypeId;
            ChickenDetails = chickenDetails;
        }

        public Guid Id { get; set; }

        public string? ChickenCode { get; set; }

        public string? ChickenName { get; set; }

        public int? TotalQuantity { get; set; }

        public string? Description { get; set; }

        public int? Status { get; set; }

        public Guid? ChickenBatchId { get; set; }

        public Guid? ChickenTypeId { get; set; }

        public List<ChickenDetailDto>? ChickenDetails { get; set; }
    }
}
