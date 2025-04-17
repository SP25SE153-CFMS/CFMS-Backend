using System;
using System.Collections.Generic;
using System.Linq;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;

namespace CFMS.Infrastructure.Persistence;

public partial class CfmsDbContext : DbContext
{
    private readonly ICurrentUserService? _currentUserService;

    public CfmsDbContext()
    {
    }

    public CfmsDbContext(DbContextOptions<CfmsDbContext> options, ICurrentUserService? currentUserService)
        : base(options)
    {
        _currentUserService = currentUserService;
    }

    public virtual DbSet<Assignment> Assignments { get; set; }

    public virtual DbSet<BreedingArea> BreedingAreas { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Chicken> Chickens { get; set; }

    public virtual DbSet<ChickenBatch> ChickenBatches { get; set; }

    public virtual DbSet<ChickenCoop> ChickenCoops { get; set; }

    public virtual DbSet<ChickenDetail> ChickenDetails { get; set; }

    public virtual DbSet<CoopEquipment> CoopEquipments { get; set; }

    public virtual DbSet<Equipment> Equipment { get; set; }

    public virtual DbSet<EvaluatedTarget> EvaluatedTargets { get; set; }

    public virtual DbSet<EvaluationResult> EvaluationResults { get; set; }

    public virtual DbSet<EvaluationResultDetail> EvaluationResultDetails { get; set; }

    public virtual DbSet<EvaluationTemplate> EvaluationTemplates { get; set; }

    public virtual DbSet<Farm> Farms { get; set; }

    public virtual DbSet<FarmEmployee> FarmEmployees { get; set; }

    public virtual DbSet<FeedLog> FeedLogs { get; set; }

    public virtual DbSet<FeedSession> FeedSessions { get; set; }

    public virtual DbSet<Food> Foods { get; set; }

    public virtual DbSet<GrowthBatch> GrowthBatches { get; set; }

    public virtual DbSet<GrowthStage> GrowthStages { get; set; }

    public virtual DbSet<HealthLog> HealthLogs { get; set; }

    public virtual DbSet<HealthLogDetail> HealthLogDetails { get; set; }

    public virtual DbSet<InventoryReceipt> InventoryReceipts { get; set; }

    public virtual DbSet<InventoryReceiptDetail> InventoryReceiptDetails { get; set; }

    public virtual DbSet<InventoryRequest> InventoryRequests { get; set; }

    public virtual DbSet<InventoryRequestDetail> InventoryRequestDetails { get; set; }

    public virtual DbSet<Medicine> Medicines { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<NutritionPlan> NutritionPlans { get; set; }

    public virtual DbSet<NutritionPlanDetail> NutritionPlanDetails { get; set; }

    public virtual DbSet<QuantityLog> QuantityLogs { get; set; }

    public virtual DbSet<Request> Requests { get; set; }

    public virtual DbSet<Resource> Resources { get; set; }

    public virtual DbSet<ResourceSupplier> ResourceSuppliers { get; set; }

    public virtual DbSet<RevokedToken> RevokedTokens { get; set; }

    public virtual DbSet<Shift> Shifts { get; set; }

    public virtual DbSet<ShiftSchedule> ShiftSchedules { get; set; }

    public virtual DbSet<SubCategory> SubCategories { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<SystemConfig> SystemConfigs { get; set; }

    public virtual DbSet<Domain.Entities.Task> Tasks { get; set; }

    public virtual DbSet<TaskHarvest> TaskHarvests { get; set; }

    public virtual DbSet<TaskLocation> TaskLocations { get; set; }

    public virtual DbSet<TaskLog> TaskLogs { get; set; }

    public virtual DbSet<TaskRequest> TaskRequests { get; set; }

    public virtual DbSet<TaskResource> TaskResources { get; set; }

    //public virtual DbSet<FrequencySchedule> FrequencySchedules { get; set; }

    public virtual DbSet<TemplateCriterion> TemplateCriteria { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<VaccineLog> VaccineLogs { get; set; }

    public virtual DbSet<WarePermission> WarePermissions { get; set; }

    public virtual DbSet<WareStock> WareStocks { get; set; }

    public virtual DbSet<WareTransaction> WareTransactions { get; set; }

    public virtual DbSet<Warehouse> Warehouses { get; set; }

    public virtual DbSet<HarvestProduct> HarvestProducts { get; set; }

    private void OnBeforeSaving()
    {
        var entities = ChangeTracker.Entries()
            .Where(x => x.Entity is EntityAudit)
            .ToList();

        if (entities.Any())
        {
            UpdateSoftDelete(entities);
            UpdateEntityAudit(entities);
        }
    }

    private void UpdateSoftDelete(List<EntityEntry> entries)
    {
        var filtered = entries
            .Where(x => x.State == EntityState.Deleted);

        foreach (var entry in filtered)
        {
            entry.State = EntityState.Modified;
            ((EntityAudit)entry.Entity).IsDeleted = true;
            ((EntityAudit)entry.Entity).DeletedWhen = DateTime.UtcNow.ToLocalTime();
            break;
        }
    }

    private void UpdateEntityAudit(List<EntityEntry> entries)
    {
        var filtered = entries
        .Where(x => x.State == EntityState.Added
                || x.State == EntityState.Modified);

        var currentUserId = Guid.Parse(_currentUserService?.GetUserId()!);

        foreach (var entry in filtered)
        {
            if (entry.State == EntityState.Added)
            {
                ((EntityAudit)entry.Entity).CreatedWhen = DateTime.UtcNow.ToLocalTime();
                ((EntityAudit)entry.Entity).CreatedByUserId = currentUserId;
            }
            ((EntityAudit)entry.Entity).LastEditedWhen = DateTime.UtcNow.ToLocalTime();
            ((EntityAudit)entry.Entity).LastEditedByUserId = currentUserId;
        }
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        OnBeforeSaving();
        return base.SaveChangesAsync(cancellationToken);
    }

    public static string GetConnectionString(string connectionStringName)
    {
        string? connectionString = Environment.GetEnvironmentVariable($"ConnectionStrings__{connectionStringName}");

        if (string.IsNullOrEmpty(connectionString))
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var directoryInfo = new DirectoryInfo(basePath);

            while (directoryInfo != null && !File.Exists(Path.Combine(directoryInfo.FullName, "CFMS.Api", "appsettings.json")))
            {
                directoryInfo = directoryInfo.Parent;
            }

            if (directoryInfo == null)
            {
                throw new FileNotFoundException("The configuration file 'appsettings.json' was not found in the project directory or any parent directories.");
            }

            var configPath = Path.Combine(directoryInfo.FullName, "CFMS.Api", "appsettings.json");

            var config = new ConfigurationBuilder()
                .SetBasePath(directoryInfo.FullName)
                .AddJsonFile(configPath, optional: false, reloadOnChange: true)
                .Build();

            connectionString = config.GetConnectionString(connectionStringName);
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException($"Connection string '{connectionStringName}' is not found in the configuration.");
            }
        }

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException($"Không tìm thấy Connection String '{connectionStringName}'");
        }

        return connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(GetConnectionString("DefaultConnection"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<HarvestProduct>()
        .HasOne(a => a.CreatedByUser)
        .WithMany()
        .HasForeignKey(a => a.CreatedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<HarvestProduct>()
        .HasOne(a => a.LastEditedByUser)
        .WithMany()
        .HasForeignKey(a => a.LastEditedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Notification>()
        .HasOne(a => a.CreatedByUser)
        .WithMany()
        .HasForeignKey(a => a.CreatedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Notification>()
        .HasOne(a => a.LastEditedByUser)
        .WithMany()
        .HasForeignKey(a => a.LastEditedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<WarePermission>()
        .HasOne(a => a.CreatedByUser)
        .WithMany()
        .HasForeignKey(a => a.CreatedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<WarePermission>()
        .HasOne(a => a.LastEditedByUser)
        .WithMany()
        .HasForeignKey(a => a.LastEditedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Assignment>()
        .HasOne(a => a.CreatedByUser)
        .WithMany()
        .HasForeignKey(a => a.CreatedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Assignment>()
        .HasOne(a => a.LastEditedByUser)
        .WithMany()
        .HasForeignKey(a => a.LastEditedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<BreedingArea>()
        .HasOne(a => a.CreatedByUser)
        .WithMany()
        .HasForeignKey(a => a.CreatedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<BreedingArea>()
        .HasOne(a => a.LastEditedByUser)
        .WithMany()
        .HasForeignKey(a => a.LastEditedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Category>()
        .HasOne(a => a.CreatedByUser)
        .WithMany()
        .HasForeignKey(a => a.CreatedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Category>()
        .HasOne(a => a.LastEditedByUser)
        .WithMany()
        .HasForeignKey(a => a.LastEditedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ChickenBatch>()
        .HasOne(a => a.CreatedByUser)
        .WithMany()
        .HasForeignKey(a => a.CreatedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ChickenBatch>()
        .HasOne(a => a.LastEditedByUser)
        .WithMany()
        .HasForeignKey(a => a.LastEditedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ChickenCoop>()
        .HasOne(a => a.CreatedByUser)
        .WithMany()
        .HasForeignKey(a => a.CreatedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ChickenCoop>()
        .HasOne(a => a.LastEditedByUser)
        .WithMany()
        .HasForeignKey(a => a.LastEditedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Equipment>()
        .HasOne(a => a.CreatedByUser)
        .WithMany()
        .HasForeignKey(a => a.CreatedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Equipment>()
        .HasOne(a => a.LastEditedByUser)
        .WithMany()
        .HasForeignKey(a => a.LastEditedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Farm>()
        .HasOne(a => a.CreatedByUser)
        .WithMany()
        .HasForeignKey(a => a.CreatedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Farm>()
        .HasOne(a => a.LastEditedByUser)
        .WithMany()
        .HasForeignKey(a => a.LastEditedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<FarmEmployee>()
        .HasOne(a => a.CreatedByUser)
        .WithMany()
        .HasForeignKey(a => a.CreatedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<FarmEmployee>()
        .HasOne(a => a.LastEditedByUser)
        .WithMany()
        .HasForeignKey(a => a.LastEditedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Food>()
        .HasOne(a => a.CreatedByUser)
        .WithMany()
        .HasForeignKey(a => a.CreatedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Food>()
        .HasOne(a => a.LastEditedByUser)
        .WithMany()
        .HasForeignKey(a => a.LastEditedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<HealthLog>()
        .HasOne(a => a.CreatedByUser)
        .WithMany()
        .HasForeignKey(a => a.CreatedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<HealthLog>()
        .HasOne(a => a.LastEditedByUser)
        .WithMany()
        .HasForeignKey(a => a.LastEditedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<QuantityLog>()
        .HasOne(a => a.CreatedByUser)
        .WithMany()
        .HasForeignKey(a => a.CreatedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<QuantityLog>()
        .HasOne(a => a.LastEditedByUser)
        .WithMany()
        .HasForeignKey(a => a.LastEditedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Request>()
        .HasOne(a => a.CreatedByUser)
        .WithMany()
        .HasForeignKey(a => a.CreatedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Request>()
        .HasOne(a => a.LastEditedByUser)
        .WithMany()
        .HasForeignKey(a => a.LastEditedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<SubCategory>()
        .HasOne(a => a.CreatedByUser)
        .WithMany()
        .HasForeignKey(a => a.CreatedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<SubCategory>()
        .HasOne(a => a.LastEditedByUser)
        .WithMany()
        .HasForeignKey(a => a.LastEditedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Domain.Entities.Task>()
        .HasOne(a => a.CreatedByUser)
        .WithMany()
        .HasForeignKey(a => a.CreatedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Domain.Entities.Task>()
        .HasOne(a => a.LastEditedByUser)
        .WithMany()
        .HasForeignKey(a => a.LastEditedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TaskLog>()
        .HasOne(a => a.CreatedByUser)
        .WithMany()
        .HasForeignKey(a => a.CreatedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TaskLog>()
        .HasOne(a => a.LastEditedByUser)
        .WithMany()
        .HasForeignKey(a => a.LastEditedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Warehouse>()
        .HasOne(a => a.CreatedByUser)
        .WithMany()
        .HasForeignKey(a => a.CreatedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Warehouse>()
        .HasOne(a => a.LastEditedByUser)
        .WithMany()
        .HasForeignKey(a => a.LastEditedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<WareTransaction>()
        .HasOne(a => a.CreatedByUser)
        .WithMany()
        .HasForeignKey(a => a.CreatedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<WareTransaction>()
        .HasOne(a => a.LastEditedByUser)
        .WithMany()
        .HasForeignKey(a => a.LastEditedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<SystemConfig>()
        .HasOne(a => a.CreatedByUser)
        .WithMany()
        .HasForeignKey(a => a.CreatedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<SystemConfig>()
        .HasOne(a => a.LastEditedByUser)
        .WithMany()
        .HasForeignKey(a => a.LastEditedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<HarvestProduct>(entity =>
        {
            entity.HasKey(e => e.HarvestProductId).HasName("HarvestProduct_pkey");

            entity.ToTable("HarvestProduct");

            entity.Property(e => e.HarvestProductId).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.HarvestProductName).HasColumnType("character varying");

            entity.HasOne(d => d.HarvestProductType).WithMany(p => p.HarvestProductTypes)
                .HasForeignKey(d => d.HarvestProductTypeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("HarvestProduct_HarvestProductTypeId_fkey");
        });

        modelBuilder.Entity<Assignment>(entity =>
        {
            entity.HasKey(e => e.AssignmentId).HasName("Assignment_pkey");

            entity.ToTable("Assignment");

            entity.Property(e => e.AssignmentId).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.AssignedDate).HasColumnType("timestamp(6) without time zone");
            entity.Property(e => e.Status).HasDefaultValue(1);

            entity.HasOne(d => d.AssignedTo).WithMany(p => p.Assignments)
                .HasForeignKey(d => d.AssignedToId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Assignment_AssignedToId_fkey");

            //entity.HasOne(d => d.ShiftSchedule).WithMany(p => p.Assignments)
            //    .HasForeignKey(d => d.ShiftScheduleId)
            //    .HasConstraintName("ShiftSchedule_ShiftScheduleId_fkey");

            entity.HasOne(d => d.Task).WithMany(p => p.Assignments)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Assignment_taskId_fkey");

            //entity.HasOne(d => d.FrequencySchedule).WithMany(p => p.Assignments)
            //    .HasForeignKey(d => d.TaskScheduleId)
            //    .OnDelete(DeleteBehavior.SetNull)
            //    .HasConstraintName("Assignment_TaskScheduleId_fkey");
        });

        modelBuilder.Entity<BreedingArea>(entity =>
        {
            entity.HasKey(e => e.BreedingAreaId).HasName("BreedingArea_pkey");

            entity.ToTable("BreedingArea");

            entity.Property(e => e.BreedingAreaId).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.BreedingAreaCode).HasColumnType("character varying");
            entity.Property(e => e.BreedingAreaName).HasColumnType("character varying");
            entity.Property(e => e.ImageUrl).HasColumnType("character varying");
            entity.Property(e => e.Status).HasDefaultValue(1);

            entity.HasOne(d => d.Farm).WithMany(p => p.BreedingAreas)
                .HasForeignKey(d => d.FarmId)
                .HasConstraintName("BreedingArea_FarmId_fkey");

            entity.HasOne(d => d.AreaUnit).WithMany(p => p.BreedingAreaUnits)
                .HasForeignKey(d => d.AreaUnitId)
                .HasConstraintName("BreedingArea_AreaUnitId_fkey");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("Category_pkey");

            entity.ToTable("Category");

            entity.Property(e => e.CategoryId).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.CategoryName).HasColumnType("character varying");
            entity.Property(e => e.CategoryType).HasColumnType("character varying");
            entity.Property(e => e.Status).HasDefaultValue(1);
        });

        modelBuilder.Entity<Chicken>(entity =>
        {
            entity.HasKey(e => e.ChickenId).HasName("Chicken_pkey");

            entity.ToTable("Chicken");

            entity.Property(e => e.ChickenId).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.ChickenCode).HasColumnType("character varying");
            entity.Property(e => e.ChickenName).HasColumnType("character varying");
            entity.Property(e => e.Status).HasDefaultValue(1);

            entity.HasOne(e => e.ChickenNavigation)
                .WithOne(c => c.Chicken)
                .HasForeignKey<SystemConfig>(e => e.EntityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Chicken_ChickenId_fkey");

            entity.HasOne(d => d.ChickenType).WithMany(p => p.Chickens)
                    .HasForeignKey(d => d.ChickenTypeId)
                    .HasConstraintName("Chicken_ChickenTypeId_fkey");
        });

        modelBuilder.Entity<ChickenBatch>(entity =>
        {
            entity.HasKey(e => e.ChickenBatchId).HasName("ChickenBatch_pkey");

            entity.ToTable("ChickenBatch");

            entity.Property(e => e.ChickenBatchId).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.ChickenBatchName).HasColumnType("character varying");
            entity.Property(e => e.EndDate).HasColumnType("timestamp(6) without time zone");
            entity.Property(e => e.StartDate).HasColumnType("timestamp(6) without time zone");
            entity.Property(e => e.Status).HasDefaultValue(1);

            //entity.HasOne(d => d.Chicken).WithMany(p => p.ChickenBatches)
            //    .HasForeignKey(d => d.ChickenId)
            //    .HasConstraintName("Chicken_ChickenId_fkey");

            entity.HasOne(d => d.ChickenCoop).WithMany(p => p.ChickenBatches)
                .HasForeignKey(d => d.ChickenCoopId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("ChickenBatch_ChickenCoopId_fkey");

            entity.HasOne(d => d.CurrentStage).WithMany(p => p.ChickenBatches)
                .HasForeignKey(d => d.CurrentStageId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("ChickenBatch_CurrentStageId_fkey");
        });

        modelBuilder.Entity<ChickenCoop>(entity =>
        {
            entity.HasKey(e => e.ChickenCoopId).HasName("ChickenCoop_pkey");

            entity.ToTable("ChickenCoop");

            entity.Property(e => e.ChickenCoopId).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.ChickenCoopCode).HasColumnType("character varying");
            entity.Property(e => e.ChickenCoopName).HasColumnType("character varying");
            entity.Property(e => e.Description).HasColumnType("character varying");
            entity.Property(e => e.Status).HasDefaultValue(1);

            entity.HasOne(d => d.BreedingArea).WithMany(p => p.ChickenCoops)
                .HasForeignKey(d => d.BreedingAreaId)
                .HasConstraintName("ChickenCoop_BreedingAreaId_fkey");

            entity.HasOne(e => e.ChickenCoopNavigation)
                .WithOne(c => c.ChickenCoop)
                .HasForeignKey<SystemConfig>(e => e.EntityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ChickenCoop_ChickenCoopId_fkey");

            entity.HasOne(d => d.DensityUnit).WithMany(p => p.ChickenCoopDensityUnits)
                .HasForeignKey(d => d.DensityUnitId)
                .HasConstraintName("ChickenCoop_DensityUnitId_fkey");

            entity.HasOne(d => d.AreaUnit).WithMany(p => p.ChickenCoopAreaUnits)
                .HasForeignKey(d => d.AreaUnitId)
                .HasConstraintName("ChickenCoop_AreaUnitId_fkey");

            entity.HasOne(d => d.Purpose).WithMany(p => p.ChickenCoopPurposes)
                .HasForeignKey(d => d.PurposeId)
                .HasConstraintName("ChickenCoop_PurposeId_fkey");
        });

        modelBuilder.Entity<ChickenDetail>(entity =>
        {
            entity.HasKey(e => e.ChickenDetailId).HasName("ChickenDetail_pkey");

            entity.ToTable("ChickenDetail");

            entity.Property(e => e.ChickenDetailId).HasDefaultValueSql("gen_random_uuid()");

            entity.HasOne(d => d.Chicken).WithMany(p => p.ChickenDetails)
                .HasForeignKey(d => d.ChickenId)
                .HasConstraintName("ChickenDetail_ChickenId_fkey");

            entity.HasOne(d => d.ChickenBatch).WithMany(p => p.ChickenDetails)
                .HasForeignKey(d => d.ChickenBatchId)
                .HasConstraintName("ChickenDetail_ChickenBatchId_fkey");
        });

        modelBuilder.Entity<CoopEquipment>(entity =>
        {
            entity.HasKey(e => e.CoopEquipmentId).HasName("CoopEquipment_pkey");

            entity.ToTable("CoopEquipment");

            entity.Property(e => e.CoopEquipmentId).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.AssignedDate).HasColumnType("timestamp(6) without time zone");
            entity.Property(e => e.LastMaintenanceDate).HasColumnType("timestamp(6) without time zone");
            entity.Property(e => e.MaintenanceInterval).HasDefaultValue(30);
            entity.Property(e => e.NextMaintenanceDate).HasColumnType("timestamp(6) without time zone");
            entity.Property(e => e.Status).HasDefaultValue(1);

            entity.HasOne(d => d.ChickenCoop).WithMany(p => p.CoopEquipments)
                .HasForeignKey(d => d.ChickenCoopId)
                .HasConstraintName("CoopEquipment_ChickenCoopId_fkey");

            entity.HasOne(d => d.Equipment).WithMany(p => p.CoopEquipments)
                .HasForeignKey(d => d.EquipmentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("CoopEquipment_EquipmentId_fkey");
        });

        modelBuilder.Entity<Equipment>(entity =>
        {
            entity.HasKey(e => e.EquipmentId).HasName("Equipment_pkey");

            entity.Property(e => e.EquipmentId).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.EquipmentCode).HasColumnType("character varying");
            entity.Property(e => e.EquipmentName).HasColumnType("character varying");
            entity.Property(e => e.PurchaseDate).HasColumnType("timestamp(6) without time zone");
            entity.Property(e => e.Usage).HasColumnType("character varying");

            //entity.HasOne(d => d.EquipmentNavigation).WithOne(p => p.Equipment)
            //    .HasForeignKey<Equipment>(d => d.EquipmentId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("Equipment_EquipmentId_fkey");

            entity.HasOne(d => d.SizeUnit).WithMany(p => p.EquipmentSizeUnits)
                .HasForeignKey(d => d.SizeUnitId)
                .HasConstraintName("Equipment_SizeUnitId_fkey");

            entity.HasOne(d => d.WeightUnit).WithMany(p => p.EquipmentWeightUnits)
                .HasForeignKey(d => d.WeightUnitId)
                .HasConstraintName("Equipment_WeightUnitId_fkey");

            entity.HasOne(d => d.Material).WithMany(p => p.EquipmentMaterials)
                .HasForeignKey(d => d.MaterialId)
                .HasConstraintName("Equipment_MaterialId_fkey");
        });

        modelBuilder.Entity<EvaluatedTarget>(entity =>
        {
            entity.HasKey(e => e.EvaluatedTargetId).HasName("EvaluatedTarget_pkey");

            entity.ToTable("EvaluatedTarget");

            entity.Property(e => e.EvaluatedTargetId).HasDefaultValueSql("gen_random_uuid()");

            entity.HasOne(d => d.Target).WithMany(p => p.EvaluatedTargets)
                .HasForeignKey(d => d.TargetId)
                .HasConstraintName("EvaluatedTarget_TargetId_fkey1");

            entity.HasOne(d => d.TargetNavigation).WithMany(p => p.EvaluatedTargets)
                .HasForeignKey(d => d.TargetId)
                .HasConstraintName("EvaluatedTarget_TargetId_fkey");

            entity.HasOne(d => d.TargetType).WithMany(p => p.EvaluatedTargets)
                .HasForeignKey(d => d.TargetTypeId)
                .HasConstraintName("EvaluatedTarget_TargetTypeId_fkey");
        });

        modelBuilder.Entity<EvaluationResult>(entity =>
        {
            entity.HasKey(e => e.EvaluationResultId).HasName("EvaluationResult_pkey");

            entity.ToTable("EvaluationResult");

            entity.Property(e => e.EvaluationResultId).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.EvaluatedDate)
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("EvaluatedDate ");

            entity.HasOne(d => d.EvaluatedTarget).WithMany(p => p.EvaluationResults)
                .HasForeignKey(d => d.EvaluatedTargetId)
                .HasConstraintName("EvaluationResult_EvaluatedTargetId_fkey");

            entity.HasOne(d => d.EvaluationTemplate).WithMany(p => p.EvaluationResults)
                .HasForeignKey(d => d.EvaluationTemplateId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("EvaluationResult_EvaluationTemplateId_fkey");
        });

        modelBuilder.Entity<EvaluationResultDetail>(entity =>
        {
            entity.HasKey(e => e.EvaluationResultDetailId).HasName("EvaluationResultDetail_pkey");

            entity.ToTable("EvaluationResultDetail");

            entity.Property(e => e.EvaluationResultDetailId).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.ActualValue).HasColumnType("character varying");
            entity.Property(e => e.IsPass).HasDefaultValue(0);

            entity.HasOne(d => d.EvaluationResult).WithMany(p => p.EvaluationResultDetails)
                .HasForeignKey(d => d.EvaluationResultId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("EvaluationResultDetail_EvaluationResultId_fkey");
        });

        modelBuilder.Entity<EvaluationTemplate>(entity =>
        {
            entity.HasKey(e => e.EvaluationTemplateId).HasName("EvaluationTemplate_pkey");

            entity.ToTable("EvaluationTemplate");

            entity.Property(e => e.EvaluationTemplateId).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.TemplateName).HasColumnType("character varying");

            entity.HasOne(d => d.TemplateType).WithMany(p => p.EvaluationTemplates)
                .HasForeignKey(d => d.TemplateTypeId)
                .HasConstraintName("EvaluationTemplate_TemplateTypeId_fkey");
        });

        modelBuilder.Entity<Farm>(entity =>
        {
            entity.HasKey(e => e.FarmId).HasName("Farm_pkey");

            entity.ToTable("Farm");

            entity.Property(e => e.FarmId).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.Address).HasColumnType("character varying");
            entity.Property(e => e.FarmCode).HasColumnType("character varying");
            entity.Property(e => e.FarmName).HasColumnType("character varying");
            entity.Property(e => e.ImageUrl).HasColumnType("character varying");
            entity.Property(e => e.PhoneNumber).HasColumnType("character varying");
            entity.Property(e => e.Website).HasColumnType("character varying");

            entity.HasOne(d => d.AreaUnit).WithMany(p => p.FarmAreaUnits)
                .HasForeignKey(d => d.AreaUnitId)
                .HasConstraintName("Farm_AreaUnitId_fkey");
        });

        modelBuilder.Entity<FarmEmployee>(entity =>
        {
            entity.HasKey(e => e.FarmEmployeeId).HasName("FarmEmployee_pkey");

            entity.ToTable("FarmEmployee");

            entity.Property(e => e.FarmEmployeeId).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.EndDate).HasColumnType("timestamp(6) without time zone");
            entity.Property(e => e.StartDate).HasColumnType("timestamp(6) without time zone");
            entity.Property(e => e.Status).HasDefaultValue(1);

            entity.HasOne(d => d.Farm).WithMany(p => p.FarmEmployees)
                .HasForeignKey(d => d.FarmId)
                .HasConstraintName("FarmEmployee_FarmId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.FarmEmployees)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FarmEmployee_UserId_fkey");
        });

        modelBuilder.Entity<FeedLog>(entity =>
        {
            entity.HasKey(e => e.FeedLogId).HasName("FeedLog_pkey");

            entity.ToTable("FeedLog");

            entity.Property(e => e.FeedLogId).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.FeedingDate)
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("FeedingDate  ");
            entity.Property(e => e.Note).HasColumnType("character varying");

            entity.HasOne(d => d.ChickenBatch).WithMany(p => p.FeedLogs)
                .HasForeignKey(d => d.ChickenBatchId)
                .HasConstraintName("FeedLog_ChickenBatchId_fkey");

            entity.HasOne(d => d.Task).WithMany(p => p.FeedLogs)
                .HasForeignKey(d => d.TaskId)
                .HasConstraintName("FeedLog_TaskId_fkey");

            entity.HasOne(d => d.Unit).WithMany(p => p.FeedLogs)
                .HasForeignKey(d => d.UnitId)
                .HasConstraintName("FeedLog_UnitId_fkey");
        });

        modelBuilder.Entity<FeedSession>(entity =>
        {
            entity.HasKey(e => e.FeedSessionId).HasName("FeedSession_pkey");

            entity.ToTable("FeedSession");

            entity.Property(e => e.FeedSessionId).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.EndTime).HasColumnType("time");
            entity.Property(e => e.StartTime).HasColumnType("time");
            entity.Property(e => e.Note).HasColumnType("character varying");

            entity.HasOne(d => d.NutritionPlan).WithMany(p => p.FeedSessions)
                .HasForeignKey(d => d.NutritionPlanId)
                .HasConstraintName("FeedSession_NutritionPlanId_fkey");

            entity.HasOne(d => d.Unit).WithMany(p => p.FeedSessions)
                .HasForeignKey(d => d.UnitId)
                .HasConstraintName("FeedSession_UnitId_fkey");
        });

        modelBuilder.Entity<Food>(entity =>
        {
            entity.HasKey(e => e.FoodId).HasName("Food_pkey");

            entity.ToTable("Food");

            entity.Property(e => e.FoodId).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.ExpiryDate).HasColumnType("timestamp(6) without time zone");
            entity.Property(e => e.FoodCode).HasColumnType("character varying");
            entity.Property(e => e.FoodName).HasColumnType("character varying");
            entity.Property(e => e.Note).HasColumnType("character varying");
            entity.Property(e => e.ProductionDate).HasColumnType("timestamp(6) without time zone");

            //entity.HasOne(d => d.FoodNavigation).WithOne(p => p.Food)
            //    .HasForeignKey<Food>(d => d.FoodId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("Food_FoodId_fkey");
        });

        modelBuilder.Entity<GrowthBatch>(entity =>
        {
            entity.HasKey(e => e.GrowthBatchId).HasName("GrowthBatch_pkey");

            entity.ToTable("GrowthBatch");

            entity.Property(e => e.GrowthBatchId).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.EndDate).HasColumnType("timestamp(6) without time zone");
            entity.Property(e => e.Note).HasColumnType("character varying");
            entity.Property(e => e.StartDate).HasColumnType("timestamp(6) without time zone");
            entity.Property(e => e.Status).HasDefaultValue(1);

            entity.HasOne(d => d.ChickenBatch).WithMany(p => p.GrowthBatches)
                .HasForeignKey(d => d.ChickenBatchId)
                .HasConstraintName("GrowthBatch_ChickenBatchId_fkey");

            entity.HasOne(d => d.GrowthStage).WithMany(p => p.GrowthBatches)
                .HasForeignKey(d => d.GrowthStageId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("GrowthBatch_GrowthStageId_fkey");
        });

        modelBuilder.Entity<GrowthStage>(entity =>
        {
            entity.HasKey(e => e.GrowthStageId).HasName("GrowthStage_pkey");

            entity.ToTable("GrowthStage");

            entity.Property(e => e.GrowthStageId).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.Description).HasColumnType("character varying");
            entity.Property(e => e.StageName).HasColumnType("character varying");

            entity.HasOne(d => d.ChickenTypeNavigation).WithMany(p => p.GrowthStages)
                .HasForeignKey(d => d.ChickenType)
                .HasConstraintName("GrowthStage_ChickenType_fkey");

            entity.HasOne(d => d.NutritionPlan).WithMany(p => p.GrowthStages)
                .HasForeignKey(d => d.NutritionPlanId)
                .HasConstraintName("NutritionPlan_NutritionPlanId_fkey");
        });

        modelBuilder.Entity<HealthLog>(entity =>
        {
            entity.HasKey(e => e.HealthLogId).HasName("HealthLog_pkey");

            entity.ToTable("HealthLog");

            entity.Property(e => e.HealthLogId).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.CheckedAt).HasColumnType("timestamp(6) without time zone");
            entity.Property(e => e.EndDate).HasColumnType("timestamp(6) without time zone");
            entity.Property(e => e.Location).HasColumnType("character varying");
            entity.Property(e => e.Notes).HasColumnType("character varying");
            entity.Property(e => e.StartDate).HasColumnType("timestamp(6) without time zone");

            entity.HasOne(d => d.ChickenBatch).WithMany(p => p.HealthLogs)
                .HasForeignKey(d => d.ChickenBatchId)
                .HasConstraintName("HealthLog_ChickenBatchId_fkey");

            entity.HasOne(d => d.Task).WithMany(p => p.HealthLogs)
                .HasForeignKey(d => d.TaskId)
                .HasConstraintName("HealthLog_TaskId_fkey");
        });

        modelBuilder.Entity<HealthLogDetail>(entity =>
        {
            entity.HasKey(e => e.HealthLogDetailId).HasName("HealthLogDetail_pkey");

            entity.ToTable("HealthLogDetail");

            entity.Property(e => e.HealthLogDetailId).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.Result).HasColumnType("character varying");

            entity.HasOne(d => d.Criteria).WithMany(p => p.HealthLogDetails)
                .HasForeignKey(d => d.CriteriaId)
                .HasConstraintName("HealthLogDetail_CriteriaId_fkey");

            entity.HasOne(d => d.HealthLog).WithMany(p => p.HealthLogDetails)
                .HasForeignKey(d => d.HealthLogId)
                .HasConstraintName("HealthLogDetail_HealthLogId_fkey");
        });

        modelBuilder.Entity<InventoryReceipt>(entity =>
        {
            entity.HasKey(e => e.InventoryReceiptId).HasName("InventoryReceipt_pkey");

            entity.ToTable("InventoryReceipt");

            entity.Property(e => e.InventoryReceiptId).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.ReceiptCodeNumber).HasColumnType("character varying");
            //entity.Property(e => e.Status).HasDefaultValue(1);

            entity.HasOne(d => d.InventoryRequest).WithMany(p => p.InventoryReceipts)
                .HasForeignKey(d => d.InventoryRequestId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("InventoryReceipt_InventoryRequestId_fkey");

            entity.HasOne(d => d.ReceiptType).WithMany(p => p.InventoryReceipts)
                .HasForeignKey(d => d.ReceiptTypeId)
                .HasConstraintName("InventoryReceipt_ReceiptTypeId_fkey");
        });

        modelBuilder.Entity<InventoryReceiptDetail>(entity =>
        {
            entity.HasKey(e => e.InventoryReceiptDetailId).HasName("InventoryReceiptDetail_pkey");

            entity.ToTable("InventoryReceiptDetail");

            entity.Property(e => e.InventoryReceiptDetailId).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.ActualDate).HasColumnType("timestamp(6) without time zone");
            entity.Property(e => e.Note).HasColumnType("character varying");

            entity.HasOne(d => d.InventoryReceipt).WithMany(p => p.InventoryReceiptDetails)
                .HasForeignKey(d => d.InventoryReceiptId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("InventoryReceiptDetail_InventoryReceiptId_fkey");
        });

        modelBuilder.Entity<InventoryRequest>(entity =>
        {
            entity.HasKey(e => e.InventoryRequestId).HasName("InventoryRequest_pkey");

            entity.ToTable("InventoryRequest");

            entity.Property(e => e.InventoryRequestId).HasDefaultValueSql("gen_random_uuid()");

            entity.HasOne(d => d.InventoryRequestType).WithMany(p => p.InventoryRequests)
                .HasForeignKey(d => d.InventoryRequestTypeId)
                .HasConstraintName("InventoryRequest_InventoryRequestTypeId_fkey");

            entity.HasOne(d => d.Request).WithMany(p => p.InventoryRequests)
                .HasForeignKey(d => d.RequestId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("InventoryRequest_RequestId_fkey");

            entity.HasOne(d => d.WareFrom).WithMany(p => p.InventoryRequestWareFroms)
                .HasForeignKey(d => d.WareFromId)
                .HasConstraintName("InventoryRequest_WareFromId_fkey");

            entity.HasOne(d => d.WareTo).WithMany(p => p.InventoryRequestWareTos)
                .HasForeignKey(d => d.WareToId)
                .HasConstraintName("InventoryRequest_WareToId_fkey");
        });

        modelBuilder.Entity<InventoryRequestDetail>(entity =>
        {
            entity.HasKey(e => e.InventoryRequestDetailId).HasName("InventoryRequestDetail_pkey");

            entity.ToTable("InventoryRequestDetail");

            entity.Property(e => e.InventoryRequestDetailId).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.ExpectedDate).HasColumnType("timestamp(6) without time zone");
            entity.Property(e => e.Note).HasColumnType("character varying");
            entity.Property(e => e.Reason).HasColumnType("character varying");

            entity.HasOne(d => d.InventoryRequest).WithMany(p => p.InventoryRequestDetails)
                .HasForeignKey(d => d.InventoryRequestId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("InventoryRequestDetail_InventoryRequestId_fkey");

            entity.HasOne(d => d.Resource).WithMany(p => p.InventoryRequestDetails)
                .HasForeignKey(d => d.ResourceId)
                .HasConstraintName("InventoryRequestDetail_ResourceId_fkey");

            entity.HasOne(d => d.Unit).WithMany(p => p.InventoryRequestDetails)
                .HasForeignKey(d => d.UnitId)
                .HasConstraintName("InventoryRequestDetail_UnitId_fkey");

            entity.HasOne(d => d.ResourceSupplier).WithMany(p => p.InventoryRequestDetails)
                .HasForeignKey(d => d.ResourceSupplierId)
                .HasConstraintName("InventoryRequestDetail_ResourceSupplierId_fkey");
        });

        modelBuilder.Entity<Medicine>(entity =>
        {
            entity.HasKey(e => e.MedicineId).HasName("Medicine_pkey");

            entity.ToTable("Medicine");

            entity.Property(e => e.MedicineId).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.ExpiryDate).HasColumnType("timestamp(6) without time zone");
            entity.Property(e => e.ProductionDate).HasColumnType("timestamp(6) without time zone");
            entity.Property(e => e.StorageCondition).HasColumnType("character varying");
            entity.Property(e => e.Usage).HasColumnType("character varying");

            entity.HasOne(d => d.Disease).WithMany(p => p.Medicines)
                .HasForeignKey(d => d.DiseaseId)
                .HasConstraintName("Medicine_DiseaseId_fkey");

            //entity.HasOne(d => d.MedicineNavigation).WithOne(p => p.Medicine)
            //    .HasForeignKey<Medicine>(d => d.MedicineId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("Medicine_MedicineId_fkey");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("Notification_pkey");

            entity.ToTable("Notification");

            entity.Property(e => e.NotificationId).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.IsRead).HasDefaultValue(0);
            entity.Property(e => e.NotificationName).HasColumnType("character varying");
            entity.Property(e => e.NotificationType).HasColumnType("character varying");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Notification_UserId_fkey");
        });

        modelBuilder.Entity<NutritionPlan>(entity =>
        {
            entity.HasKey(e => e.NutritionPlanId).HasName("NutritionPlan_pkey");

            entity.ToTable("NutritionPlan");

            entity.Property(e => e.NutritionPlanId).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.Description).HasColumnType("character varying");
            entity.Property(e => e.Name).HasColumnType("character varying");
        });

        modelBuilder.Entity<NutritionPlanDetail>(entity =>
        {
            entity.HasKey(e => e.NutritionPlanDetailId).HasName("NutritionPlanDetail_pkey");

            entity.ToTable("NutritionPlanDetail");

            entity.Property(e => e.NutritionPlanDetailId).HasDefaultValueSql("gen_random_uuid()");

            entity.HasOne(d => d.Food).WithMany(p => p.NutritionPlanDetails)
                .HasForeignKey(d => d.FoodId)
                .HasConstraintName("NutritionPlanDetail_FoodId_fkey");

            entity.HasOne(d => d.NutritionPlan).WithMany(p => p.NutritionPlanDetails)
                .HasForeignKey(d => d.NutritionPlanId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("NutritionPlanDetail_NutritionPlanId_fkey");

            entity.HasOne(d => d.Unit).WithMany(p => p.NutritionPlanDetails)
                .HasForeignKey(d => d.UnitId)
                .HasConstraintName("NutritionPlanDetail_UnitId_fkey");
        });

        modelBuilder.Entity<QuantityLog>(entity =>
        {
            entity.HasKey(e => e.QuantityLogId).HasName("QuantityLog_pkey");

            entity.ToTable("QuantityLog");

            entity.Property(e => e.QuantityLogId).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.LogDate).HasColumnType("timestamp(6) without time zone");

            entity.HasOne(d => d.ChickenBatch).WithMany(p => p.QuantityLogs)
                .HasForeignKey(d => d.ChickenBatchId)
                .HasConstraintName("QuantityLog_ChickenBatchId_fkey");
        });

        modelBuilder.Entity<Request>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("Request_pkey");

            entity.ToTable("Request");

            entity.Property(e => e.RequestId).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.ApprovedAt).HasColumnType("timestamp(6) without time zone");
            entity.Property(e => e.Status).HasDefaultValue(0);

            entity.HasOne(d => d.ApprovedBy).WithMany(p => p.Requests)
                .HasForeignKey(d => d.ApprovedById)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Request_ApprovedById_fkey");

            entity.HasOne(d => d.RequestType).WithMany(p => p.Requests)
                .HasForeignKey(d => d.RequestTypeId)
                .HasConstraintName("Request_RequestTypeId_fkey");
        });

        modelBuilder.Entity<Resource>(entity =>
        {
            entity.HasKey(e => e.ResourceId).HasName("Resource_pkey");

            entity.ToTable("Resource");

            entity.Property(e => e.ResourceId).HasDefaultValueSql("gen_random_uuid()");
            //entity.Property(e => e.Description).HasColumnType("character varying");

            entity.HasOne(d => d.Package).WithMany(p => p.ResourcePackages)
                .HasForeignKey(d => d.PackageId)
                .HasConstraintName("Resource_PackageId_fkey");

            entity.HasOne(d => d.ResourceType).WithMany(p => p.ResourceResourceTypes)
                .HasForeignKey(d => d.ResourceTypeId)
                .HasConstraintName("Resource_ResourceTypeId_fkey");

            entity.HasOne(d => d.Unit).WithMany(p => p.ResourceUnits)
                .HasForeignKey(d => d.UnitId)
                .HasConstraintName("Resource_UnitId_fkey");
        });

        modelBuilder.Entity<ResourceSupplier>(entity =>
        {
            entity.HasKey(e => e.ResourceSupplierId).HasName("ResourceSupplier_pkey");

            entity.ToTable("ResourceSupplier");

            entity.Property(e => e.ResourceSupplierId).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.Description).HasColumnType("character varying");

            //entity.HasOne(d => d.PackagePrice).WithMany(p => p.ResourceSupplierPackagePrices)
            //    .HasForeignKey(d => d.PackagePriceId)
            //    .HasConstraintName("ResourceSupplier_PackagePriceId_fkey");

            entity.HasOne(d => d.Resource).WithMany(p => p.ResourceSuppliers)
                .HasForeignKey(d => d.ResourceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ResourceSupplier_ResourceId_fkey");

            entity.HasOne(d => d.Supplier).WithMany(p => p.ResourceSuppliers)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ResourceSupplier_SupplierId_fkey");

            //entity.HasOne(d => d.UnitPrice).WithMany(p => p.ResourceSupplierUnitPrices)
            //    .HasForeignKey(d => d.UnitPriceId)
            //    .HasConstraintName("ResourceSupplier_UnitPriceId_fkey");
        });

        modelBuilder.Entity<RevokedToken>(entity =>
        {
            entity.HasKey(e => e.RevokedTokenId).HasName("RevokedToken_pkey");

            entity.ToTable("RevokedToken");

            entity.Property(e => e.RevokedTokenId).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.ExpiryDate).HasColumnType("timestamp(6) without time zone");
            entity.Property(e => e.RevokedAt).HasColumnType("timestamp(6) without time zone");
            entity.Property(e => e.Token).HasColumnType("character varying");

            entity.HasOne(d => d.User).WithMany(p => p.RevokedTokens)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("RevokedToken_UserId_fkey");
        });

        modelBuilder.Entity<Shift>(entity =>
        {
            entity.HasKey(e => e.ShiftId).HasName("Shift_pkey");

            entity.ToTable("Shift");

            entity.Property(e => e.ShiftId).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.EndTime).HasColumnType("time");
            entity.Property(e => e.StartTime).HasColumnType("time");
            entity.Property(e => e.ShiftName).HasColumnType("character varying");
        });

        modelBuilder.Entity<ShiftSchedule>(entity =>
        {
            entity.HasKey(e => e.ShiftScheduleId).HasName("ShiftSchedule_pkey");

            entity.ToTable("ShiftSchedule");

            entity.Property(e => e.ShiftScheduleId).HasDefaultValueSql("gen_random_uuid()");

            entity.HasOne(d => d.Shift).WithMany(p => p.ShiftSchedules)
                .HasForeignKey(d => d.ShiftId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("ShiftSchedule_ShiftId_fkey");

            entity.HasOne(d => d.Task).WithMany(p => p.ShiftSchedules)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("ShiftSchedule_TaskId_fkey");

        });

        modelBuilder.Entity<SubCategory>(entity =>
        {
            entity.HasKey(e => e.SubCategoryId).HasName("SubCategory_pkey");

            entity.ToTable("SubCategory");

            entity.Property(e => e.SubCategoryId).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.DataType).HasColumnType("character varying");
            entity.Property(e => e.Status).HasDefaultValue(1);
            entity.Property(e => e.SubCategoryName).HasColumnType("character varying");

            entity.HasOne(d => d.Category).WithMany(p => p.SubCategories)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SubCategory_CategoryId_fkey");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("Supplier_pkey");

            entity.ToTable("Supplier");

            entity.Property(e => e.SupplierId).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.Address).HasColumnType("character varying");
            entity.Property(e => e.BankAccount).HasColumnType("character varying");
            entity.Property(e => e.PhoneNumber).HasColumnType("character varying");
            entity.Property(e => e.Status).HasDefaultValue(1);
            entity.Property(e => e.SupplierCode).HasColumnType("character varying");
            entity.Property(e => e.SupplierName).HasColumnType("character varying");
        });

        modelBuilder.Entity<SystemConfig>(entity =>
        {
            entity.HasKey(e => e.SystemConfigId).HasName("SystemConfig_pkey");

            entity.ToTable("SystemConfig");

            entity.Property(e => e.SystemConfigId).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.EffectedDateFrom).HasColumnType("timestamp(6) without time zone");
            entity.Property(e => e.EffectedDateTo).HasColumnType("timestamp(6) without time zone");
            entity.Property(e => e.EntityType).HasColumnType("character varying");
            entity.Property(e => e.SettingName).HasColumnType("character varying");
            entity.Property(e => e.Status).HasDefaultValue(1);
        });

        modelBuilder.Entity<Domain.Entities.Task>(entity =>
        {
            entity.HasKey(e => e.TaskId).HasName("Task_pkey");

            entity.ToTable("Task");

            entity.Property(e => e.TaskId).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.Description).HasColumnType("character varying");
            entity.Property(e => e.TaskName).HasColumnType("character varying");
            entity.Property(e => e.IsHarvest).HasDefaultValue(0);
            entity.Property(e => e.Status).HasDefaultValue(1);
            entity.Property(e => e.StartWorkDate).HasColumnType("timestamp(6) without time zone");
            entity.Property(e => e.EndWorkDate).HasColumnType("timestamp(6) without time zone");

            entity.HasOne(e => e.TaskNavigation)
                .WithOne(c => c.Task)
                .HasForeignKey<SystemConfig>(e => e.EntityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Task_TaskId_fkey");

            entity.HasOne(d => d.TaskType).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.TaskTypeId)
                .HasConstraintName("Task_TaskTypeId_fkey");
        });

        modelBuilder.Entity<TaskHarvest>(entity =>
        {
            entity.HasKey(e => e.TaskHarvestId).HasName("TaskHarvest_pkey");

            entity.ToTable("TaskHarvest");

            entity.Property(e => e.TaskHarvestId).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.Quality).HasColumnType("character varying");

            entity.HasOne(d => d.HarvestType).WithMany(p => p.TaskHarvests)
                .HasForeignKey(d => d.HarvestTypeId)
                .HasConstraintName("TaskHarvest_HarvestTypeId_fkey");

            entity.HasOne(d => d.Task).WithMany(p => p.TaskHarvests)
                .HasForeignKey(d => d.TaskId)
                .HasConstraintName("TaskHarvest_TaskId_fkey");
        });

        modelBuilder.Entity<TaskLocation>(entity =>
        {
            entity.HasKey(e => e.TaskLocationId).HasName("TaskLocation_pkey");

            entity.ToTable("TaskLocation");

            entity.Property(e => e.TaskLocationId).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.LocationType).HasColumnType("character varying");

            entity.HasOne(d => d.Location).WithMany(p => p.TaskLocations)
                .HasForeignKey(d => d.TaskLocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("TaskLocation_LocationId_fkey1");

            entity.HasOne(d => d.Location).WithMany(p => p.TaskLocations)
                .HasForeignKey(d => d.CoopId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("TaskLocation_CoopId_fkey");

            entity.HasOne(d => d.LocationNavigation).WithMany(p => p.TaskLocations)
                .HasForeignKey(d => d.WareId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("TaskLocation_WareId_fkey");

            entity.HasOne(d => d.Task).WithMany(p => p.TaskLocations)
                .HasForeignKey(d => d.TaskId)
                .HasConstraintName("TaskLocation_TaskId_fkey");
        });

        modelBuilder.Entity<TaskLog>(entity =>
        {
            entity.HasKey(e => e.TaskLogId).HasName("TaskLog_pkey");

            entity.ToTable("TaskLog");

            entity.Property(e => e.TaskLogId).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.CompletedAt).HasColumnType("timestamp(6) without time zone");
            entity.Property(e => e.Note).HasColumnType("character varying");

            entity.HasOne(d => d.ChickenCoop).WithMany(p => p.TaskLogs)
                .HasForeignKey(d => d.ChickenCoopId)
                .HasConstraintName("TaskLog_ChickenCoopId_fkey");

            entity.HasOne(d => d.Task).WithMany(p => p.TaskLogs)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TaskLog_TaskId_fkey");
        });

        modelBuilder.Entity<TaskRequest>(entity =>
        {
            entity.HasKey(e => e.TaskRequestId).HasName("TaskRequest_pkey");

            entity.ToTable("TaskRequest");

            entity.Property(e => e.TaskRequestId).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.Description).HasColumnType("character varying");

            entity.HasOne(d => d.Request).WithMany(p => p.TaskRequests)
                .HasForeignKey(d => d.RequestId)
                .HasConstraintName("TaskRequest_RequestId_fkey");

            //entity.HasOne(d => d.TaskType).WithMany(p => p.TaskRequests)
            //    .HasForeignKey(d => d.TaskTypeId)
            //    .HasConstraintName("TaskRequest_TaskTypeId_fkey");
        });

        modelBuilder.Entity<TaskResource>(entity =>
        {
            entity.HasKey(e => e.TaskResourceId).HasName("TaskResource_pkey");

            entity.ToTable("TaskResource");

            entity.Property(e => e.TaskResourceId).HasDefaultValueSql("gen_random_uuid()");

            entity.HasOne(d => d.ResourceType).WithMany(p => p.TaskResources)
                .HasForeignKey(d => d.ResourceTypeId)
                .HasConstraintName("TaskResource_ResourceTypeId_fkey");

            entity.HasOne(d => d.Task).WithMany(p => p.TaskResources)
                .HasForeignKey(d => d.TaskId)
                .HasConstraintName("TaskResource_TaskId_fkey");

            entity.HasOne(d => d.Resource).WithMany(p => p.TaskResources)
                .HasForeignKey(d => d.ResourceId)
                .HasConstraintName("TaskResource_ResourceId_fkey");

            entity.HasOne(d => d.Unit).WithMany(p => p.TaskResourceUnits)
                .HasForeignKey(d => d.UnitId)
                .HasConstraintName("TaskResource_UnitId_fkey");
        });

        //modelBuilder.Entity<FrequencySchedule>(entity =>
        //{
        //    entity.HasKey(e => e.FrequencyScheduleId).HasName("FrequencySchedule_pkey");

        //    entity.ToTable("FrequencySchedule");

        //    entity.Property(e => e.FrequencyScheduleId).HasDefaultValueSql("gen_random_uuid()");
        //    entity.Property(e => e.StartWorkDate).HasColumnType("timestamp(6) without time zone");
        //    entity.Property(e => e.EndWorkDate).HasColumnType("timestamp(6) without time zone");

        //    entity.HasOne(d => d.Task).WithMany(p => p.FrequencySchedules)
        //        .HasForeignKey(d => d.FrequencyScheduleId)
        //        .OnDelete(DeleteBehavior.SetNull)
        //        .HasConstraintName("FrequencySchedule_FrequencyScheduleId_fkey");
        //});

        modelBuilder.Entity<TemplateCriterion>(entity =>
        {
            entity.HasKey(e => e.TemplateCriteriaId).HasName("TemplateCriteria_pkey");

            entity.Property(e => e.TemplateCriteriaId).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.ExpectedCondition).HasColumnType("character varying");
            entity.Property(e => e.ExpectedUnit).HasColumnType("character varying");
            entity.Property(e => e.ExpectedValue).HasColumnType("character varying");
            entity.Property(e => e.TemplateName).HasColumnType("character varying");

            entity.HasOne(d => d.Criteria).WithMany(p => p.TemplateCriteria)
                .HasForeignKey(d => d.CriteriaId)
                .HasConstraintName("TemplateCriteria_CriteriaId_fkey");

            entity.HasOne(d => d.EvaluationTemplate).WithMany(p => p.TemplateCriteria)
                .HasForeignKey(d => d.EvaluationTemplateId)
                .HasConstraintName("TemplateCriteria_EvaluationTemplateId_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("User_pkey");

            entity.ToTable("User");

            entity.Property(e => e.UserId).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.Avatar).HasColumnType("character varying");
            entity.Property(e => e.Cccd)
                .HasColumnType("character varying")
                .HasColumnName("CCCD");
            entity.Property(e => e.FullName).HasColumnType("character varying");
            entity.Property(e => e.GoogleId).HasColumnType("character varying");
            entity.Property(e => e.HashedPassword).HasColumnType("character varying");
            entity.Property(e => e.Mail).HasColumnType("character varying");
            entity.Property(e => e.PhoneNumber).HasColumnType("character varying");
            entity.Property(e => e.Status).HasDefaultValue(1);
        });

        modelBuilder.Entity<VaccineLog>(entity =>
        {
            entity.HasKey(e => e.VaccineLogId).HasName("VaccineLog_pkey");

            entity.ToTable("VaccineLog");

            entity.Property(e => e.VaccineLogId).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.Notes).HasColumnType("character varying");
            entity.Property(e => e.Reaction).HasColumnType("character varying");

            entity.HasOne(d => d.ChickenBatch).WithMany(p => p.VaccineLogs)
                .HasForeignKey(d => d.ChickenBatchId)
                .HasConstraintName("VaccineLog_ChickenBatchId_fkey");

            entity.HasOne(d => d.Task).WithMany(p => p.VaccineLogs)
                .HasForeignKey(d => d.TaskId)
                .HasConstraintName("VaccineLog_TaskId_fkey");
        });

        modelBuilder.Entity<WarePermission>(entity =>
        {
            entity.HasKey(e => e.PermissionId).HasName("WarePermission_pkey");

            entity.ToTable("WarePermission");

            entity.Property(e => e.PermissionId).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.GrantedAt).HasColumnType("timestamp(6) without time zone");
            entity.Property(e => e.PermissionLevel).HasDefaultValue(0);

            entity.HasOne(d => d.User).WithMany(p => p.WarePermissions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("WarePermission_UserId_fkey");

            entity.HasOne(d => d.Ware).WithMany(p => p.WarePermissions)
                .HasForeignKey(d => d.WareId)
                .HasConstraintName("WarePermission_WareId_fkey");
        });

        modelBuilder.Entity<WareStock>(entity =>
        {
            entity.HasKey(e => e.WareStockId).HasName("WareStock_pkey");

            entity.ToTable("WareStock");

            entity.Property(e => e.WareStockId).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.Quantity).HasDefaultValue(0);

            entity.HasOne(d => d.Resource).WithMany(p => p.WareStocks)
                .HasForeignKey(d => d.ResourceId)
                .HasConstraintName("WareStock_ResourceId_fkey");

            entity.HasOne(d => d.Ware).WithMany(p => p.WareStocks)
                .HasForeignKey(d => d.WareId)
                .HasConstraintName("WareStock_WareId_fkey");
        });

        modelBuilder.Entity<WareTransaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("WareTransaction_pkey");

            entity.ToTable("WareTransaction");

            entity.Property(e => e.TransactionId).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.TransactionDate).HasColumnType("timestamp(6) without time zone");

            entity.HasOne(d => d.LocationFrom).WithMany(p => p.WareTransactionLocationFroms)
                .HasForeignKey(d => d.LocationFromId)
                .HasConstraintName("WareTransaction_LocationFromId_fkey");

            entity.HasOne(d => d.LocationTo).WithMany(p => p.WareTransactionLocationTos)
                .HasForeignKey(d => d.LocationToId)
                .HasConstraintName("WareTransaction_LocationToId_fkey");

            entity.HasOne(d => d.TransactionTypeNavigation).WithMany(p => p.WareTransactions)
                .HasForeignKey(d => d.TransactionType)
                .HasConstraintName("WareTransaction_TransactionType_fkey");

            entity.HasOne(d => d.Ware).WithMany(p => p.WareTransactionWares)
                .HasForeignKey(d => d.WareId)
                .HasConstraintName("WareTransaction_WareId_fkey");
        });

        modelBuilder.Entity<Warehouse>(entity =>
        {
            entity.HasKey(e => e.WareId).HasName("Warehouse_pkey");

            entity.ToTable("Warehouse");

            entity.Property(e => e.WareId).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.Status).HasDefaultValue(1);
            entity.Property(e => e.WarehouseName).HasColumnType("character varying");

            entity.HasOne(d => d.Farm).WithMany(p => p.Warehouses)
                .HasForeignKey(d => d.FarmId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Warehouse_FarmId_fkey");

            entity.HasOne(d => d.ResourceType).WithMany(p => p.Warehouses)
                .HasForeignKey(d => d.ResourceTypeId)
                .HasConstraintName("Warehouse_ResourceTypeId_fkey");

            entity.HasOne(e => e.Ware)
                .WithOne(c => c.Warehouse)
                .HasForeignKey<SystemConfig>(e => e.EntityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Warehouse_WareId_fkey");
        });

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                {
                    property.SetColumnType("timestamp without time zone");
                }
            }
        }

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
