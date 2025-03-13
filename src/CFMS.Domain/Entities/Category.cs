using CFMS.Domain.Enums.Status;
using CFMS.Domain.Enums.Types;
using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public  class Category : EntityAudit
{
    public Guid CategoryId { get; set; }

    public string? CategoryName { get; set; }

    public CategoryType? CategoryType { get; set; }

    public string? CategoryCode { get; set; }

    public string? Description { get; set; }

    public CategoryStatus? Status { get; set; }

    public virtual ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
}
    