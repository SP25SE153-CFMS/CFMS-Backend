using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.ChickenCoopFeat.AddCoopEquipment
{
    public class AddCoopEquipmentCommand : IRequest<BaseResponse<bool>>
    {
        public AddCoopEquipmentCommand(Guid? chickenCoopId, Guid? equipmentId, int? quantity, DateTime? assignedDate, int maintenanceInterval, int? status, string? note)
        {
            ChickenCoopId = chickenCoopId;
            EquipmentId = equipmentId;
            Quantity = quantity;
            AssignedDate = assignedDate;
            MaintenanceInterval = maintenanceInterval;
            Status = status;
            Note = note;
        }

        public Guid? ChickenCoopId { get; set; }

        public Guid? EquipmentId { get; set; }

        public int? Quantity { get; set; }

        public DateTime? AssignedDate { get; set; }

        //public DateTime? LastMaintenanceDate { get; set; }

        //public DateTime? NextMaintenanceDate { get; set; }

        public int MaintenanceInterval { get; set; }

        public int? Status { get; set; }

        public string? Note { get; set; }
    }
}
