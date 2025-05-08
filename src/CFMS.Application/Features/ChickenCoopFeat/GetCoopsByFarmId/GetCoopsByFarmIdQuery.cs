using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;

namespace CFMS.Application.Features.ChickenCoopFeat.GetCoopsByFarmId
{
    public class GetCoopsByFarmIdQuery : IRequest<BaseResponse<IEnumerable<ChickenCoop>>>
    {
        public GetCoopsByFarmIdQuery(Guid farmId)
        {
            FarmId = farmId;
        }

        public Guid FarmId { get; set; }
    }
}
