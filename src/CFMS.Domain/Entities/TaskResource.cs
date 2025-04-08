using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CFMS.Domain.Entities;

public partial class TaskResource : EntityAudit
{
    public Guid TaskResourceId { get; set; }

    public Guid TaskId { get; set; }

    public Guid ResourceId { get; set; }

    public Guid? ResourceTypeId { get; set; }

    public int? Quantity { get; set; }

    public Guid? UnitId { get; set; }

    public virtual SubCategory? ResourceType { get; set; }

    public virtual SubCategory? Unit { get; set; }

    public virtual Resource? Resource { get; set; }

    [JsonIgnore]
    public virtual Task Task { get; set; } = null!;
}
