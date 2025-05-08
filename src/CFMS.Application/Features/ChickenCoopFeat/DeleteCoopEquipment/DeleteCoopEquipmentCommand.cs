using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.ChickenCoopFeat.DeleteCoopEquipment
{
    public class DeleteCoopEquipmentCommand : IRequest<BaseResponse<bool>>
    {
        public DeleteCoopEquipmentCommand(Guid coopId, Guid coopEquipId)
        {
            CoopId = coopId;
            CoopEquipId = coopEquipId;
        }

        public Guid CoopId { get; set; }

        public Guid CoopEquipId { get; set; }
    }
}
