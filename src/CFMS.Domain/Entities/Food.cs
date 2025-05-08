using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CFMS.Domain.Entities;

public partial class Food : EntityAudit
{
    public Guid FoodId { get; set; }

    public string? FoodCode { get; set; }

    public string? FoodName { get; set; }

    public string? Note { get; set; }

    public DateTime? ProductionDate { get; set; }

    public DateTime? ExpiryDate { get; set; }

    [JsonIgnore]
    public virtual ICollection<NutritionPlanDetail> NutritionPlanDetails { get; set; } = new List<NutritionPlanDetail>();
}
