using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public class Equipment : EntityAudit
{
    public Guid EquipmentId { get; set; }

    public string? EquipmentCode { get; set; }

    public string? EquipmentName { get; set; }

    public DateTime? PurchaseDate { get; set; }

    public int? WarrantyPeriod { get; set; }

    public string? Status { get; set; }

    public double? Cost { get; set; }

    public int? Quantity { get; set; }

    public string? Specifications { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public Guid ProductId { get; set; }

    public virtual ICollection<CoopEquipment> CoopEquipments { get; set; } = new List<CoopEquipment>();

    public virtual Product? Product { get; set; }
}
