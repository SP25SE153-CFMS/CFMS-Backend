using AutoMapper;
using CFMS.Application.Features.ChickenFeat.Create;
using CFMS.Domain.Entities;

namespace CFMS.Application.Mappings
{
    public class ChickenProfile : Profile
    {
        public ChickenProfile()
        {
            CreateMap<CreateChickenCommand, Chicken>()
                .ForMember(dest => dest.ChickenDetails, opt => opt.Ignore());
        }
    }
}
