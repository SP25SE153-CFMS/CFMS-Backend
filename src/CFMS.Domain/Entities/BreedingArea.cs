using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public class BreedingArea : EntityAudit
{
    public Guid BreedingAreaId { get; set; }

    public string? BreedingAreaCode { get; set; }

    public string? BreedingAreaName { get; set; }

    public int? MealsPerDay { get; set; }

    public string? Image { get; set; }

    public string? Notes { get; set; }

    public Guid? FarmId { get; set; }

    public double? Area { get; set; }

    public virtual ICollection<ChickenCoop> ChickenCoops { get; set; } = new List<ChickenCoop>();

    public virtual Farm? Farm { get; set; }
}
