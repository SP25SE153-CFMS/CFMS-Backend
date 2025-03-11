using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.BreedingAreaFeat.Create
{
    public class CreateBreedingAreaCommand : IRequest<BaseResponse<bool>>
    {
        public CreateBreedingAreaCommand(string? breedingAreaCode, string? breedingAreaName, int? mealsPerDay, string? image, string? notes, Guid? farmId)
        {
            BreedingAreaCode = breedingAreaCode;
            BreedingAreaName = breedingAreaName;
            MealsPerDay = mealsPerDay;
            Image = image;
            Notes = notes;
            //CreatedDate = createdDate;
            FarmId = farmId;
        }

        public string? BreedingAreaCode { get; set; }

        public string? BreedingAreaName { get; set; }

        public int? MealsPerDay { get; set; }

        public string? Image { get; set; }

        public string? Notes { get; set; }

        public double? Area { get; set; }

        //public DateTime? CreatedDate { get; set; }

        public Guid? FarmId { get; set; }
    }
}
