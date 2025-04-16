using AutoMapper;
using CFMS.Application.Features.FoodFeat.Create;
using CFMS.Application.Features.HarvestProductFeat.Create;
using CFMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Mappings
{
   public class HarvestProductProfile : Profile
    {
        public HarvestProductProfile()
        {
            CreateMap<CreateHarvestProductCommand, HarvestProduct>();
        }
    }
}
