using AutoMapper;
using CFMS.Application.Features.GrowthStageFeat.Create;
using CFMS.Domain.Entities;

namespace CFMS.Application.Mappings
{
    public class GrowthStageProfile : Profile
    {
        public GrowthStageProfile()
        {
            CreateMap<CreateStageCommand, GrowthStage>();
        }
    }
}
