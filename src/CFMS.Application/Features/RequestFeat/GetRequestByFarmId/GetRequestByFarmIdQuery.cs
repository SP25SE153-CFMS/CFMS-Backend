using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;

namespace CFMS.Application.Features.RequestFeat.GetRequestByFarmId
{
    public class GetRequestByFarmIdQuery : IRequest<BaseResponse<IEnumerable<Request>>>
    {
        public GetRequestByFarmIdQuery(Guid farmId)
        {
            FarmId = farmId;
        }

        public Guid FarmId { get; set; }
    }
}
