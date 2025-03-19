using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public class TemplateCriterion : EntityAudit
{
    public Guid TemplateCriteriaId { get; set; }

    public string? TemplateName { get; set; }

    public Guid? EvaluationTemplateId { get; set; }

    public Guid? CriteriaId { get; set; }

    public string? ExpectedCondition { get; set; }

    public string? ExpectedUnit { get; set; }

    public string? ExpectedValue { get; set; }

    public virtual SubCategory? Criteria { get; set; }

    public virtual EvaluationTemplate? EvaluationTemplate { get; set; }
}
