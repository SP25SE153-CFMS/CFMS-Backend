using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CFMS.Domain.Entities;

public partial class Equipment : EntityAudit
{
    public Guid EquipmentId { get; set; }

    public string? EquipmentCode { get; set; }

    public string? EquipmentName { get; set; }

    public Guid? MaterialId { get; set; }

    public string? Usage { get; set; }

    public int? Warranty { get; set; }

    public decimal? Size { get; set; }

    public Guid? SizeUnitId { get; set; }

    public decimal? Weight { get; set; }

    public Guid? WeightUnitId { get; set; }

    public DateTime? PurchaseDate { get; set; }

    public virtual ICollection<CoopEquipment> CoopEquipments { get; set; } = new List<CoopEquipment>();

    public virtual SubCategory? SizeUnit { get; set; }

    public virtual SubCategory? Material { get; set; }

    public virtual SubCategory? WeightUnit { get; set; }
}
