using System;
using System.Collections.Generic;

namespace CFMS.Infrastructure.Persistence;

public partial class Performance
{
    public Guid PerId { get; set; }

    public Guid? UserId { get; set; }

    public int? TotalTask { get; set; }

    public int? CompletedTask { get; set; }

    public int? DelayTask { get; set; }

    public double? CompletionRate { get; set; }

    public double? PerformanceRating { get; set; }

    public string? Note { get; set; }

    public string? RangeTime { get; set; }

    public virtual User? User { get; set; }
}
