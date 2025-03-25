using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;

namespace CFMS.Application.Features.EquipmentFeat.GetEquipments
{
    public class GetEquipmentsQuery : IRequest<BaseResponse<IEnumerable<Equipment>>>
    {
    }
}
