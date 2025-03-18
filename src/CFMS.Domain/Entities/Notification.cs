using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public class Notification : EntityAudit
{
    public Guid NotificationId { get; set; }

    public string? NotificationName { get; set; }

    public string? Type { get; set; }

    public string? Content { get; set; }

    public bool? IsRead { get; set; }

    public Guid? UserId { get; set; }

    public virtual User? User { get; set; }
}
