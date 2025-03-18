using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.BreedingAreaFeat.Update
{
    public class UpdateBreedingAreaCommand : IRequest<BaseResponse<bool>>
    {
        public UpdateBreedingAreaCommand(Guid breedingAreaId, string? breedingAreaCode, string? breedingAreaName, int? mealsPerDay, string? image, string? notes)
        {
            BreedingAreaId = breedingAreaId;
            BreedingAreaCode = breedingAreaCode;
            BreedingAreaName = breedingAreaName;
            MealsPerDay = mealsPerDay;
            Image = image;
            Notes = notes;
        }

        public Guid BreedingAreaId { get; set; }

        public string? BreedingAreaCode { get; set; }

        public string? BreedingAreaName { get; set; }

        public int? MealsPerDay { get; set; }

        public string? Image { get; set; }

        public string? Notes { get; set; }

        public double? Area { get; set; }
    }
}
