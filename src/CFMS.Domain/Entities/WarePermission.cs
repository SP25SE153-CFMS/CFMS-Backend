using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public class WarePermission : EntityAudit
{
    public Guid PermissionId { get; set; }

    public Guid? WareId { get; set; }

    public Guid? UserId { get; set; }

    public int PermissionLevel { get; set; }

    public DateTime? GrantedAt { get; set; }

    public virtual User? User { get; set; }

    public virtual Warehouse? Ware { get; set; }
}
