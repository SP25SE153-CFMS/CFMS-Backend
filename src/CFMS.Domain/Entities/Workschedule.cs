using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class Workschedule
{
    public Guid Workscheduleid { get; set; }

    public Guid Userid { get; set; }

    public Guid Taskid { get; set; }

    public DateOnly Workdate { get; set; }

    public string? Status { get; set; }

    public string? Colorcode { get; set; }

    public virtual Task Task { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
