using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public  class EvaluationResultDetail : EntityAudit
{
    public Guid EvaluationResultDetailId { get; set; }

    public Guid? EvaluationResultId { get; set; }

    public string? ActualValue { get; set; }

    public int? IsPass { get; set; }

    public virtual EvaluationResult? EvaluationResult { get; set; }
}
