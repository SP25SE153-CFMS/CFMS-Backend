using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class Salary
{
    public Guid Salaryid { get; set; }

    public Guid Userid { get; set; }

    public decimal Basicsalary { get; set; }

    public decimal? Bonus { get; set; }

    public decimal? Deduction { get; set; }

    public decimal? Totalhoursworked { get; set; }

    public decimal? Overtimehours { get; set; }

    public decimal? Finalsalary { get; set; }

    public string? Status { get; set; }

    public DateOnly Salarymonth { get; set; }

    public virtual User User { get; set; } = null!;
}
