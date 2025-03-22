using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class TaskLocation : EntityAudit
{
    public Guid TaskLocationId { get; set; }

    public Guid? TaskId { get; set; }

    public string? LocationType { get; set; }

    public Guid LocationId { get; set; }

    public virtual ChickenCoop Location { get; set; } = null!;

    public virtual Warehouse LocationNavigation { get; set; } = null!;

    public virtual Task? Task { get; set; }
}
