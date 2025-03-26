using AutoMapper;
using CFMS.Application.Features.FeedLogFeat.Create;
using CFMS.Domain.Entities;

namespace CFMS.Application.Mappings
{
    public class FeedLogProfile : Profile
    {
        public FeedLogProfile()
        {
            CreateMap<CreateFeedLogCommand, FeedLog>();
        }
    }
}
