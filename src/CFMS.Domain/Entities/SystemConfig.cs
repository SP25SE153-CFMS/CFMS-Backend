using System;
using System.Collections.Generic;

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

    public virtual ChickenCoop? Entity { get; set; }

    public virtual Warehouse? EntityNavigation { get; set; }
}
