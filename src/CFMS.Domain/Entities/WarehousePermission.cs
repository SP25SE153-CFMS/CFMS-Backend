using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class WarehousePermission
{
    public Guid PermissionId { get; set; }

    public Guid? WareId { get; set; }

    public Guid? UserId { get; set; }

    public DateTime? GrantedAt { get; set; }

    public virtual User? User { get; set; }

    public virtual Warehouse? Ware { get; set; }
}
