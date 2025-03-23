using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class TaskRequest
{
    public Guid TaskRequestId { get; set; }

    public Guid? RequestId { get; set; }

    public Guid? TaskTypeId { get; set; }

    public int? Priority { get; set; }

    public string? Description { get; set; }

    public virtual Request? Request { get; set; }

    public virtual SubCategory? TaskType { get; set; }
}
