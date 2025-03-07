using System;
using System.Collections.Generic;

namespace CFMS.Infrastructure.Persistence;

public partial class HealthLogDetail
{
    public Guid LogDetailId { get; set; }

    public Guid? HLogId { get; set; }

    public Guid? CriteriaId { get; set; }

    public string? Result { get; set; }

    public Guid? CheckedBy { get; set; }

    public DateTime? CheckedAt { get; set; }

    public virtual User? CheckedByNavigation { get; set; }

    public virtual SubCategory? Criteria { get; set; }

    public virtual HealthLog? HLog { get; set; }
}
