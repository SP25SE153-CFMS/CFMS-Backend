using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class Farm
{
    public Guid Farmid { get; set; }

    public string Farmname { get; set; } = null!;

    public string Farmcode { get; set; } = null!;

    public Guid Userid { get; set; }

    public string? Type { get; set; }

    public string? Address { get; set; }

    public decimal? Area { get; set; }

    public string? Scale { get; set; }

    public string? Phonenumber { get; set; }

    public string? Website { get; set; }

    public string? Farmimage { get; set; }

    public virtual ICollection<Breadingarea> Breadingareas { get; set; } = new List<Breadingarea>();

    public virtual ICollection<Exportedproduct> Exportedproducts { get; set; } = new List<Exportedproduct>();

    public virtual User User { get; set; } = null!;
}
