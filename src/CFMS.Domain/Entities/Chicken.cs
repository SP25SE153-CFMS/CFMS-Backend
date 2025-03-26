using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CFMS.Domain.Entities;

public partial class Chicken : EntityAudit
{
    public Guid ChickenId { get; set; }

    public string? ChickenCode { get; set; }

    public string? ChickenName { get; set; }

    public int? TotalQuantity { get; set; }

    public string? Description { get; set; }

    public int? Status { get; set; }

    public Guid? ChickenTypeId { get; set; }

    [JsonIgnore]
    public virtual ICollection<ChickenBatch> ChickenBatches { get; set; } = new List<ChickenBatch>();

    public virtual ICollection<ChickenDetail> ChickenDetails { get; set; } = new List<ChickenDetail>();

    public virtual SystemConfig ChickenNavigation { get; set; } = null!;

    public virtual SubCategory? ChickenType { get; set; }

    public virtual ICollection<EvaluatedTarget> EvaluatedTargets { get; set; } = new List<EvaluatedTarget>();
}
