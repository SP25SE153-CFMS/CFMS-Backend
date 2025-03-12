using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;

namespace CFMS.Application.Features.BreedingAreaFeat.GetBreedingArea
{
    public class GetBreedingAreaQuery : IRequest<BaseResponse<BreedingArea>>
    {
        public GetBreedingAreaQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
