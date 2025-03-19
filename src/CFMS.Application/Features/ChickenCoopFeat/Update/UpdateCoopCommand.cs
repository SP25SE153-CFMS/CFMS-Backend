using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.ChickenCoopFeat.Update
{
    public class UpdateCoopCommand : IRequest<BaseResponse<bool>>
    {
        public UpdateCoopCommand(Guid id, string? chickenCoopCode, string? chickenCoopName, int? capacity, int? area, double? density, int? currentQuantity, string? description, bool? status, Guid? purposeId, Guid? breedingAreaId)
        {
            Id = id;
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

        public Guid Id { get; set; }

        public string? ChickenCoopCode { get; set; }

        public string? ChickenCoopName { get; set; }

        public int? Capacity { get; set; }

        public int? Area { get; set; }

        public double? Density { get; set; }

        public int? CurrentQuantity { get; set; }

        public string? Description { get; set; }

        public bool? Status { get; set; }

        public Guid? PurposeId { get; set; }

        public Guid? BreedingAreaId { get; set; }
    }
}
