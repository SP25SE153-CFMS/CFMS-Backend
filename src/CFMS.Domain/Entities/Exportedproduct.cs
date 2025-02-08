using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class Exportedproduct
{
    public Guid Eproductid { get; set; }

    public string Productcode { get; set; } = null!;

    public int Quantity { get; set; }

    public string? Reason { get; set; }

    public DateOnly Exporteddate { get; set; }

    public DateOnly? Expireddate { get; set; }

    public string? Storagelocation { get; set; }

    public Guid Chickenbatchid { get; set; }

    public Guid Farmid { get; set; }

    public Guid Productid { get; set; }

    public virtual Chickenbatch Chickenbatch { get; set; } = null!;

    public virtual Farm Farm { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
