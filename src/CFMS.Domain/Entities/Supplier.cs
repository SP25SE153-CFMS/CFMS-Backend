﻿using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class Supplier : EntityAudit
{
    public Guid SupplierId { get; set; }

    public string? SupplierName { get; set; }

    public string? SupplierCode { get; set; }

    public string? Address { get; set; }

    public string? PhoneNumber { get; set; }

    public string? BankAccount { get; set; }

    public Guid? FarmId { get; set; }

    public int? Status { get; set; }

    public virtual ICollection<ResourceSupplier> ResourceSuppliers { get; set; } = new List<ResourceSupplier>();

    public virtual ICollection<WareStock> ResourceWareStocks { get; set; } = new List<WareStock>();
}
