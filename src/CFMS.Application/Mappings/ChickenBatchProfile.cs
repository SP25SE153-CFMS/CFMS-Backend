using AutoMapper;
using CFMS.Application.Features.ChickenBatchFeat.Create;
using CFMS.Domain.Entities;

namespace CFMS.Application.Mappings
{
    public class ChickenBatchProfile : Profile
    {
        public ChickenBatchProfile()
        {
            CreateMap<CreateChickenBatchCommand, ChickenBatch>();
        }
    }
}
