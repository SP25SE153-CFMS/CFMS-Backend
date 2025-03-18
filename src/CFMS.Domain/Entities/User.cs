using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public class User
{
    public Guid UserId { get; set; }

    public string? FullName { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Mail { get; set; }

    public string? Avatar { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public int? Status { get; set; }

    public string? Address { get; set; }

    public string? Cccd { get; set; }

    public int? SystemRole { get; set; }

    public string? HashedPassword { get; set; }

    public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

    public virtual ICollection<FarmEmployee> FarmEmployees { get; set; } = new List<FarmEmployee>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();

    public virtual ICollection<RevokedToken> RevokedTokens { get; set; } = new List<RevokedToken>();

    public virtual ICollection<WarePermission> WarePermissions { get; set; } = new List<WarePermission>();
}
