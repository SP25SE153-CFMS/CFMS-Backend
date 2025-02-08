using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class Equipment
{
    public Guid Equipmentid { get; set; }

    public string Equipmentname { get; set; } = null!;

    public string Type { get; set; } = null!;

    public DateOnly Purchasedate { get; set; }

    public TimeSpan? Warrantyperiod { get; set; }

    public string? Status { get; set; }

    public decimal? Cost { get; set; }

    public string? Note { get; set; }

    public virtual ICollection<Breadingequipment> Breadingequipments { get; set; } = new List<Breadingequipment>();
}
