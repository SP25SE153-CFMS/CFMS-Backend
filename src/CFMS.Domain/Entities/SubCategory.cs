using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public class SubCategory : EntityAudit
{
    public Guid SubCategoryId { get; set; }

    public string? SubCategoryName { get; set; }

    public string? Description { get; set; }

    public int? Status { get; set; }

    public string? DataType { get; set; }

    public Guid? CategoryId { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<Chicken> Chickens { get; set; } = new List<Chicken>();

    public virtual ICollection<EvaluatedTarget> EvaluatedTargets { get; set; } = new List<EvaluatedTarget>();

    public virtual ICollection<EvaluationTemplate> EvaluationTemplates { get; set; } = new List<EvaluationTemplate>();

    public virtual ICollection<FeedLog> FeedLogs { get; set; } = new List<FeedLog>();

    public virtual ICollection<FeedSession> FeedSessions { get; set; } = new List<FeedSession>();

    public virtual ICollection<GrowthStage> GrowthStages { get; set; } = new List<GrowthStage>();

    public virtual ICollection<HealthLogDetail> HealthLogDetails { get; set; } = new List<HealthLogDetail>();

    public virtual ICollection<InventoryReceipt> InventoryReceipts { get; set; } = new List<InventoryReceipt>();

    public virtual ICollection<InventoryRequestDetail> InventoryRequestDetails { get; set; } = new List<InventoryRequestDetail>();

    public virtual ICollection<InventoryRequest> InventoryRequests { get; set; } = new List<InventoryRequest>();

    public virtual ICollection<Medicine> Medicines { get; set; } = new List<Medicine>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<NutritionPlanDetail> NutritionPlanDetails { get; set; } = new List<NutritionPlanDetail>();

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();

    public virtual ICollection<Resource> ResourcePackages { get; set; } = new List<Resource>();

    public virtual ICollection<Resource> ResourceResourceTypes { get; set; } = new List<Resource>();

    public virtual ICollection<ResourceSupplier> ResourceSupplierPackagePrices { get; set; } = new List<ResourceSupplier>();

    public virtual ICollection<ResourceSupplier> ResourceSupplierSuppliers { get; set; } = new List<ResourceSupplier>();

    public virtual ICollection<ResourceSupplier> ResourceSupplierUnitPrices { get; set; } = new List<ResourceSupplier>();

    public virtual ICollection<Resource> ResourceUnits { get; set; } = new List<Resource>();

    public virtual ICollection<TaskHarvest> TaskHarvests { get; set; } = new List<TaskHarvest>();

    public virtual ICollection<TaskLocation> TaskLocations { get; set; } = new List<TaskLocation>();

    public virtual ICollection<TaskRequest> TaskRequests { get; set; } = new List<TaskRequest>();

    public virtual ICollection<TaskResource> TaskResources { get; set; } = new List<TaskResource>();

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();

    public virtual ICollection<TemplateCriterion> TemplateCriteria { get; set; } = new List<TemplateCriterion>();

    public virtual ICollection<WareTransaction> WareTransactions { get; set; } = new List<WareTransaction>();

    public virtual ICollection<Warehouse> Warehouses { get; set; } = new List<Warehouse>();
}
