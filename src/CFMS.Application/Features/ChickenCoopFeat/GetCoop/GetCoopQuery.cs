using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;

namespace CFMS.Application.Features.ChickenCoopFeat.GetCoop
{
    public class GetCoopQuery : IRequest<BaseResponse<ChickenCoop>>
    {
        public GetCoopQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
