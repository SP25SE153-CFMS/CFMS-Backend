using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.BreedingAreaFeat.Create
{
    public class CreateBreedingAreaCommand : IRequest<BaseResponse<bool>>
    {
        public CreateBreedingAreaCommand(string? breedingAreaCode, string? breedingAreaName, string? imageUrl, string? notes, Guid? farmId, double? area, bool? status)
        {
            BreedingAreaCode = breedingAreaCode;
            BreedingAreaName = breedingAreaName;
            ImageUrl = imageUrl;
            Notes = notes;
            FarmId = farmId;
            Area = area;
            Status = status;
        }

        public string? BreedingAreaCode { get; set; }

        public string? BreedingAreaName { get; set; }

        public string? ImageUrl { get; set; }

        public string? Notes { get; set; }

        public Guid? FarmId { get; set; }

        public double? Area { get; set; }

        public bool? Status { get; set; }
    }
}
