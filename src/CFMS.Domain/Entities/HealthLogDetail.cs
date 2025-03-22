using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class HealthLogDetail : EntityAudit
{
    public Guid HealthLogDetailId { get; set; }

    public Guid? HealthLogId { get; set; }

    public Guid? CriteriaId { get; set; }

    public string? Result { get; set; }

    public virtual SubCategory? Criteria { get; set; }

    public virtual HealthLog? HealthLog { get; set; }
}
