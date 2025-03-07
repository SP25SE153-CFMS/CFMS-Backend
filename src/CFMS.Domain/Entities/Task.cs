using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class Task
{
    public Guid TaskId { get; set; }

    public string? TaskName { get; set; }

    public string? TaskType { get; set; }

    public string? Status { get; set; }

    public string? Location { get; set; }

    public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

    public virtual ICollection<DailyTask> DailyTasks { get; set; } = new List<DailyTask>();

    public virtual ICollection<HarvestTask> HarvestTasks { get; set; } = new List<HarvestTask>();

    public virtual ICollection<TaskEvaluation> TaskEvaluations { get; set; } = new List<TaskEvaluation>();
}
