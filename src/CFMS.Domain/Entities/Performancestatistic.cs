using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class Performancestatistic
{
    public Guid Perstaid { get; set; }

    public Guid Userid { get; set; }

    public int? Totaltask { get; set; }

    public int? Completedtask { get; set; }

    public int? Delaytask { get; set; }

    public decimal? Completionrate { get; set; }

    public string? Performancerating { get; set; }

    public string? Note { get; set; }

    public DateOnly Rangetime { get; set; }

    public virtual User User { get; set; } = null!;
}
