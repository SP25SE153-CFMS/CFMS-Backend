using CFMS.Domain.Enums.Roles;
using CFMS.Domain.Enums.Status;
using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public class FarmEmployee : EntityAudit
{
    public Guid FarmEmployeeId { get; set; }

    public Guid? FarmId { get; set; }

    public Guid? EmployeeId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public FarmEmployeeStatus? Status { get; set; }

    public FarmRole? FarmRole { get; set; }

    public virtual User? Employee { get; set; }

    public virtual Farm? Farm { get; set; }
}
