using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;

namespace CFMS.Application.Features.FarmFeat.GetFarm
{
    public class GetFarmQuery : IRequest<BaseResponse<Farm>>
    {
        public GetFarmQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
