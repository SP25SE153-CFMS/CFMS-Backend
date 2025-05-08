using AutoMapper;
using CFMS.Application.Features.FoodFeat.Create;
using CFMS.Application.Features.MedicineFeat.Create;
using CFMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Mappings
{
    public class MedicineProfile : Profile
    {
        public MedicineProfile()
        {
            CreateMap<CreateMedicineCommand, Medicine>();
        }
    }
}
