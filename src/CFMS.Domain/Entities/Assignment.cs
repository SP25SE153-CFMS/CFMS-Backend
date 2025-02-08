using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class Assignment
{
    public Guid Assignmentid { get; set; }

    public Guid Taskid { get; set; }

    public Guid Userid { get; set; }

    public DateTime Assigneddate { get; set; }

    public DateTime? Completeddate { get; set; }

    public string? Status { get; set; }

    public string? Note { get; set; }

    public virtual Task Task { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
