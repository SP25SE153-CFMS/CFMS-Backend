using AutoMapper;
using CFMS.Application.Features.EquipmentFeat.Create;
using CFMS.Application.Features.MedicineFeat.Create;
using CFMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
