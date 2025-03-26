using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CFMS.Domain.Entities;

public partial class Medicine : EntityAudit
{
    public Guid MedicineId { get; set; }

    public string? MedicineCode { get; set; }

    public string? MedicineName { get; set; }

    public string? Usage { get; set; }

    public string? DosageForm { get; set; }

    public string? StorageCondition { get; set; }

    public Guid? DiseaseId { get; set; }

    public DateTime? ProductionDate { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public virtual SubCategory? Disease { get; set; }

    //[JsonIgnore]
    //public virtual Resource MedicineNavigation { get; set; } = null!;
}
