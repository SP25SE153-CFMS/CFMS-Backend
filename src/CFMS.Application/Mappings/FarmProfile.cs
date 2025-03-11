using AutoMapper;
using CFMS.Application.Features.FarmFeat.Create;
using CFMS.Domain.Entities;

namespace CFMS.Application.Mappings
{
    public class FarmProfile : Profile
    {
        public FarmProfile()
        {
            // Mapping from CreateFarmCommand to Farm entity
            CreateMap<CreateFarmCommand, Farm>()
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }
}
