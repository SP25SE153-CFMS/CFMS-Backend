using System;
using System.Collections.Generic;

namespace CFMS.Infrastructure.Persistence;

public partial class Farm
{
    public Guid FarmId { get; set; }

    public string? FarmName { get; set; }

    public string? FarmCode { get; set; }

    public string? Type { get; set; }

    public string? Address { get; set; }

    public double? Area { get; set; }

    public string? Scale { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Website { get; set; }

    public string? FarmImage { get; set; }

    public Guid? OwnerId { get; set; }

    public virtual ICollection<BreedingArea> BreedingAreas { get; set; } = new List<BreedingArea>();

    public virtual ICollection<FarmEmployee> FarmEmployees { get; set; } = new List<FarmEmployee>();

    public virtual User? Owner { get; set; }

    public virtual ICollection<Warehouse> Warehouses { get; set; } = new List<Warehouse>();
}
