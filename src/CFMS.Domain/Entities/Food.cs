using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class Food
{
    public Guid FoodId { get; set; }

    public string? Name { get; set; }

    public string? Notes { get; set; }

    public string? Ingredients { get; set; }

    public DateTime? ProductionDate { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public Guid ProductId { get; set; }

    public virtual ICollection<Nutrition> Nutritions { get; set; } = new List<Nutrition>();

    public virtual Product? Product { get; set; }
}
