using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CFMS.Domain.Entities;

public partial class SystemConfig : EntityAudit
{
    public Guid SystemConfigId { get; set; }

    public string? SettingName { get; set; }

    public decimal? SettingValue { get; set; }

    public string? Description { get; set; }

    public DateTime? EffectedDateFrom { get; set; }

    public DateTime? EffectedDateTo { get; set; }

    public string? EntityType { get; set; }

    public Guid? EntityId { get; set; }

    public int? Status { get; set; }

    public virtual Chicken? Chicken { get; set; }

    public virtual ChickenCoop? ChickenCoop { get; set; }

    public virtual Task? Task { get; set; }

    public virtual Warehouse? Warehouse { get; set; }
}
