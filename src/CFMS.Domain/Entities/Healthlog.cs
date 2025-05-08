using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CFMS.Domain.Entities;

public partial class HealthLog : EntityAudit
{
    public Guid HealthLogId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? Notes { get; set; }

    public Guid? ChickenBatchId { get; set; }

    public Guid? TaskId { get; set; }

    public DateTime? CheckedAt { get; set; }

    public string? Location { get; set; }

    [JsonIgnore]
    public virtual ChickenBatch? ChickenBatch { get; set; }

    public virtual ICollection<HealthLogDetail> HealthLogDetails { get; set; } = new List<HealthLogDetail>();

    [JsonIgnore]
    public virtual Task? Task { get; set; }
}
