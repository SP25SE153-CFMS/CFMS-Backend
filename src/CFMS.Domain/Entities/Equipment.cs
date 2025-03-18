using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public class Equipment : EntityAudit
{
    public Guid EquipmentId { get; set; }

    public string? EquipmentCode { get; set; }

    public string? EquipmentName { get; set; }

    public string? Material { get; set; }

    public string? Usage { get; set; }

    public int? Warranty { get; set; }

    public double? Size { get; set; }

    public double? Weight { get; set; }

    public DateTime? PurchaseDate { get; set; }

    public virtual ICollection<CoopEquipment> CoopEquipments { get; set; } = new List<CoopEquipment>();

    public virtual Resource EquipmentNavigation { get; set; } = null!;
}
