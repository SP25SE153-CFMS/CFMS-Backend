using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.ChickenCoopFeat.Update
{
    public class UpdateCoopCommand : IRequest<BaseResponse<bool>>
    {
        public UpdateCoopCommand(Guid id, string? chickenCoopCode, string? chickenCoopName, int? capacity, int? area, string? status, string? description, Guid? purposeId, Guid? breedingAreaId)
        {
            Id = id;
            ChickenCoopCode = chickenCoopCode;
            ChickenCoopName = chickenCoopName;
            Capacity = capacity;
            Area = area;
            Status = status;
            Description = description;
            PurposeId = purposeId;
            BreedingAreaId = breedingAreaId;
        }

        public Guid Id { get; set; }

        public string? ChickenCoopCode { get; set; }

        public string? ChickenCoopName { get; set; }

        public int? Capacity { get; set; }

        public int? Area { get; set; }

        public string? Status { get; set; }

        public string? Description { get; set; }

        public Guid? PurposeId { get; set; }

        public Guid? BreedingAreaId { get; set; }
    }
}
