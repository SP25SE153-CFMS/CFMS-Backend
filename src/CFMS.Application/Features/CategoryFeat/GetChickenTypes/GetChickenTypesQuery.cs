using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;

namespace CFMS.Application.Features.CategoryFeat.GetChickenTypes
{
    public class GetChickenTypesQuery : IRequest<BaseResponse<IEnumerable<SubCategory>>>
    {
        public GetChickenTypesQuery(Guid farmId)
        {
            FarmId = farmId;
        }

        public Guid FarmId { get; set; }
    }
}
