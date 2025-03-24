using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class EvaluatedTarget : EntityAudit
{
    public Guid EvaluatedTargetId { get; set; }

    public Guid? TargetTypeId { get; set; }

    public Guid? TargetId { get; set; }

    public virtual ICollection<EvaluationResult> EvaluationResults { get; set; } = new List<EvaluationResult>();

    public virtual Chicken? Target { get; set; }

    public virtual Task? TargetNavigation { get; set; }

    public virtual SubCategory? TargetType { get; set; }
}
