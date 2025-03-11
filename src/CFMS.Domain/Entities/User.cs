using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class User
{
    public Guid UserId { get; set; }

    public string? FullName { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Mail { get; set; }

    public string? Avatar { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public DateOnly? StartDate { get; set; }

    public string? Status { get; set; }

    public string? Address { get; set; }

    public string? Cccd { get; set; }

    public string? RoleName { get; set; }

    public string? HashedPassword { get; set; }

    public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    public virtual ICollection<FarmEmployee> FarmEmployees { get; set; } = new List<FarmEmployee>();

    public virtual ICollection<Farm> Farms { get; set; } = new List<Farm>();

    public virtual ICollection<HealthLogDetail> HealthLogDetails { get; set; } = new List<HealthLogDetail>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Performance> Performances { get; set; } = new List<Performance>();

    public virtual ICollection<QuantityLog> QuantityLogs { get; set; } = new List<QuantityLog>();

    public virtual ICollection<Request> RequestApprovedByNavigations { get; set; } = new List<Request>();

    public virtual ICollection<Request> RequestCreatedByNavigations { get; set; } = new List<Request>();

    public virtual ICollection<Request> RequestUsers { get; set; } = new List<Request>();

    public virtual ICollection<RevokedToken> RevokedTokens { get; set; } = new List<RevokedToken>();

    public virtual ICollection<Salary> Salaries { get; set; } = new List<Salary>();

    public virtual ICollection<VaccinationEmployee> VaccinationEmployees { get; set; } = new List<VaccinationEmployee>();

    public virtual ICollection<WarehousePermission> WarehousePermissions { get; set; } = new List<WarehousePermission>();
}
