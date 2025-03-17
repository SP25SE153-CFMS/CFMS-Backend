using AutoMapper;
using CFMS.Application.Features.ChickenCoopFeat.Create;
using CFMS.Domain.Entities;

namespace CFMS.Application.Mappings
{
    public class ChickenCoopProfile : Profile
    {
        public ChickenCoopProfile()
        {
            CreateMap<CreateCoopCommand, ChickenCoop>();
        }
    }
}
