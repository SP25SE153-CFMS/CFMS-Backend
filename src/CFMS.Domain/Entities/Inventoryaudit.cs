using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class Inventoryaudit
{
    public Guid Auditid { get; set; }

    public DateTime Auditdate { get; set; }

    public Guid Userid { get; set; }

    public Guid Productid { get; set; }

    public int Systemquantity { get; set; }

    public int Actualquantity { get; set; }

    public string Condition { get; set; } = null!;

    public string? Reason { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
