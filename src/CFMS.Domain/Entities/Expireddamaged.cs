using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class Expireddamaged
{
    public Guid Edproductid { get; set; }

    public int Quantity { get; set; }

    public string Reason { get; set; } = null!;

    public Guid Productid { get; set; }

    public string Actiontaken { get; set; } = null!;

    public DateTime Recorddate { get; set; }

    public virtual Product Product { get; set; } = null!;
}
