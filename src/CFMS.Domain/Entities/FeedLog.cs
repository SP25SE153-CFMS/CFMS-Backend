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

    public Guid? TaskId { get; set; }

    public string? Note { get; set; }

    public Guid? ResourceId { get; set; }

    [JsonIgnore]
    public virtual ChickenBatch? ChickenBatch { get; set; }

    [JsonIgnore]
    public virtual Task? Task { get; set; }

    public virtual Resource? Resource { get; set; }
}
