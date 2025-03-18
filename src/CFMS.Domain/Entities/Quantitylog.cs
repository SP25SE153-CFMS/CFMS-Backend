using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public class QuantityLog : EntityAudit
{
    public Guid QuantityLogId { get; set; }

    public Guid? ChickenBatchId { get; set; }

    public DateTime? LogDate { get; set; }

    public string? Notes { get; set; }

    public int? Quantity { get; set; }

    public string? Img { get; set; }

    public int? LogType { get; set; }

    public virtual ChickenBatch? ChickenBatch { get; set; }
}
