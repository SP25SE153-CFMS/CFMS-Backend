using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;

namespace CFMS.Application.Features.BreedingAreaFeat.GetBreedingAreas
{
    public class GetBreedingAreasQuery : IRequest<BaseResponse<IEnumerable<BreedingArea>>>
    {
    }
}
