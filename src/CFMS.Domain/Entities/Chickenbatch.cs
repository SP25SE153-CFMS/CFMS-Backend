using System;
using System.Collections.Generic;

namespace CFMS.Infrastructure.Persistence;

public partial class ChickenBatch
{
    public Guid ChickenBatchId { get; set; }

    public Guid? ChickenCoopId { get; set; }

    public string? Name { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? Note { get; set; }

    public int? Status { get; set; }

    public virtual ChickenCoop? ChickenCoop { get; set; }

    public virtual ICollection<Flock> Flocks { get; set; } = new List<Flock>();
}
