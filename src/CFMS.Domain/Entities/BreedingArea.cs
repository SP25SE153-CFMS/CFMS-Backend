using System;
using System.Collections.Generic;

namespace CFMS.Infrastructure.Persistence;

public partial class BreedingArea
{
    public Guid BreedingAreaId { get; set; }

    public string? BreedingAreaCode { get; set; }

    public string? BreedingAreaName { get; set; }

    public int? MealsPerDay { get; set; }

    public string? Image { get; set; }

    public string? Notes { get; set; }

    public DateTime? CreatedDate { get; set; }

    public Guid? FarmId { get; set; }

    public virtual ICollection<ChickenCoop> ChickenCoops { get; set; } = new List<ChickenCoop>();

    public virtual Farm? Farm { get; set; }
}
