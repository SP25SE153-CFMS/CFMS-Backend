using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public class QuantityLog : EntityAudit
{
    public Guid QLogId { get; set; }

    public DateTime? LogDate { get; set; }

    public string? Notes { get; set; }

    public int? Quantity { get; set; }

    public string? Img { get; set; }

    public string? LogType { get; set; }

    public Guid? FlockId { get; set; }

    public Guid? ReasonId { get; set; }

    public virtual Flock? Flock { get; set; }

    public virtual SubCategory? Reason { get; set; }
}
