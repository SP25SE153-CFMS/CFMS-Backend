using System;
using System.Collections.Generic;

namespace CFMS.Infrastructure.Persistence;

public partial class Category
{
    public Guid CategoryId { get; set; }

    public string? CategoryType { get; set; }

    public string? CategoryCode { get; set; }

    public string? Description { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
}
