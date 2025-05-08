using AutoMapper;
using CFMS.Application.Features.SystemConfigFeat.Create;
using CFMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Mappings
{
    class SystemConfigProfile : Profile
    {
        public SystemConfigProfile()
        {
            CreateMap<CreateConfigCommand, SystemConfig>();
        }
    }
}
