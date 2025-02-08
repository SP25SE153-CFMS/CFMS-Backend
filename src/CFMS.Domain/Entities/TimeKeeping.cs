using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class TimeKeeping
{
    public Guid Timekeepingid { get; set; }

    public DateOnly Workdate { get; set; }

    public TimeOnly Endtime { get; set; }

    public Guid Userid { get; set; }

    public Guid Timekeepingtype { get; set; }

    public virtual TimeKeepingType TimekeepingtypeNavigation { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
