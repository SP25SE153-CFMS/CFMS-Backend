using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class Dailytask
{
    public Guid Dtaskid { get; set; }

    public Guid Taskid { get; set; }

    public DateOnly Taskdate { get; set; }

    public string? Status { get; set; }

    public string? Note { get; set; }

    public int? Priority { get; set; }

    public virtual Task Task { get; set; } = null!;
}
