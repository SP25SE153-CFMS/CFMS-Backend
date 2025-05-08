using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CFMS.Domain.Entities;

public partial class BreedingArea : EntityAudit
{
    public Guid BreedingAreaId { get; set; }

    public string? BreedingAreaCode { get; set; }

    public string? BreedingAreaName { get; set; }

    public string? ImageUrl { get; set; }

    public string? Notes { get; set; }

    public Guid? FarmId { get; set; }

    public decimal? Area { get; set; }

    public Guid? AreaUnitId { get; set; }

    public int? Status { get; set; }

    public virtual SubCategory? AreaUnit { get; set; }

    public virtual ICollection<ChickenCoop> ChickenCoops { get; set; } = new List<ChickenCoop>();

    [JsonIgnore]
    public virtual Farm? Farm { get; set; }
}
