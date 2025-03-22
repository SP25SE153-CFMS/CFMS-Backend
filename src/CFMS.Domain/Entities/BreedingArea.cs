using System;
using System.Collections.Generic;

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

    public int? Status { get; set; }

    public virtual ICollection<ChickenCoop> ChickenCoops { get; set; } = new List<ChickenCoop>();

    public virtual Farm? Farm { get; set; }
}
