using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class Healthcriterion
{
    public Guid Criteriaid { get; set; }

    public string Name { get; set; } = null!;

    public string Characteristic { get; set; } = null!;

    public string Unit { get; set; } = null!;

    public string? Description { get; set; }
}
