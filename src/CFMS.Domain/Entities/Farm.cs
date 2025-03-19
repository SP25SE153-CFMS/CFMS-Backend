using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public class Farm : EntityAudit
{
    public Guid FarmId { get; set; }

    public string? FarmName { get; set; }

    public string? FarmCode { get; set; }

    public string? Address { get; set; }

    public double? Area { get; set; }

    public int? Scale { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Website { get; set; }

    public string? ImageUrl { get; set; }

    public virtual ICollection<BreedingArea> BreedingAreas { get; set; } = new List<BreedingArea>();

    public virtual ICollection<FarmEmployee> FarmEmployees { get; set; } = new List<FarmEmployee>();

    public virtual ICollection<Warehouse> Warehouses { get; set; } = new List<Warehouse>();
}
