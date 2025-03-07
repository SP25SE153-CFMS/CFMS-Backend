using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class HarvestLog
{
    public Guid HarvestLogId { get; set; }

    public Guid? ChickenCoopId { get; set; }

    public DateTime? Date { get; set; }

    public string? Type { get; set; }

    public int? Total { get; set; }

    public string? Note { get; set; }

    public virtual ChickenCoop? ChickenCoop { get; set; }

    public virtual ICollection<HarvestDetail> HarvestDetails { get; set; } = new List<HarvestDetail>();
}
