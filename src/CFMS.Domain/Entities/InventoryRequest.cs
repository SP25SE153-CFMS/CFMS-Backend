using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CFMS.Domain.Entities;

public partial class InventoryRequest : EntityAudit
{
    public Guid InventoryRequestId { get; set; }

    public Guid? RequestId { get; set; }

    public Guid? InventoryRequestTypeId { get; set; }

    public Guid? WareFromId { get; set; }

    public Guid? WareToId { get; set; }

    public int? IsFulfilled { get; set; }

    public int? BatchNumber { get; set; }

    public virtual ICollection<InventoryReceipt> InventoryReceipts { get; set; } = new List<InventoryReceipt>();

    public virtual ICollection<InventoryRequestDetail> InventoryRequestDetails { get; set; } = new List<InventoryRequestDetail>();

    public virtual SubCategory? InventoryRequestType { get; set; }

    [JsonIgnore]
    public virtual Request? Request { get; set; }

    public virtual Warehouse? WareFrom { get; set; }

    public virtual Warehouse? WareTo { get; set; }
}
