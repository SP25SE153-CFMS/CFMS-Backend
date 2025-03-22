using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class TaskHarvest : EntityAudit
{
    public Guid TaskHarvestId { get; set; }

    public Guid TaskId { get; set; }

    public Guid? HarvestTypeId { get; set; }

    public int? Quantity { get; set; }

    public Guid? UnitId { get; set; }

    public string? Quality { get; set; }

    public virtual SubCategory? HarvestType { get; set; }

    public virtual Task Task { get; set; } = null!;
}
