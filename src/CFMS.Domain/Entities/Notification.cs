using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class Notification : EntityAudit
{
    public Guid NotificationId { get; set; }

    public string? NotificationName { get; set; }

    public string? NotificationType { get; set; }

    public string? Content { get; set; }

    public int? IsRead { get; set; }

    public Guid? UserId { get; set; }

    public virtual User? User { get; set; }
}
