using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;

namespace CFMS.Application.Features.ChickenCoopFeat.GetCoops
{
    public class GetCoopsQuery : IRequest<BaseResponse<IEnumerable<ChickenCoop>>>
    {
        public GetCoopsQuery(Guid breedingAreaId)
        {
            BreedingAreaId = breedingAreaId;
        }

        public Guid BreedingAreaId { get; set; }
    }
}
