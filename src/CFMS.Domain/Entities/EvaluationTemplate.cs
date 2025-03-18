using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public  class EvaluationTemplate : EntityAudit
{
    public Guid EvaluationTemplateId { get; set; }

    public string? TemplateName { get; set; }

    public Guid? TemplateTypeId { get; set; }

    public virtual ICollection<EvaluationResult> EvaluationResults { get; set; } = new List<EvaluationResult>();

    public virtual ICollection<TemplateCriterion> TemplateCriteria { get; set; } = new List<TemplateCriterion>();

    public virtual SubCategory? TemplateType { get; set; }
}
