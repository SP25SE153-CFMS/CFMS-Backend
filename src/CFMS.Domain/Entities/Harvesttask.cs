using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class Harvesttask
{
    public Guid Htaskid { get; set; }

    public Guid Taskid { get; set; }

    public string Harvesttype { get; set; } = null!;

    public int Totalquantity { get; set; }

    public int Damagedquantity { get; set; }

    public int Goodquantity { get; set; }

    public DateOnly Harvestdate { get; set; }

    public string? Status { get; set; }

    public string? Note { get; set; }

    public virtual Task Task { get; set; } = null!;
}
