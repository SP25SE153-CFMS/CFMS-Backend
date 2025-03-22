using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class FarmEmployee : EntityAudit
{
    public Guid FarmEmployeeId { get; set; }

    public Guid? FarmId { get; set; }

    public Guid? UserId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int? Status { get; set; }

    public int? FarmRole { get; set; }

    public virtual Farm? Farm { get; set; }

    public virtual User? User { get; set; }
}
