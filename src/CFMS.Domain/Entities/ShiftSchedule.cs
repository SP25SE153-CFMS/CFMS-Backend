using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class ShiftSchedule
{
    public Guid ShiftScheduleId { get; set; }

    public Guid? ShiftId { get; set; }

    public DateOnly? Date { get; set; }

    public Guid? TaskId { get; set; }

    public virtual Task? Task { get; set; }

    public virtual Shift? Shift { get; set; }
}
