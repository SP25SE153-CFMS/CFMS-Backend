using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CFMS.Domain.Entities;

public partial class TaskLocation
{
    public Guid TaskLocationId { get; set; }

    public Guid? TaskId { get; set; }

    public string? LocationType { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Guid? CoopId { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Guid? WareId { get; set; }

    public virtual ChickenCoop Location { get; set; } = null!;

    public virtual Warehouse LocationNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual Task? Task { get; set; }
}
