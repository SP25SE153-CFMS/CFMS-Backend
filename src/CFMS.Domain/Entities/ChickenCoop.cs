using System;
using System.Collections.Generic;

namespace CFMS.Infrastructure.Persistence;

public partial class ChickenCoop
{
    public Guid ChickenCoopId { get; set; }

    public string? ChickenCoopCode { get; set; }

    public string? ChickenCoopName { get; set; }

    public int? Capacity { get; set; }

    public int? Area { get; set; }

    public string? Status { get; set; }

    public string? Description { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? PurposeId { get; set; }

    public Guid? BreedingAreaId { get; set; }

    public virtual BreedingArea? BreedingArea { get; set; }

    public virtual ICollection<ChickenBatch> ChickenBatches { get; set; } = new List<ChickenBatch>();

    public virtual ICollection<CoopEquipment> CoopEquipments { get; set; } = new List<CoopEquipment>();

    public virtual ICollection<HarvestLog> HarvestLogs { get; set; } = new List<HarvestLog>();

    public virtual SubCategory? Purpose { get; set; }

    public virtual ICollection<TaskLog> TaskLogs { get; set; } = new List<TaskLog>();
}
