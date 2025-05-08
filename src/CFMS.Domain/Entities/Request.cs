using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class Request : EntityAudit
{
    public Guid RequestId { get; set; }

    public Guid? RequestTypeId { get; set; }

    public int? Status { get; set; }

    public Guid? ApprovedById { get; set; }

    public DateTime? ApprovedAt { get; set; }

    public Guid? FarmId { get; set; }

    public virtual User? ApprovedBy { get; set; }

    public virtual ICollection<InventoryRequest> InventoryRequests { get; set; } = new List<InventoryRequest>();

    public virtual SubCategory? RequestType { get; set; }

    public virtual ICollection<TaskRequest> TaskRequests { get; set; } = new List<TaskRequest>();
}
