using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.ChickenCoopFeat.Create
{
    public class CreateCoopCommand : IRequest<BaseResponse<bool>>
    {
        public CreateCoopCommand(string? chickenCoopCode, string? chickenCoopName, int? capacity, int? area, double? density, int? currentQuantity, string? description, int? status, Guid? purposeId, Guid? breedingAreaId)
        {
            ChickenCoopCode = chickenCoopCode;
            ChickenCoopName = chickenCoopName;
            Capacity = capacity;
            Area = area;
            Density = density;
            CurrentQuantity = currentQuantity;
            Description = description;
            Status = status;
            PurposeId = purposeId;
            BreedingAreaId = breedingAreaId;
        }

        public string? ChickenCoopCode { get; set; }

        public string? ChickenCoopName { get; set; }

        public int? Capacity { get; set; }

        public int? Area { get; set; }

        public double? Density { get; set; }

        public int? CurrentQuantity { get; set; }

        public string? Description { get; set; }

        public int? Status { get; set; }

        public Guid? PurposeId { get; set; }

        public Guid? BreedingAreaId { get; set; }
    }
}
