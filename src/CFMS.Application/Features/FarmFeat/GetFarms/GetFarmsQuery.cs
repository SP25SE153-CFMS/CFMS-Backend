using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;

namespace CFMS.Application.Features.FarmFeat.GetFarms
{
    public class GetFarmsQuery : IRequest<BaseResponse<IEnumerable<Farm>>>
    {
        public GetFarmsQuery() { }
    }
}
