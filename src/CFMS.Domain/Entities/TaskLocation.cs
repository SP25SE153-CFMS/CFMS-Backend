using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public class TaskLocation : EntityAudit
{
    public Guid TaskLocationId { get; set; }

    public Guid? TaskId { get; set; }

    public Guid? LocationTypeId { get; set; }

    public Guid LocationId { get; set; }

    public virtual SubCategory? LocationType { get; set; }

    public virtual Task? Task { get; set; }
}
