using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public class EvaluationResult : EntityAudit
{
    public Guid EvaluationResultId { get; set; }

    public Guid? EvaluationTemplateId { get; set; }

    public Guid? EvaluatedTargetId { get; set; }

    public DateTime? EvaluatedDate { get; set; }

    public virtual EvaluatedTarget? EvaluatedTarget { get; set; }

    public virtual ICollection<EvaluationResultDetail> EvaluationResultDetails { get; set; } = new List<EvaluationResultDetail>();

    public virtual EvaluationTemplate? EvaluationTemplate { get; set; }
}
