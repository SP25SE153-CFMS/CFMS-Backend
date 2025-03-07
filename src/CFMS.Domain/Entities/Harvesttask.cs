using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class HarvestTask
{
    public Guid HTaskId { get; set; }

    public Guid? TaskId { get; set; }

    public string? HarvestType { get; set; }

    public string? Description { get; set; }

    public Guid? QuantityTypeId { get; set; }

    public DateOnly? HarvestDate { get; set; }

    public virtual HarvestProduct? QuantityType { get; set; }

    public virtual Task? Task { get; set; }
}
