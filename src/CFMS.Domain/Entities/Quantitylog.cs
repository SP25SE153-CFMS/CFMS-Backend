using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CFMS.Domain.Entities;

public partial class QuantityLog : EntityAudit
{
    public Guid QuantityLogId { get; set; }

    public Guid? ChickenBatchId { get; set; }

    public DateTime? LogDate { get; set; }

    public string? Notes { get; set; }

    public int? Quantity { get; set; }

    public int? LogType { get; set; }

    [JsonIgnore]
    public virtual ChickenBatch? ChickenBatch { get; set; }
}
