using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.ChickenCoopFeat.AddCoopEquipment
{
    public class AddCoopEquipmentCommand : IRequest<BaseResponse<bool>>
    {
        public Guid? ChickenCoopId { get; set; }

        public Guid? EquipmentId { get; set; }

        public int? Quantity { get; set; }

        public DateTime? AssignedDate { get; set; }

        public DateTime? LastMaintenanceDate { get; set; }

        public DateTime? NextMaintenanceDate { get; set; }

        public int MaintenanceInterval { get; set; }

        public int? Status { get; set; }

        public string? Note { get; set; }
    }
}
