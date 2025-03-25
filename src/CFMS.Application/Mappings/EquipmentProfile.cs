using AutoMapper;
using CFMS.Application.Features.EquipmentFeat.Create;
using CFMS.Domain.Entities;

namespace CFMS.Application.Mappings
{
    public class EquipmentProfile : Profile
    {
        public EquipmentProfile()
        {
            CreateMap<CreateEquipmentCommand, Equipment>();
        }
    }
}
