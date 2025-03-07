using System;
using System.Collections.Generic;

namespace CFMS.Infrastructure.Persistence;

public partial class FarmEmployee
{
    public Guid FarmEmployeeId { get; set; }

    public Guid? FarmId { get; set; }

    public Guid? EmployeeId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int? Status { get; set; }

    public string? RoleName { get; set; }

    public virtual User? Employee { get; set; }

    public virtual Farm? Farm { get; set; }
}
