using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public class Salary : EntityAudit
{
    public Guid SalaryId { get; set; }

    public Guid? UserId { get; set; }

    public double? BasicSalary { get; set; }

    public double? Bonus { get; set; }

    public double? Deduction { get; set; }

    public double? Final { get; set; }

    public string? Status { get; set; }

    public int? TotalHoursWorked { get; set; }

    public int? OverTimeHours { get; set; }

    public virtual User? User { get; set; }
}
