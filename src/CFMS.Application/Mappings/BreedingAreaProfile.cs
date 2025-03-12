using AutoMapper;
using CFMS.Application.Features.BreedingAreaFeat.Create;
using CFMS.Domain.Entities;

namespace CFMS.Application.Mappings
{
    public class BreedingAreaProfile : Profile
    {
        public BreedingAreaProfile()
        {
            CreateMap<CreateBreedingAreaCommand, BreedingArea>();
        }
    }
}
