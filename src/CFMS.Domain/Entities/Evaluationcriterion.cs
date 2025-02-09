using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class Evaluationcriterion
{
    public Guid Criteriaid { get; set; }

    public string Criterianame { get; set; } = null!;

    public string? Description { get; set; }

    public double? Minvalue { get; set; }

    public double? Maxvalue { get; set; }

    public string? Unit { get; set; }

    public string Tasktype { get; set; } = null!;

    public List<Evaluationsummary> EvaluationSummaries { get; set; } = new List<Evaluationsummary>();
}
