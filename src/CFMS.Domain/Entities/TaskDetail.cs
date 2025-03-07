using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class TaskDetail
{
    public Guid TaskDetailId { get; set; }

    public Guid? TaskLogId { get; set; }

    public Guid? TypeProductId { get; set; }

    public int? Quantity { get; set; }

    public string? Note { get; set; }

    public virtual TaskLog? TaskLog { get; set; }

    public virtual SubCategory? TypeProduct { get; set; }
}
