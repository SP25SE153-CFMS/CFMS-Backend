using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class User
{
    public Guid Userid { get; set; }

    public string Fullname { get; set; } = null!;

    public DateOnly Dateofbirth { get; set; }

    public DateOnly Startdate { get; set; }

    public bool? Status { get; set; }

    public string? Address { get; set; }

    public string Cccd { get; set; } = null!;

    public Guid? Roleid { get; set; }

    public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

    public virtual ICollection<Farm> Farms { get; set; } = new List<Farm>();

    public virtual ICollection<Inventoryaudit> Inventoryaudits { get; set; } = new List<Inventoryaudit>();

    public virtual ICollection<Performancestatistic> Performancestatistics { get; set; } = new List<Performancestatistic>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<Salary> Salaries { get; set; } = new List<Salary>();

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();

    public virtual ICollection<TimeKeeping> TimeKeepings { get; set; } = new List<TimeKeeping>();

    public virtual ICollection<Workschedule> Workschedules { get; set; } = new List<Workschedule>();

    public List<Healthlog> HealthLogs { get; set; } = new List<Healthlog>();

    public List<Quantitylog> QuantityLogs { get; set; } = new List<Quantitylog>();

    public List<Vaccinationlog> VaccinationLogs { get; set; } = new List<Vaccinationlog>();

    public List<Evaluationsummary> EvaluationSummaries { get; set; } = new List<Evaluationsummary>();
}
