using AutoMapper;
using CFMS.Application.Commands.FarmFeat.Create;
using CFMS.Domain.Entities;

namespace CFMS.Application.Mappings
{
    public class FarmProfile : Profile
    {
        public FarmProfile()
        {
            // Mapping from CreateFarmCommand to Farm entity
            CreateMap<CreateFarmCommand, Farm>();
        }
    }
}
