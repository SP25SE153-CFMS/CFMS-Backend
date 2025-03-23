using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class ChickenCoop : EntityAudit
{
    public Guid ChickenCoopId { get; set; }

    public string? ChickenCoopCode { get; set; }

    public string? ChickenCoopName { get; set; }

    public int? MaxQuantity { get; set; }

    public int? Area { get; set; }

    public decimal? Density { get; set; }

    public Guid? DensityUnitId { get; set; }

    public int? CurrentQuantity { get; set; }

    public string? Description { get; set; }

    public int? Status { get; set; }

    public Guid? PurposeId { get; set; }

    public Guid? BreedingAreaId { get; set; }

    public virtual BreedingArea? BreedingArea { get; set; }

    public virtual ICollection<ChickenBatch> ChickenBatches { get; set; } = new List<ChickenBatch>();

    public virtual SystemConfig ChickenCoopNavigation { get; set; } = null!;

    public virtual ICollection<CoopEquipment> CoopEquipments { get; set; } = new List<CoopEquipment>();

    public virtual SubCategory? DensityUnit { get; set; }

    public virtual SubCategory? Purpose { get; set; }

    public virtual ICollection<TaskLocation> TaskLocations { get; set; } = new List<TaskLocation>();

    public virtual ICollection<TaskLog> TaskLogs { get; set; } = new List<TaskLog>();
}
