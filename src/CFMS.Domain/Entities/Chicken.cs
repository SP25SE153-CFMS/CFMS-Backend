using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public class Chicken : EntityAudit
{
    public Guid ChickenId { get; set; }

    public string? ChickenCode { get; set; }

    public string? ChickenName { get; set; }

    public int? TotalQuantity { get; set; }

    public string? Description { get; set; }

    public int? Status { get; set; }

    public Guid? PurposeId { get; set; }

    public Guid? ChickenBatchId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual ChickenBatch? ChickenBatch { get; set; }

    public virtual ICollection<ChickenDetail> ChickenDetails { get; set; } = new List<ChickenDetail>();

    public virtual ICollection<EvaluatedTarget> EvaluatedTargets { get; set; } = new List<EvaluatedTarget>();

    public virtual SubCategory? Purpose { get; set; }
}
