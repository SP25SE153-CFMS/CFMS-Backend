using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public class Food : EntityAudit
{
    public Guid FoodId { get; set; }

    public string? FoodCode { get; set; }

    public string? FoodName { get; set; }

    public string? Note { get; set; }

    public Guid? FoodIngredientId { get; set; }

    public DateTime? ProductionDate { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public virtual Resource FoodNavigation { get; set; } = null!;

    public virtual ICollection<NutritionPlanDetail> NutritionPlanDetails { get; set; } = new List<NutritionPlanDetail>();
}
