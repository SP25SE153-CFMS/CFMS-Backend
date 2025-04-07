using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.BreedingAreaFeat.Update
{
    public class UpdateBreedingAreaCommand : IRequest<BaseResponse<bool>>
    {
        public UpdateBreedingAreaCommand(Guid breedingAreaId, string? breedingAreaCode, string? breedingAreaName, string? imageUrl, string? notes, decimal? area, bool? status)
        {
            BreedingAreaId = breedingAreaId;
            BreedingAreaCode = breedingAreaCode;
            BreedingAreaName = breedingAreaName;
            ImageUrl = imageUrl;
            Notes = notes;
            Area = area;
            Status = status;
        }

        public Guid BreedingAreaId { get; set; }

        public Guid FarmId { get; set; }

        public string? BreedingAreaCode { get; set; }

        public string? BreedingAreaName { get; set; }

        public string? ImageUrl { get; set; }

        public string? Notes { get; set; }

        public decimal? Area { get; set; }

        public int? Status { get; set; }
    }
}
