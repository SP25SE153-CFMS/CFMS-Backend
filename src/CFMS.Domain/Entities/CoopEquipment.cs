using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CFMS.Domain.Entities;

public partial class CoopEquipment : EntityAudit
{
    public Guid CoopEquipmentId { get; set; }

    public Guid? ChickenCoopId { get; set; }

    public Guid? EquipmentId { get; set; }

    public int? Quantity { get; set; }

    public DateTime? AssignedDate { get; set; }

    public DateTime? LastMaintenanceDate { get; set; }

    public DateTime? NextMaintenanceDate { get; set; }

    public int MaintenanceInterval { get; set; }

    public int? Status { get; set; }

    public string? Note { get; set; }

    [JsonIgnore]
    public virtual ChickenCoop? ChickenCoop { get; set; }

    public virtual Equipment? Equipment { get; set; }
}
