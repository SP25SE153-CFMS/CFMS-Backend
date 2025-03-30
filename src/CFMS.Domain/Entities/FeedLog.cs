using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CFMS.Domain.Entities;

public partial class FeedLog : EntityAudit
{
    public Guid FeedLogId { get; set; }

    public Guid? ChickenBatchId { get; set; }

    public DateTime? FeedingDate { get; set; }

    public decimal? ActualFeedAmount { get; set; }

    public Guid? UnitId { get; set; }

    public Guid? TaskId { get; set; }

    public string? Note { get; set; }

    [JsonIgnore]
    public virtual ChickenBatch? ChickenBatch { get; set; }

    [JsonIgnore]
    public virtual Task? Task { get; set; }

    [JsonIgnore]
    public virtual SubCategory? Unit { get; set; }
}
