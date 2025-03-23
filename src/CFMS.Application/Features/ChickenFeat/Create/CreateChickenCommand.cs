using CFMS.Application.Common;
using CFMS.Application.DTOs.Chicken;
using CFMS.Application.DTOs.NutritionPlan;
using MediatR;

namespace CFMS.Application.Features.ChickenFeat.Create
{
    public class CreateChickenCommand : IRequest<BaseResponse<bool>>
    {
        public CreateChickenCommand(string? chickenCode, string? chickenName, int? totalQuantity, string? description, int? status, Guid? chickenBatchId, List<ChickenDetailDto>? chickenDetails, Guid? chickenTypeId)
        {
            ChickenCode = chickenCode;
            ChickenName = chickenName;
            TotalQuantity = totalQuantity;
            Description = description;
            Status = status;
            ChickenBatchId = chickenBatchId;
            ChickenDetails = chickenDetails;
            ChickenTypeId = chickenTypeId;
        }

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
