using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;

namespace CFMS.Application.Features.EquipmentFeat.GetEquipment
{
    public class GetEquipmentQuery : IRequest<BaseResponse<Equipment>>
    {
        public GetEquipmentQuery(Guid equipmentId)
        {
            EquipmentId = equipmentId;
        }

        public Guid EquipmentId { get; set; }
    }
}
