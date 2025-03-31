using AutoMapper;
using CFMS.Application.Features.RequestFeat.Create;
using CFMS.Domain.Entities;

public class RequestProfile : Profile
{
    public RequestProfile()
    {
        CreateMap<CreateRequestCommand, Request>()
            .ForMember(dest => dest.RequestId, opt => opt.Ignore())
            .ForMember(dest => dest.InventoryRequests, opt => opt.Ignore()) 
            .ForMember(dest => dest.TaskRequests, opt => opt.Ignore());
    }
}
