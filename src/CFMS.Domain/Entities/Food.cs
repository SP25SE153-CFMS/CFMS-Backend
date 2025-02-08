using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class Food
{
    public Guid Foodid { get; set; }

    public string Name { get; set; } = null!;

    public DateOnly Expirydate { get; set; }

    public string Ingredients { get; set; } = null!;

    public string? Usage { get; set; }

    public string? Notes { get; set; }

    public Guid Supplierid { get; set; }

    public virtual ICollection<Nutrition> Nutritions { get; set; } = new List<Nutrition>();

    public virtual Supplier Supplier { get; set; } = null!;
}
