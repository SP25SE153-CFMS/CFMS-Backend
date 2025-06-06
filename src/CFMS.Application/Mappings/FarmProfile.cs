﻿using AutoMapper;
using CFMS.Application.Features.FarmFeat.Create;
using CFMS.Domain.Entities;

namespace CFMS.Application.Mappings
{
    public class FarmProfile : Profile
    {
        public FarmProfile()
        {
            CreateMap<CreateFarmCommand, Farm>();
        }
    }
}
