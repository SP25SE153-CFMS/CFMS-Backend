using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CFMS.Domain.Entities;

public partial class Farm : EntityAudit
{
    public Guid FarmId { get; set; }

    public string? FarmName { get; set; }

    public string? FarmCode { get; set; }

    public string? Address { get; set; }

    public decimal? Area { get; set; }

    public Guid? AreaUnitId { get; set; }

    public int? Scale { get; set; }

    public decimal? Longitude { get; set; }

    public decimal? Latitude { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Website { get; set; }

    public string? ImageUrl { get; set; }

    public virtual SubCategory? AreaUnit { get; set; }

    public virtual ICollection<BreedingArea> BreedingAreas { get; set; } = new List<BreedingArea>();

    public virtual ICollection<FarmEmployee> FarmEmployees { get; set; } = new List<FarmEmployee>();

    [JsonIgnore]
    public virtual ICollection<Warehouse> Warehouses { get; set; } = new List<Warehouse>();
}
