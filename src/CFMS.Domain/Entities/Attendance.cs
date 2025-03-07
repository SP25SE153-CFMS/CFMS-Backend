using System;
using System.Collections.Generic;

namespace CFMS.Infrastructure.Persistence;

public partial class Attendance
{
    public Guid AttendanceId { get; set; }

    public Guid? UserId { get; set; }

    public DateOnly? WorkDate { get; set; }

    public TimeOnly? CheckIn { get; set; }

    public TimeOnly? CheckOut { get; set; }

    public virtual User? User { get; set; }
}
