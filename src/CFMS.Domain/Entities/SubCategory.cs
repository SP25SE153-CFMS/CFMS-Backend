using System;
using System.Collections.Generic;

namespace CFMS.Infrastructure.Persistence;

public partial class SubCategory
{
    public Guid SubCategoryId { get; set; }

    public string? SubCategoryName { get; set; }

    public string? Description { get; set; }

    public string? Status { get; set; }

    public string? DataType { get; set; }

    public DateTime? CreatedDate { get; set; }

    public Guid? CategoryId { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<ChickenCoop> ChickenCoops { get; set; } = new List<ChickenCoop>();

    public virtual ICollection<Flock> FlockBreeds { get; set; } = new List<Flock>();

    public virtual ICollection<Flock> FlockPurposes { get; set; } = new List<Flock>();

    public virtual ICollection<HarvestDetail> HarvestDetails { get; set; } = new List<HarvestDetail>();

    public virtual ICollection<HarvestProduct> HarvestProducts { get; set; } = new List<HarvestProduct>();

    public virtual ICollection<HealthLogDetail> HealthLogDetails { get; set; } = new List<HealthLogDetail>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<QuantityLog> QuantityLogs { get; set; } = new List<QuantityLog>();

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();

    public virtual ICollection<TaskDetail> TaskDetails { get; set; } = new List<TaskDetail>();

    public virtual ICollection<TaskEvaluation> TaskEvaluations { get; set; } = new List<TaskEvaluation>();

    public virtual ICollection<Vaccine> VaccineDiseases { get; set; } = new List<Vaccine>();

    public virtual ICollection<Vaccine> VaccineSuppliers { get; set; } = new List<Vaccine>();
}
