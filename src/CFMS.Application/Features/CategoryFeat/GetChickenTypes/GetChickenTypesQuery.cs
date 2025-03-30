using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;

namespace CFMS.Application.Features.CategoryFeat.GetChickenTypes
{
    public class GetChickenTypesQuery : IRequest<BaseResponse<IEnumerable<SubCategory>>>
    {
    }
}
