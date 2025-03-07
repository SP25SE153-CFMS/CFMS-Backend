using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class TaskEvaluation
{
    public Guid TaskEvalId { get; set; }

    public Guid? CategoryId { get; set; }

    public Guid? TaskId { get; set; }

    public string? Description { get; set; }

    public int? TotalCriteria { get; set; }

    public int? PassedCriteria { get; set; }

    public int? FailedCriteria { get; set; }

    public string? OverallResult { get; set; }

    public string? TaskType { get; set; }

    public string? StaffName { get; set; }

    public virtual SubCategory? Category { get; set; }

    public virtual Task? Task { get; set; }
}
