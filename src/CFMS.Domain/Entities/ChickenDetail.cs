using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CFMS.Domain.Entities;

public partial class ChickenDetail
{
    public Guid ChickenDetailId { get; set; }

    public Guid? ChickenId { get; set; }

    public Guid? ChickenBatchId { get; set; }

    //public decimal? Weight { get; set; }

    public int? Quantity { get; set; }

    public int? Gender { get; set; }

    [JsonIgnore]
    public virtual Chicken? Chicken { get; set; }

    [JsonIgnore]
    public virtual ChickenBatch? ChickenBatch { get; set; }
}
