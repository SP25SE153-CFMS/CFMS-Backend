using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class FlockNutrition
{
    public Guid FlockNutritionId { get; set; }

    public Guid? FlockId { get; set; }

    public Guid? NutritionId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual Flock? Flock { get; set; }

    public virtual Nutrition? Nutrition { get; set; }
}
