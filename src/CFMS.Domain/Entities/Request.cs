using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public class Request : EntityAudit
{
    public Guid RequestId { get; set; }

    public Guid? RequestTypeId { get; set; }

    public string? Status { get; set; }

    public string? Description { get; set; }

    public bool? IsEmergency { get; set; }

    public Guid? ApprovedBy { get; set; }

    public DateTime? ApprovedAt { get; set; }

    public virtual User? ApprovedByNavigation { get; set; }

    public virtual ICollection<RequestDetail> RequestDetails { get; set; } = new List<RequestDetail>();

    public virtual SubCategory? RequestType { get; set; }
}
