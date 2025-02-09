using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class Evaluationsummary
{
    public Guid Summaryid { get; set; }

    public Guid Taskid { get; set; }

    public string Tasktype { get; set; } = null!;

    public int Totalcriteria { get; set; }

    public int Passedcriteria { get; set; }

    public int Failedcriteria { get; set; }

    public bool Overallresult { get; set; }

    public Guid Criteriaid { get; set; }

    public Guid Userid { get; set; }

    public string? Description { get; set; }

    public DateTime? Evaluationdate { get; set; }

    public List<User> Users { get; set; } = new List<User>();

    public List<Evaluationcriterion> EvaluationCriterions { get; set; } = new List<Evaluationcriterion>();
}
