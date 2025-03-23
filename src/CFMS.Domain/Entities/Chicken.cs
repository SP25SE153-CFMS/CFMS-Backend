using System;
using System.Collections.Generic;

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

    public Guid? ChickenBatchId { get; set; }

    public virtual ChickenBatch? ChickenBatch { get; set; }

    public virtual ICollection<ChickenDetail> ChickenDetails { get; set; } = new List<ChickenDetail>();

    public virtual SystemConfig ChickenNavigation { get; set; } = null!;

    public virtual ICollection<ChickenNutrition> ChickenNutritions { get; set; } = new List<ChickenNutrition>();

    public virtual SubCategory? ChickenType { get; set; }

    public virtual ICollection<EvaluatedTarget> EvaluatedTargets { get; set; } = new List<EvaluatedTarget>();
}
