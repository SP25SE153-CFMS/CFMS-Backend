using AutoMapper;
using CFMS.Application.Features.ShiftFeat.Create;
using CFMS.Application.Features.SupplierFeat.Create;
using CFMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Mappings
{
    public class ShiftProfile : Profile
    {
        public ShiftProfile()
        {
            CreateMap<CreateShiftCommand, Shift>();
        }
    }
}
