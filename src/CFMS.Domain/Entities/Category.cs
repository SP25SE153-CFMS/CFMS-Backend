using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class Category : EntityAudit
{
    public Guid CategoryId { get; set; }

    public string? CategoryName { get; set; }

    public string? CategoryType { get; set; }

    public string? Description { get; set; }

    public int? Status { get; set; }

    public virtual ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
}
