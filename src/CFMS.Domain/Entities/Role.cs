using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class Role
{
    public Guid Roleid { get; set; }

    public string Rolename { get; set; } = null!;

    public string Permission { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
