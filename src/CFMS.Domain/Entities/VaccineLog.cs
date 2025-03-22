using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class VaccineLog : EntityAudit
{
    public Guid VaccineLogId { get; set; }

    public string? Notes { get; set; }

    public int? Status { get; set; }

    public string? Reaction { get; set; }

    public Guid? ChickenBatchId { get; set; }

    public Guid? TaskId { get; set; }

    public virtual ChickenBatch? ChickenBatch { get; set; }

    public virtual Task? Task { get; set; }
}
