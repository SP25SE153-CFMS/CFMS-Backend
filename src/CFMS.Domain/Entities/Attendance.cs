using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public class Attendance : EntityAudit
{
    public Guid AttendanceId { get; set; }

    public Guid? UserId { get; set; }

    public DateOnly? WorkDate { get; set; }

    public TimeOnly? CheckIn { get; set; }

    public TimeOnly? CheckOut { get; set; }

    public virtual User? User { get; set; }
}
