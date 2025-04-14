using CFMS.Application.Common;
using CFMS.Application.DTOs.Chicken;
using CFMS.Application.DTOs.NutritionPlan;
using MediatR;

namespace CFMS.Application.Features.ChickenFeat.Create
{
    public class CreateChickenCommand : IRequest<BaseResponse<bool>>
    {
        public CreateChickenCommand(string? chickenCode, string? chickenName, int? totalQuantity, string? description, int? status, List<ChickenDetailDto>? chickenDetails, Guid? chickenTypeId, Guid? unitId, Guid? packageId, decimal? packageSize, Guid wareId)
        {
            ChickenCode = chickenCode;
            ChickenName = chickenName;
            TotalQuantity = totalQuantity;
            Description = description;
            Status = status;
            ChickenDetails = chickenDetails;
            ChickenTypeId = chickenTypeId;
            UnitId = unitId;
            PackageId = packageId;
            PackageSize = packageSize;
            WareId = wareId;
        }

        public string? ChickenCode { get; set; }

        public string? ChickenName { get; set; }

        public int? TotalQuantity { get; set; }

        public string? Description { get; set; }

        public int? Status { get; set; }

        //public Guid? ChickenBatchId { get; set; }

        public Guid? ChickenTypeId { get; set; }

        public Guid? UnitId { get; set; }

        public Guid? PackageId { get; set; }

        public decimal? PackageSize { get; set; }

        public Guid WareId { get; set; }

        public List<ChickenDetailDto>? ChickenDetails { get; set; }
    }
}
