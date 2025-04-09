using AutoMapper;
using CFMS.Application.Features.ChickenBatchFeat.AddHealthLog;
using CFMS.Domain.Entities;

namespace CFMS.Application.Mappings
{
    public class HealthLogProfile : Profile
    {
        public HealthLogProfile()
        {
            CreateMap<AddHealthLogCommand, HealthLog>()
                .ForMember(dest => dest.HealthLogDetails, opt => opt.Ignore());
        }
    }
}
