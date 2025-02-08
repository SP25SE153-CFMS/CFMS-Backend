using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class Product
{
    public Guid Productid { get; set; }

    public string Productcode { get; set; } = null!;

    public string Productname { get; set; } = null!;

    public string Storagelocation { get; set; } = null!;

    public string Unit { get; set; } = null!;

    public int? Minstock { get; set; }

    public int? Maxstock { get; set; }

    public Guid Supplierid { get; set; }

    public virtual ICollection<Expireddamaged> Expireddamageds { get; set; } = new List<Expireddamaged>();

    public virtual ICollection<Exportedproduct> Exportedproducts { get; set; } = new List<Exportedproduct>();

    public virtual ICollection<Inventoryaudit> Inventoryaudits { get; set; } = new List<Inventoryaudit>();

    public virtual Supplier Supplier { get; set; } = null!;
}
