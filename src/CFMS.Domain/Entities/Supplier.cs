using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class Supplier
{
    public Guid Supplierid { get; set; }

    public string Name { get; set; } = null!;

    public string Contactinformation { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Food> Foods { get; set; } = new List<Food>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<Vaccine> Vaccines { get; set; } = new List<Vaccine>();

    public virtual ICollection<Water> Water { get; set; } = new List<Water>();
}
