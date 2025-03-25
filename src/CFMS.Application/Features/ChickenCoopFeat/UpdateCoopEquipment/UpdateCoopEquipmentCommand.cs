using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.ChickenCoopFeat.UpdateCoopEquipment
{
    public class UpdateCoopEquipmentCommand : IRequest<BaseResponse<bool>>
    {
        public UpdateCoopEquipmentCommand(Guid coopEquipmentId, Guid? chickenCoopId, Guid? equipmentId, int? quantity, DateTime? assignedDate, DateTime? lastMaintenanceDate, DateTime? nextMaintenanceDate, int maintenanceInterval, int? status, string? note)
        {
            CoopEquipmentId = coopEquipmentId;
            ChickenCoopId = chickenCoopId;
            EquipmentId = equipmentId;
            Quantity = quantity;
            AssignedDate = assignedDate;
            LastMaintenanceDate = lastMaintenanceDate;
            NextMaintenanceDate = nextMaintenanceDate;
            MaintenanceInterval = maintenanceInterval;
            Status = status;
            Note = note;
        }

        public Guid CoopEquipmentId { get; set; }

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
