using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CFMS.Domain.Entities;

public partial class TaskHarvest
{
    public Guid TaskHarvestId { get; set; }

    public Guid TaskId { get; set; }

    public Guid? HarvestProductId { get; set; }

    public Guid? HarvestTypeId { get; set; }

    public decimal? Quantity { get; set; }

    //public Guid? UnitId { get; set; }

    //public string? Quality { get; set; }

    public virtual SubCategory? HarvestType { get; set; }

    [JsonIgnore]
    public virtual Task Task { get; set; } = null!;
}
