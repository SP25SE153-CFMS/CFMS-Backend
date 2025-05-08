using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CFMS.Domain.Entities;

public partial class SubCategory : EntityAudit
{
    public Guid SubCategoryId { get; set; }

    public string? SubCategoryName { get; set; }

    public string? Description { get; set; }

    public int? Status { get; set; }

    public string? DataType { get; set; }

    public Guid? CategoryId { get; set; }

    public Guid? FarmId { get; set; }

    //public int? IsHidden { get; set; }

    [JsonIgnore]
    public virtual Category? Category { get; set; }

    [JsonIgnore]
    public virtual ICollection<HarvestProduct> HarvestProductTypes { get; set; } = new List<HarvestProduct>();

    [JsonIgnore]
    public virtual ICollection<ChickenCoop> ChickenCoopDensityUnits { get; set; } = new List<ChickenCoop>();

    [JsonIgnore]
    public virtual ICollection<ChickenCoop> ChickenCoopAreaUnits { get; set; } = new List<ChickenCoop>();

    [JsonIgnore]
    public virtual ICollection<Farm> FarmAreaUnits { get; set; } = new List<Farm>();

    [JsonIgnore]
    public virtual ICollection<BreedingArea> BreedingAreaUnits { get; set; } = new List<BreedingArea>();

    [JsonIgnore]
    public virtual ICollection<ChickenCoop> ChickenCoopPurposes { get; set; } = new List<ChickenCoop>();

    [JsonIgnore]
    public virtual ICollection<Chicken> Chickens { get; set; } = new List<Chicken>();

    [JsonIgnore]
    public virtual ICollection<Equipment> EquipmentSizeUnits { get; set; } = new List<Equipment>();

    [JsonIgnore]
    public virtual ICollection<Equipment> EquipmentWeightUnits { get; set; } = new List<Equipment>();

    [JsonIgnore]
    public virtual ICollection<Equipment> EquipmentMaterials { get; set; } = new List<Equipment>();

    //[JsonIgnore]
    //public virtual ICollection<EvaluatedTarget> EvaluatedTargets { get; set; } = new List<EvaluatedTarget>();

    //[JsonIgnore]
    //public virtual ICollection<EvaluationTemplate> EvaluationTemplates { get; set; } = new List<EvaluationTemplate>();

    [JsonIgnore]
    public virtual ICollection<FeedLog> FeedLogs { get; set; } = new List<FeedLog>();

    [JsonIgnore]
    public virtual ICollection<FeedSession> FeedSessions { get; set; } = new List<FeedSession>();

    [JsonIgnore]
    public virtual ICollection<GrowthStage> GrowthStages { get; set; } = new List<GrowthStage>();

    [JsonIgnore]
    public virtual ICollection<HealthLogDetail> HealthLogDetails { get; set; } = new List<HealthLogDetail>();

    [JsonIgnore]
    public virtual ICollection<InventoryReceipt> InventoryReceipts { get; set; } = new List<InventoryReceipt>();

    [JsonIgnore]
    public virtual ICollection<InventoryRequestDetail> InventoryRequestDetails { get; set; } = new List<InventoryRequestDetail>();

    [JsonIgnore]
    public virtual ICollection<InventoryRequest> InventoryRequests { get; set; } = new List<InventoryRequest>();

    [JsonIgnore]
    public virtual ICollection<Medicine> Medicines { get; set; } = new List<Medicine>();

    [JsonIgnore]
    public virtual ICollection<NutritionPlanDetail> NutritionPlanDetails { get; set; } = new List<NutritionPlanDetail>();

    [JsonIgnore]
    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();

    [JsonIgnore]
    public virtual ICollection<Resource> ResourcePackages { get; set; } = new List<Resource>();

    [JsonIgnore]
    public virtual ICollection<Resource> ResourceResourceTypes { get; set; } = new List<Resource>();

    //[JsonIgnore]
    //public virtual ICollection<ResourceSupplier> ResourceSupplierPackagePrices { get; set; } = new List<ResourceSupplier>();

    //[JsonIgnore]
    //public virtual ICollection<ResourceSupplier> ResourceSupplierUnitPrices { get; set; } = new List<ResourceSupplier>();

    [JsonIgnore]
    public virtual ICollection<Resource> ResourceUnits { get; set; } = new List<Resource>();

    [JsonIgnore]
    public virtual ICollection<TaskHarvest> TaskHarvests { get; set; } = new List<TaskHarvest>();

    [JsonIgnore]
    public virtual ICollection<TaskRequest> TaskRequests { get; set; } = new List<TaskRequest>();

    [JsonIgnore]
    public virtual ICollection<TaskResource> TaskResources { get; set; } = new List<TaskResource>();

    [JsonIgnore]
    public virtual ICollection<TaskResource> TaskResourceUnits { get; set; } = new List<TaskResource>();

    [JsonIgnore]
    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();

    //public virtual ICollection<FrequencySchedule> TimeUnits { get; set; } = new List<FrequencySchedule>();

    [JsonIgnore]
    //public virtual ICollection<TemplateCriterion> TemplateCriteria { get; set; } = new List<TemplateCriterion>();

    //[JsonIgnore]
    public virtual ICollection<WareTransaction> WareTransactions { get; set; } = new List<WareTransaction>();

    [JsonIgnore]
    public virtual ICollection<Warehouse> Warehouses { get; set; } = new List<Warehouse>();
}
