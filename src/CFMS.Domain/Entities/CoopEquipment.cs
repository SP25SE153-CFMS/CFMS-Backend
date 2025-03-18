using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public class CoopEquipment : EntityAudit
{
    public Guid CoopEquipmentId { get; set; }

    public Guid? ChickenCoopId { get; set; }

    public Guid? EquipmentId { get; set; }

    public int? Quantity { get; set; }

    public DateTime? AssignedDate { get; set; }

    public DateTime? MaintainDate { get; set; }

    public string? Status { get; set; }

    public string? Note { get; set; }

    public virtual ChickenCoop? ChickenCoop { get; set; }

    public virtual Equipment? Equipment { get; set; }
}
