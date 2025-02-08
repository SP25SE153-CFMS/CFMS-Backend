using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class TimeKeepingType
{
    public Guid Timetypeid { get; set; }

    public string Typename { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Unitsalary { get; set; }

    public virtual ICollection<TimeKeeping> TimeKeepings { get; set; } = new List<TimeKeeping>();
}
