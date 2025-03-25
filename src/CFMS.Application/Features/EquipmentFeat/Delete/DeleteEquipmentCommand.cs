using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.EquipmentFeat.Delete
{
    public class DeleteEquipmentCommand : IRequest<BaseResponse<bool>>
    {
        public DeleteEquipmentCommand(Guid equipmentId)
        {
            EquipmentId = equipmentId;
        }

        public Guid EquipmentId { get; set; }
    }
}
