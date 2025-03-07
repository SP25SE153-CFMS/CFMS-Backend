using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class DailyTask
{
    public Guid DTaskId { get; set; }

    public Guid? TaskId { get; set; }

    public DateOnly? TaskDate { get; set; }

    public string? Description { get; set; }

    public Guid? ItemId { get; set; }

    public virtual Task? Task { get; set; }
}
