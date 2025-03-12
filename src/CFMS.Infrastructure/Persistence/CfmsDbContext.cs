using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;

namespace CFMS.Infrastructure.Persistence;

public partial class CfmsDbContext : DbContext
{
    private readonly ICurrentUserService _currentUserService;

    public CfmsDbContext()
    {
    }

    public CfmsDbContext(DbContextOptions<CfmsDbContext> options, ICurrentUserService currentUserService)
        : base(options)
    {
        _currentUserService = currentUserService;
    }

    public virtual DbSet<Assignment> Assignments { get; set; }

    public virtual DbSet<Attendance> Attendances { get; set; }

    public virtual DbSet<BreedingArea> BreedingAreas { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<ChickenBatch> ChickenBatches { get; set; }

    public virtual DbSet<ChickenCoop> ChickenCoops { get; set; }

    public virtual DbSet<CoopEquipment> CoopEquipments { get; set; }

    public virtual DbSet<DailyTask> DailyTasks { get; set; }

    public virtual DbSet<Equipment> Equipment { get; set; }

    public virtual DbSet<Farm> Farms { get; set; }

    public virtual DbSet<FarmEmployee> FarmEmployees { get; set; }

    public virtual DbSet<FeedSchedule> FeedSchedules { get; set; }

    public virtual DbSet<Flock> Flocks { get; set; }

    public virtual DbSet<FlockNutrition> FlockNutritions { get; set; }

    public virtual DbSet<Food> Foods { get; set; }

    public virtual DbSet<HarvestDetail> HarvestDetails { get; set; }

    public virtual DbSet<HarvestLog> HarvestLogs { get; set; }

    public virtual DbSet<HarvestProduct> HarvestProducts { get; set; }

    public virtual DbSet<HarvestTask> HarvestTasks { get; set; }

    public virtual DbSet<HealthLog> HealthLogs { get; set; }

    public virtual DbSet<HealthLogDetail> HealthLogDetails { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Nutrition> Nutritions { get; set; }

    public virtual DbSet<Performance> Performances { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<QuantityLog> QuantityLogs { get; set; }

    public virtual DbSet<Request> Requests { get; set; }

    public virtual DbSet<RequestDetail> RequestDetails { get; set; }

    public virtual DbSet<RevokedToken> RevokedTokens { get; set; }

    public virtual DbSet<Salary> Salaries { get; set; }

    public virtual DbSet<StockReceipt> StockReceipts { get; set; }

    public virtual DbSet<SubCategory> SubCategories { get; set; }

    public virtual DbSet<Domain.Entities.Task> Tasks { get; set; }

    public virtual DbSet<TaskDetail> TaskDetails { get; set; }

    public virtual DbSet<TaskEvaluation> TaskEvaluations { get; set; }

    public virtual DbSet<TaskLog> TaskLogs { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<VaccinationEmployee> VaccinationEmployees { get; set; }

    public virtual DbSet<VaccinationLog> VaccinationLogs { get; set; }

    public virtual DbSet<Vaccine> Vaccines { get; set; }

    public virtual DbSet<WareTransaction> WareTransactions { get; set; }

    public virtual DbSet<Warehouse> Warehouses { get; set; }

    public virtual DbSet<WarehousePermission> WarehousePermissions { get; set; }

    public virtual DbSet<WarehouseStock> WarehouseStocks { get; set; }

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
            ((EntityAudit)entry.Entity).DeletedWhen = DateTime.UtcNow;
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
                ((EntityAudit)entry.Entity).CreatedWhen = DateTime.UtcNow;
                ((EntityAudit)entry.Entity).CreatedByUserId = currentUserId;
            }

            ((EntityAudit)entry.Entity).LastEditedWhen = DateTime.UtcNow;
            ((EntityAudit)entry.Entity).LastEditedByUserId = currentUserId;
        }
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        //foreach (var entry in ChangeTracker.Entries())
        //{
        //    if (entry is { State: EntityState.Deleted, Entity: ISoftDelete delete })
        //    {
        //        entry.State = EntityState.Modified;
        //        delete.IsDeleted = true;
        //        delete.DeletedWhen = DateTime.UtcNow;
        //    }
        //}
        OnBeforeSaving();
        return base.SaveChangesAsync(cancellationToken);
    }

    public static string GetConnectionString(string connectionStringName)
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

        string? connectionString = config.GetConnectionString(connectionStringName);
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException($"Connection string '{connectionStringName}' is not found in the configuration.");
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

        modelBuilder.Entity<Attendance>()
        .HasOne(a => a.CreatedByUser)
        .WithMany()
        .HasForeignKey(a => a.CreatedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Attendance>()
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

        modelBuilder.Entity<DailyTask>()
        .HasOne(a => a.CreatedByUser)
        .WithMany()
        .HasForeignKey(a => a.CreatedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<DailyTask>()
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

        modelBuilder.Entity<FeedSchedule>()
        .HasOne(a => a.CreatedByUser)
        .WithMany()
        .HasForeignKey(a => a.CreatedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<FeedSchedule>()
        .HasOne(a => a.LastEditedByUser)
        .WithMany()
        .HasForeignKey(a => a.LastEditedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Flock>()
        .HasOne(a => a.CreatedByUser)
        .WithMany()
        .HasForeignKey(a => a.CreatedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Flock>()
        .HasOne(a => a.LastEditedByUser)
        .WithMany()
        .HasForeignKey(a => a.LastEditedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<FlockNutrition>()
        .HasOne(a => a.CreatedByUser)
        .WithMany()
        .HasForeignKey(a => a.CreatedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<FlockNutrition>()
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

        modelBuilder.Entity<HarvestDetail>()
        .HasOne(a => a.CreatedByUser)
        .WithMany()
        .HasForeignKey(a => a.CreatedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<HarvestDetail>()
        .HasOne(a => a.LastEditedByUser)
        .WithMany()
        .HasForeignKey(a => a.LastEditedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<HarvestLog>()
        .HasOne(a => a.CreatedByUser)
        .WithMany()
        .HasForeignKey(a => a.CreatedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<HarvestLog>()
        .HasOne(a => a.LastEditedByUser)
        .WithMany()
        .HasForeignKey(a => a.LastEditedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<HarvestTask>()
        .HasOne(a => a.CreatedByUser)
        .WithMany()
        .HasForeignKey(a => a.CreatedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<HarvestTask>()
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

        modelBuilder.Entity<HealthLogDetail>()
        .HasOne(a => a.CreatedByUser)
        .WithMany()
        .HasForeignKey(a => a.CreatedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<HealthLogDetail>()
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

        modelBuilder.Entity<Nutrition>()
        .HasOne(a => a.CreatedByUser)
        .WithMany()
        .HasForeignKey(a => a.CreatedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Nutrition>()
        .HasOne(a => a.LastEditedByUser)
        .WithMany()
        .HasForeignKey(a => a.LastEditedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Performance>()
        .HasOne(a => a.CreatedByUser)
        .WithMany()
        .HasForeignKey(a => a.CreatedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Performance>()
        .HasOne(a => a.LastEditedByUser)
        .WithMany()
        .HasForeignKey(a => a.LastEditedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Product>()
        .HasOne(a => a.CreatedByUser)
        .WithMany()
        .HasForeignKey(a => a.CreatedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Product>()
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

        modelBuilder.Entity<RequestDetail>()
        .HasOne(a => a.CreatedByUser)
        .WithMany()
        .HasForeignKey(a => a.CreatedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<RequestDetail>()
        .HasOne(a => a.LastEditedByUser)
        .WithMany()
        .HasForeignKey(a => a.LastEditedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Salary>()
        .HasOne(a => a.CreatedByUser)
        .WithMany()
        .HasForeignKey(a => a.CreatedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Salary>()
        .HasOne(a => a.LastEditedByUser)
        .WithMany()
        .HasForeignKey(a => a.LastEditedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<StockReceipt>()
        .HasOne(a => a.CreatedByUser)
        .WithMany()
        .HasForeignKey(a => a.CreatedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<StockReceipt>()
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

        modelBuilder.Entity<TaskDetail>()
        .HasOne(a => a.CreatedByUser)
        .WithMany()
        .HasForeignKey(a => a.CreatedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TaskDetail>()
        .HasOne(a => a.LastEditedByUser)
        .WithMany()
        .HasForeignKey(a => a.LastEditedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TaskEvaluation>()
        .HasOne(a => a.CreatedByUser)
        .WithMany()
        .HasForeignKey(a => a.CreatedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TaskEvaluation>()
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

        modelBuilder.Entity<VaccinationLog>()
        .HasOne(a => a.CreatedByUser)
        .WithMany()
        .HasForeignKey(a => a.CreatedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<VaccinationLog>()
        .HasOne(a => a.LastEditedByUser)
        .WithMany()
        .HasForeignKey(a => a.LastEditedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Vaccine>()
        .HasOne(a => a.CreatedByUser)
        .WithMany()
        .HasForeignKey(a => a.CreatedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Vaccine>()
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

        modelBuilder.Entity<WarehouseStock>()
        .HasOne(a => a.CreatedByUser)
        .WithMany()
        .HasForeignKey(a => a.CreatedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<WarehouseStock>()
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

        modelBuilder.Entity<Flock>()
        .HasOne(a => a.Breed)
        .WithMany()
        .HasForeignKey(a => a.BreedId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Flock>()
        .HasOne(a => a.Purpose)
        .WithMany()
        .HasForeignKey(a => a.PurposeId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Request>()
        .HasOne(a => a.ApprovedByNavigation)
        .WithMany()
        .HasForeignKey(a => a.ApprovedBy)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Assignment>(entity =>
        {
            entity.HasKey(e => e.AssignmentId).HasName("Assignment_pkey");

            entity.ToTable("Assignment");

            entity.Property(e => e.AssignmentId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("assignmentId");
            entity.Property(e => e.AssignedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("assignedDate");
            entity.Property(e => e.CompletedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("completedDate");
            entity.Property(e => e.DeadlineDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deadlineDate");
            entity.Property(e => e.Note).HasColumnName("note");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.TaskId).HasColumnName("taskId");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.Task).WithMany(p => p.Assignments)
                .HasForeignKey(d => d.TaskId)
                .HasConstraintName("Assignment_taskId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Assignments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("Assignment_userId_fkey");
        });

        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.HasKey(e => e.AttendanceId).HasName("Attendance_pkey");

            entity.ToTable("Attendance");

            entity.Property(e => e.AttendanceId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("attendanceId");
            entity.Property(e => e.CheckIn).HasColumnName("checkIn");
            entity.Property(e => e.CheckOut).HasColumnName("checkOut");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.WorkDate).HasColumnName("workDate");

            entity.HasOne(d => d.User).WithMany(p => p.Attendances)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("Attendance_userId_fkey");
        });

        modelBuilder.Entity<BreedingArea>(entity =>
        {
            entity.HasKey(e => e.BreedingAreaId).HasName("BreedingArea_pkey");

            entity.ToTable("BreedingArea");

            entity.Property(e => e.BreedingAreaId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("breedingAreaId");
            entity.Property(e => e.BreedingAreaCode)
                .HasColumnType("character varying")
                .HasColumnName("breedingAreaCode");
            entity.Property(e => e.BreedingAreaName)
                .HasColumnType("character varying")
                .HasColumnName("breedingAreaName");
            entity.Property(e => e.FarmId).HasColumnName("farmId");
            entity.Property(e => e.Image)
                .HasColumnType("character varying")
                .HasColumnName("image");
            entity.Property(e => e.MealsPerDay).HasColumnName("mealsPerDay");
            entity.Property(e => e.Notes).HasColumnName("notes");

            entity.HasOne(d => d.Farm).WithMany(p => p.BreedingAreas)
                .HasForeignKey(d => d.FarmId)
                .HasConstraintName("BreedingArea_farmId_fkey");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("Category_pkey");

            entity.ToTable("Category");

            entity.Property(e => e.CategoryId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("categoryId");
            entity.Property(e => e.CategoryCode)
                .HasColumnType("character varying")
                .HasColumnName("categoryCode");
            entity.Property(e => e.CategoryType)
                .HasColumnType("character varying")
                .HasColumnName("categoryType");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<ChickenBatch>(entity =>
        {
            entity.HasKey(e => e.ChickenBatchId).HasName("ChickenBatch_pkey");

            entity.ToTable("ChickenBatch");

            entity.Property(e => e.ChickenBatchId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("chickenBatchId");
            entity.Property(e => e.ChickenCoopId).HasColumnName("chickenCoopId");
            entity.Property(e => e.EndDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("endDate");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Note).HasColumnName("note");
            entity.Property(e => e.StartDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("startDate");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.ChickenCoop).WithMany(p => p.ChickenBatches)
                .HasForeignKey(d => d.ChickenCoopId)
                .HasConstraintName("ChickenBatch_chickenCoopId_fkey");
        });

        modelBuilder.Entity<ChickenCoop>(entity =>
        {
            entity.HasKey(e => e.ChickenCoopId).HasName("ChickenCoop_pkey");

            entity.ToTable("ChickenCoop");

            entity.Property(e => e.ChickenCoopId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("chickenCoopId");
            entity.Property(e => e.Area).HasColumnName("area");
            entity.Property(e => e.BreedingAreaId).HasColumnName("breedingAreaId");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.ChickenCoopCode)
                .HasColumnType("character varying")
                .HasColumnName("chickenCoopCode");
            entity.Property(e => e.ChickenCoopName)
                .HasColumnType("character varying")
                .HasColumnName("chickenCoopName");
            entity.Property(e => e.Description)
                .HasColumnType("character varying")
                .HasColumnName("description");
            entity.Property(e => e.PurposeId).HasColumnName("purposeId");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.BreedingArea).WithMany(p => p.ChickenCoops)
                .HasForeignKey(d => d.BreedingAreaId)
                .HasConstraintName("ChickenCoop_breedingAreaId_fkey");

            entity.HasOne(d => d.Purpose).WithMany(p => p.ChickenCoops)
                .HasForeignKey(d => d.PurposeId)
                .HasConstraintName("ChickenCoop_purposeId_fkey");
        });

        modelBuilder.Entity<CoopEquipment>(entity =>
        {
            entity.HasKey(e => e.CoopEquipmentId).HasName("CoopEquipment_pkey");

            entity.ToTable("CoopEquipment");

            entity.Property(e => e.CoopEquipmentId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("coopEquipmentId");
            entity.Property(e => e.AssignedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("assignedDate");
            entity.Property(e => e.ChickenCoopId).HasColumnName("chickenCoopId");
            entity.Property(e => e.EquipmentId).HasColumnName("equipmentId");
            entity.Property(e => e.MaintainDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("maintainDate");
            entity.Property(e => e.Note).HasColumnName("note");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.ChickenCoop).WithMany(p => p.CoopEquipments)
                .HasForeignKey(d => d.ChickenCoopId)
                .HasConstraintName("CoopEquipment_chickenCoopId_fkey");

            entity.HasOne(d => d.Equipment).WithMany(p => p.CoopEquipments)
                .HasForeignKey(d => d.EquipmentId)
                .HasConstraintName("CoopEquipment_equipmentId_fkey");
        });

        modelBuilder.Entity<DailyTask>(entity =>
        {
            entity.HasKey(e => e.DTaskId).HasName("DailyTask_pkey");

            entity.ToTable("DailyTask");

            entity.Property(e => e.DTaskId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("dTaskId");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.ItemId).HasColumnName("itemId");
            entity.Property(e => e.TaskDate).HasColumnName("taskDate");
            entity.Property(e => e.TaskId).HasColumnName("taskId");

            entity.HasOne(d => d.Task).WithMany(p => p.DailyTasks)
                .HasForeignKey(d => d.TaskId)
                .HasConstraintName("DailyTask_taskId_fkey");
        });

        modelBuilder.Entity<Equipment>(entity =>
        {
            entity.HasKey(e => e.EquipmentId).HasName("Equipment_pkey");

            entity.HasIndex(e => e.ProductId, "Equipment_productId_key").IsUnique();

            entity.Property(e => e.EquipmentId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("equipmentId");
            entity.Property(e => e.Cost).HasColumnName("cost");
            entity.Property(e => e.EndDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("endDate");
            entity.Property(e => e.EquipmentCode)
                .HasColumnType("character varying")
                .HasColumnName("equipmentCode");
            entity.Property(e => e.EquipmentName)
                .HasColumnType("character varying")
                .HasColumnName("equipmentName");
            entity.Property(e => e.ProductId)
                .IsRequired()
                .HasColumnName("productId");
            entity.Property(e => e.PurchaseDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("purchaseDate");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Specifications)
                .HasColumnType("character varying")
                .HasColumnName("specifications");
            entity.Property(e => e.StartDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("startDate");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.WarrantyPeriod).HasColumnName("warrantyPeriod");
        });

        modelBuilder.Entity<Farm>(entity =>
        {
            entity.HasKey(e => e.FarmId).HasName("Farm_pkey");

            entity.ToTable("Farm");

            entity.Property(e => e.FarmId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("farmId");
            entity.Property(e => e.Address)
                .HasColumnType("character varying")
                .HasColumnName("address");
            entity.Property(e => e.Area).HasColumnName("area");
            entity.Property(e => e.FarmCode)
                .HasColumnType("character varying")
                .HasColumnName("farmCode");
            entity.Property(e => e.FarmImage)
                .HasColumnType("character varying")
                .HasColumnName("farmImage");
            entity.Property(e => e.FarmName)
                .HasColumnType("character varying")
                .HasColumnName("farmName");
            entity.Property(e => e.PhoneNumber)
                .HasColumnType("character varying")
                .HasColumnName("phoneNumber");
            entity.Property(e => e.Scale).HasColumnName("scale");
            entity.Property(e => e.Website)
                .HasColumnType("character varying")
                .HasColumnName("website");
        });

        modelBuilder.Entity<FarmEmployee>(entity =>
        {
            entity.HasKey(e => e.FarmEmployeeId).HasName("FarmEmployee_pkey");

            entity.ToTable("FarmEmployee");

            entity.Property(e => e.FarmEmployeeId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("farmEmployeeId");
            entity.Property(e => e.EmployeeId).HasColumnName("employeeId");
            entity.Property(e => e.EndDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("endDate");
            entity.Property(e => e.FarmId).HasColumnName("farmId");
            entity.Property(e => e.FarmRole)
                .HasConversion<int>()
                .HasColumnType("int")
                .HasColumnName("farmRole");
            entity.Property(e => e.StartDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("startDate");
            entity.Property(e => e.Status)
                .HasConversion<int>()
                .HasColumnType("int")
                .HasColumnName("status");
            entity.HasOne(d => d.Employee).WithMany(p => p.FarmEmployees)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FarmEmployee_employeeId_fkey");

            entity.HasOne(d => d.Farm).WithMany(p => p.FarmEmployees)
                .HasForeignKey(d => d.FarmId)
                .HasConstraintName("FarmEmployee_farmId_fkey");
        });

        modelBuilder.Entity<FeedSchedule>(entity =>
        {
            entity.HasKey(e => e.FeedScheduleId).HasName("FeedSchedule_pkey");

            entity.ToTable("FeedSchedule");

            entity.Property(e => e.FeedScheduleId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("feedScheduleId");
            entity.Property(e => e.FeedAmount).HasColumnName("feedAmount");
            entity.Property(e => e.FeedTime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("feedTime");
            entity.Property(e => e.Notes).HasColumnName("notes");
        });

        modelBuilder.Entity<Flock>(entity =>
        {
            entity.HasKey(e => e.FlockId).HasName("Flock_pkey");

            entity.ToTable("Flock");

            entity.Property(e => e.FlockId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("flockId");
            entity.Property(e => e.AvgWeight).HasColumnName("avgWeight");
            entity.Property(e => e.BreedId).HasColumnName("breedId");
            entity.Property(e => e.ChickenBatchId).HasColumnName("chickenBatchId");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.EndDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("endDate");
            entity.Property(e => e.Gender)
                .HasColumnType("character varying")
                .HasColumnName("gender");
            entity.Property(e => e.LastHealthCheck)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("lastHealthCheck");
            entity.Property(e => e.MortalityRate).HasColumnName("mortalityRate");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.PurposeId).HasColumnName("purposeId");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.StartDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("startDate");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.Breed).WithMany(p => p.FlockBreeds)
                .HasForeignKey(d => d.BreedId)
                .HasConstraintName("Flock_breedId_fkey");

            entity.HasOne(d => d.ChickenBatch).WithMany(p => p.Flocks)
                .HasForeignKey(d => d.ChickenBatchId)
                .HasConstraintName("Flock_chickenBatchId_fkey");

            entity.HasOne(d => d.Purpose).WithMany(p => p.FlockPurposes)
                .HasForeignKey(d => d.PurposeId)
                .HasConstraintName("Flock_purposeId_fkey");
        });

        modelBuilder.Entity<FlockNutrition>(entity =>
        {
            entity.HasKey(e => e.FlockNutritionId).HasName("FlockNutrition_pkey");

            entity.ToTable("FlockNutrition");

            entity.Property(e => e.FlockNutritionId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("flockNutritionId");
            entity.Property(e => e.EndDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("endDate");
            entity.Property(e => e.FlockId).HasColumnName("flockId");
            entity.Property(e => e.NutritionId).HasColumnName("nutritionId");
            entity.Property(e => e.StartDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("startDate");

            entity.HasOne(d => d.Flock).WithMany(p => p.FlockNutritions)
                .HasForeignKey(d => d.FlockId)
                .HasConstraintName("FlockNutrition_flockId_fkey");

            entity.HasOne(d => d.Nutrition).WithMany(p => p.FlockNutritions)
                .HasForeignKey(d => d.NutritionId)
                .HasConstraintName("FlockNutrition_nutritionId_fkey");
        });

        modelBuilder.Entity<Food>(entity =>
        {
            entity.HasKey(e => e.FoodId).HasName("Food_pkey");

            entity.ToTable("Food");

            entity.HasIndex(e => e.ProductId, "Food_productId_key").IsUnique();

            entity.Property(e => e.FoodId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("foodId");
            entity.Property(e => e.ExpiryDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("expiryDate");
            entity.Property(e => e.Ingredients)
                .HasColumnType("character varying")
                .HasColumnName("ingredients");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Notes)
                .HasColumnType("character varying")
                .HasColumnName("notes");
            entity.Property(e => e.ProductId)
                .IsRequired()
                .HasColumnName("productId");
            entity.Property(e => e.ProductionDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("productionDate");
        });

        modelBuilder.Entity<HarvestDetail>(entity =>
        {
            entity.HasKey(e => e.HarvestDetailId).HasName("HarvestDetail_pkey");

            entity.ToTable("HarvestDetail");

            entity.Property(e => e.HarvestDetailId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("harvestDetailId");
            entity.Property(e => e.HarvestLogId).HasColumnName("harvestLogId");
            entity.Property(e => e.Note).HasColumnName("note");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.TypeProductId).HasColumnName("typeProductId");

            entity.HasOne(d => d.HarvestLog).WithMany(p => p.HarvestDetails)
                .HasForeignKey(d => d.HarvestLogId)
                .HasConstraintName("HarvestDetail_harvestLogId_fkey");

            entity.HasOne(d => d.TypeProduct).WithMany(p => p.HarvestDetails)
                .HasForeignKey(d => d.TypeProductId)
                .HasConstraintName("HarvestDetail_typeProductId_fkey");
        });

        modelBuilder.Entity<HarvestLog>(entity =>
        {
            entity.HasKey(e => e.HarvestLogId).HasName("HarvestLog_pkey");

            entity.ToTable("HarvestLog");

            entity.Property(e => e.HarvestLogId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("harvestLogId");
            entity.Property(e => e.ChickenCoopId).HasColumnName("chickenCoopId");
            entity.Property(e => e.Date)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date");
            entity.Property(e => e.Note).HasColumnName("note");
            entity.Property(e => e.Total).HasColumnName("total");
            entity.Property(e => e.Type)
                .HasColumnType("character varying")
                .HasColumnName("type");

            entity.HasOne(d => d.ChickenCoop).WithMany(p => p.HarvestLogs)
                .HasForeignKey(d => d.ChickenCoopId)
                .HasConstraintName("HarvestLog_chickenCoopId_fkey");
        });

        modelBuilder.Entity<HarvestProduct>(entity =>
        {
            entity.HasKey(e => e.HarvestProductId).HasName("HarvestProduct_pkey");

            entity.ToTable("HarvestProduct");

            entity.HasIndex(e => e.ProductId, "HarvestProduct_productId_key").IsUnique();

            entity.Property(e => e.HarvestProductId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("harvestProductId");
            entity.Property(e => e.HarvestProductName)
                .HasColumnType("character varying")
                .HasColumnName("harvestProductName");
            entity.Property(e => e.Note).HasColumnName("note");
            entity.Property(e => e.ProductId)
                .IsRequired()
                .HasColumnName("productId");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.UnitId).HasColumnName("unitId");

            entity.HasOne(d => d.Unit).WithMany(p => p.HarvestProducts)
                .HasForeignKey(d => d.UnitId)
                .HasConstraintName("HarvestProduct_unitId_fkey");
        });

        modelBuilder.Entity<HarvestTask>(entity =>
        {
            entity.HasKey(e => e.HTaskId).HasName("HarvestTask_pkey");

            entity.ToTable("HarvestTask");

            entity.Property(e => e.HTaskId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("hTaskId");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.HarvestDate).HasColumnName("harvestDate");
            entity.Property(e => e.HarvestType)
                .HasColumnType("character varying")
                .HasColumnName("harvestType");
            entity.Property(e => e.QuantityTypeId).HasColumnName("quantityTypeId");
            entity.Property(e => e.TaskId).HasColumnName("taskId");

            entity.HasOne(d => d.QuantityType).WithMany(p => p.HarvestTasks)
                .HasForeignKey(d => d.QuantityTypeId)
                .HasConstraintName("HarvestTask_quantityTypeId_fkey");

            entity.HasOne(d => d.Task).WithMany(p => p.HarvestTasks)
                .HasForeignKey(d => d.TaskId)
                .HasConstraintName("HarvestTask_taskId_fkey");
        });

        modelBuilder.Entity<HealthLog>(entity =>
        {
            entity.HasKey(e => e.HLogId).HasName("HealthLog_pkey");

            entity.ToTable("HealthLog");

            entity.Property(e => e.HLogId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("hLogId");
            entity.Property(e => e.EndDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("endDate");
            entity.Property(e => e.FlockId).HasColumnName("flockId");
            entity.Property(e => e.Humidity).HasColumnName("humidity");
            entity.Property(e => e.Location)
                .HasColumnType("character varying")
                .HasColumnName("location");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.StaffId).HasColumnName("staffId");
            entity.Property(e => e.StartDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("startDate");
            entity.Property(e => e.Temperature).HasColumnName("temperature");

            entity.HasOne(d => d.Flock).WithMany(p => p.HealthLogs)
                .HasForeignKey(d => d.FlockId)
                .HasConstraintName("HealthLog_flockId_fkey");
        });

        modelBuilder.Entity<HealthLogDetail>(entity =>
        {
            entity.HasKey(e => e.LogDetailId).HasName("HealthLogDetail_pkey");

            entity.ToTable("HealthLogDetail");

            entity.Property(e => e.LogDetailId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("logDetailId");
            entity.Property(e => e.CheckedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("checkedAt");
            entity.Property(e => e.CriteriaId).HasColumnName("criteriaId");
            entity.Property(e => e.HLogId).HasColumnName("hLogId");
            entity.Property(e => e.Result).HasColumnName("result");

            entity.HasOne(d => d.Criteria).WithMany(p => p.HealthLogDetails)
                .HasForeignKey(d => d.CriteriaId)
                .HasConstraintName("HealthLogDetail_criteriaId_fkey");

            entity.HasOne(d => d.HLog).WithMany(p => p.HealthLogDetails)
                .HasForeignKey(d => d.HLogId)
                .HasConstraintName("HealthLogDetail_hLogId_fkey");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("Notification_pkey");

            entity.ToTable("Notification");

            entity.Property(e => e.NotificationId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("notificationId");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.IsRead).HasColumnName("isRead");
            entity.Property(e => e.NotificationName)
                .HasColumnType("character varying")
                .HasColumnName("notificationName");
            entity.Property(e => e.Type)
                .HasColumnType("character varying")
                .HasColumnName("type");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("Notification_userId_fkey");
        });

        modelBuilder.Entity<Nutrition>(entity =>
        {
            entity.HasKey(e => e.NutritionId).HasName("Nutrition_pkey");

            entity.ToTable("Nutrition");

            entity.Property(e => e.NutritionId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("nutritionId");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.DevelopmentStage)
                .HasColumnType("character varying")
                .HasColumnName("developmentStage");
            entity.Property(e => e.FeedScheduleId).HasColumnName("feedScheduleId");
            entity.Property(e => e.FoodId).HasColumnName("foodId");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.TargetAudience)
                .HasColumnType("character varying")
                .HasColumnName("targetAudience");

            entity.HasOne(d => d.FeedSchedule).WithMany(p => p.Nutritions)
                .HasForeignKey(d => d.FeedScheduleId)
                .HasConstraintName("Nutrition_feedScheduleId_fkey");

            entity.HasOne(d => d.Food).WithMany(p => p.Nutritions)
                .HasForeignKey(d => d.FoodId)
                .HasConstraintName("Nutrition_foodId_fkey");
        });

        modelBuilder.Entity<Performance>(entity =>
        {
            entity.HasKey(e => e.PerId).HasName("Performance_pkey");

            entity.ToTable("Performance");

            entity.Property(e => e.PerId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("perId");
            entity.Property(e => e.CompletedTask).HasColumnName("completedTask");
            entity.Property(e => e.CompletionRate).HasColumnName("completionRate");
            entity.Property(e => e.DelayTask).HasColumnName("delayTask");
            entity.Property(e => e.Note).HasColumnName("note");
            entity.Property(e => e.PerformanceRating).HasColumnName("performanceRating");
            entity.Property(e => e.RangeTime)
                .HasColumnType("character varying")
                .HasColumnName("rangeTime");
            entity.Property(e => e.TotalTask).HasColumnName("totalTask");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.Performances)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("Performance_userId_fkey");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("Product_pkey");

            entity.ToTable("Product");

            entity.Property(e => e.ProductId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("productId");
            entity.Property(e => e.Package)
                .HasColumnType("character varying")
                .HasColumnName("package");
            entity.Property(e => e.ProductTypeId).HasColumnName("productTypeId");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Unit)
                .HasColumnType("character varying")
                .HasColumnName("unit");
            entity.Property(e => e.Usage).HasColumnName("usage");

            entity.HasOne(d => d.ProductNavigation).WithOne(p => p.Product)
                .HasPrincipalKey<Equipment>(p => p.ProductId)
                .HasForeignKey<Product>(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Product_productId_fkey1");

            entity.HasOne(d => d.Product1).WithOne(p => p.Product)
                .HasPrincipalKey<Food>(p => p.ProductId)
                .HasForeignKey<Product>(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Product_productId_fkey3");

            entity.HasOne(d => d.Product2).WithOne(p => p.Product)
                .HasPrincipalKey<HarvestProduct>(p => p.ProductId)
                .HasForeignKey<Product>(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Product_productId_fkey");

            entity.HasOne(d => d.Product3).WithOne(p => p.Product)
                .HasPrincipalKey<Vaccine>(p => p.ProductId)
                .HasForeignKey<Product>(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Product_productId_fkey2");

            entity.HasOne(d => d.ProductType).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductTypeId)
                .HasConstraintName("Product_productTypeId_fkey");
        });

        modelBuilder.Entity<QuantityLog>(entity =>
        {
            entity.HasKey(e => e.QLogId).HasName("QuantityLog_pkey");

            entity.ToTable("QuantityLog");

            entity.Property(e => e.QLogId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("qLogId");
            entity.Property(e => e.FlockId).HasColumnName("flockId");
            entity.Property(e => e.Img)
                .HasColumnType("character varying")
                .HasColumnName("img");
            entity.Property(e => e.LogDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("logDate");
            entity.Property(e => e.LogType)
                .HasColumnType("character varying")
                .HasColumnName("logType");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.ReasonId).HasColumnName("reasonId");

            entity.HasOne(d => d.Flock).WithMany(p => p.QuantityLogs)
                .HasForeignKey(d => d.FlockId)
                .HasConstraintName("QuantityLog_flockId_fkey");

            entity.HasOne(d => d.Reason).WithMany(p => p.QuantityLogs)
                .HasForeignKey(d => d.ReasonId)
                .HasConstraintName("QuantityLog_reasonId_fkey");
        });

        modelBuilder.Entity<Request>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("Request_pkey");

            entity.ToTable("Request");

            entity.Property(e => e.RequestId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("requestId");
            entity.Property(e => e.ApprovedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("approvedAt");
            entity.Property(e => e.ApprovedBy).HasColumnName("approvedBy");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IsEmergency).HasColumnName("isEmergency");
            entity.Property(e => e.RequestTypeId).HasColumnName("requestTypeId");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.ApprovedByNavigation).WithMany(p => p.RequestApprovedByNavigations)
                .HasForeignKey(d => d.ApprovedBy)
                .HasConstraintName("Request_approvedBy_fkey");

            entity.HasOne(d => d.RequestType).WithMany(p => p.Requests)
                .HasForeignKey(d => d.RequestTypeId)
                .HasConstraintName("Request_requestTypeId_fkey");
        });

        modelBuilder.Entity<RequestDetail>(entity =>
        {
            entity.HasKey(e => e.DetailId).HasName("RequestDetails_pkey");

            entity.Property(e => e.DetailId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("detailId");
            entity.Property(e => e.ExpectedQuantity).HasColumnName("expectedQuantity");
            entity.Property(e => e.ItemId).HasColumnName("itemId");
            entity.Property(e => e.LocationFrom)
                .HasColumnType("character varying")
                .HasColumnName("locationFrom");
            entity.Property(e => e.LocationTo)
                .HasColumnType("character varying")
                .HasColumnName("locationTo");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.RequestId).HasColumnName("requestId");

            entity.HasOne(d => d.Request).WithMany(p => p.RequestDetails)
                .HasForeignKey(d => d.RequestId)
                .HasConstraintName("RequestDetails_requestId_fkey");
        });

        modelBuilder.Entity<Salary>(entity =>
        {
            entity.HasKey(e => e.SalaryId).HasName("Salary_pkey");

            entity.ToTable("Salary");

            entity.Property(e => e.SalaryId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("salaryId");
            entity.Property(e => e.BasicSalary).HasColumnName("basicSalary");
            entity.Property(e => e.Bonus).HasColumnName("bonus");
            entity.Property(e => e.Deduction).HasColumnName("deduction");
            entity.Property(e => e.Final).HasColumnName("final");
            entity.Property(e => e.OverTimeHours).HasColumnName("overTimeHours");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.TotalHoursWorked).HasColumnName("totalHoursWorked");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.Salaries)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("Salary_userId_fkey");
        });

        modelBuilder.Entity<StockReceipt>(entity =>
        {
            entity.HasKey(e => e.StockRepId).HasName("StockReceipt_pkey");

            entity.ToTable("StockReceipt");

            entity.Property(e => e.StockRepId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("stockRepId");
            entity.Property(e => e.ActualQuantity).HasColumnName("actualQuantity");
            entity.Property(e => e.DetailId).HasColumnName("detailId");
            entity.Property(e => e.ItemType)
                .HasColumnType("character varying")
                .HasColumnName("itemType");
            entity.Property(e => e.LocationFrom)
                .HasColumnType("character varying")
                .HasColumnName("locationFrom");
            entity.Property(e => e.LocationTo)
                .HasColumnType("character varying")
                .HasColumnName("locationTo");
            entity.Property(e => e.StockReceiptType)
                .HasColumnType("character varying")
                .HasColumnName("stockReceiptType");

            entity.HasOne(d => d.Detail).WithMany(p => p.StockReceipts)
                .HasForeignKey(d => d.DetailId)
                .HasConstraintName("StockReceipt_detailId_fkey");
        });

        modelBuilder.Entity<SubCategory>(entity =>
        {
            entity.HasKey(e => e.SubCategoryId).HasName("SubCategory_pkey");

            entity.ToTable("SubCategory");

            entity.Property(e => e.SubCategoryId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("subCategoryId");
            entity.Property(e => e.CategoryId).HasColumnName("categoryId");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdDate");
            entity.Property(e => e.DataType)
                .HasColumnType("character varying")
                .HasColumnName("dataType");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.SubCategoryName)
                .HasColumnType("character varying")
                .HasColumnName("subCategoryName");

            entity.HasOne(d => d.Category).WithMany(p => p.SubCategories)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("SubCategory_categoryId_fkey");
        });

        modelBuilder.Entity<Domain.Entities.Task>(entity =>
        {
            entity.HasKey(e => e.TaskId).HasName("Task_pkey");

            entity.ToTable("Task");

            entity.Property(e => e.TaskId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("taskId");
            entity.Property(e => e.Location)
                .HasColumnType("character varying")
                .HasColumnName("location");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.TaskName)
                .HasColumnType("character varying")
                .HasColumnName("taskName");
            entity.Property(e => e.TaskType)
                .HasColumnType("character varying")
                .HasColumnName("taskType");
        });

        modelBuilder.Entity<TaskDetail>(entity =>
        {
            entity.HasKey(e => e.TaskDetailId).HasName("TaskDetail_pkey");

            entity.ToTable("TaskDetail");

            entity.Property(e => e.TaskDetailId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("taskDetailId");
            entity.Property(e => e.Note).HasColumnName("note");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.TaskLogId).HasColumnName("taskLogId");
            entity.Property(e => e.TypeProductId).HasColumnName("typeProductId");

            entity.HasOne(d => d.TaskLog).WithMany(p => p.TaskDetails)
                .HasForeignKey(d => d.TaskLogId)
                .HasConstraintName("TaskDetail_taskLogId_fkey");

            entity.HasOne(d => d.TypeProduct).WithMany(p => p.TaskDetails)
                .HasForeignKey(d => d.TypeProductId)
                .HasConstraintName("TaskDetail_typeProductId_fkey");
        });

        modelBuilder.Entity<TaskEvaluation>(entity =>
        {
            entity.HasKey(e => e.TaskEvalId).HasName("TaskEvaluation_pkey");

            entity.ToTable("TaskEvaluation");

            entity.Property(e => e.TaskEvalId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("taskEvalId");
            entity.Property(e => e.CategoryId).HasColumnName("categoryId");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.FailedCriteria).HasColumnName("failedCriteria");
            entity.Property(e => e.OverallResult)
                .HasColumnType("character varying")
                .HasColumnName("overallResult");
            entity.Property(e => e.PassedCriteria).HasColumnName("passedCriteria");
            entity.Property(e => e.StaffName)
                .HasColumnType("character varying")
                .HasColumnName("staffName");
            entity.Property(e => e.TaskId).HasColumnName("taskId");
            entity.Property(e => e.TaskType)
                .HasColumnType("character varying")
                .HasColumnName("taskType");
            entity.Property(e => e.TotalCriteria).HasColumnName("totalCriteria");

            entity.HasOne(d => d.Category).WithMany(p => p.TaskEvaluations)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("TaskEvaluation_categoryId_fkey");

            entity.HasOne(d => d.Task).WithMany(p => p.TaskEvaluations)
                .HasForeignKey(d => d.TaskId)
                .HasConstraintName("TaskEvaluation_taskId_fkey");
        });

        modelBuilder.Entity<TaskLog>(entity =>
        {
            entity.HasKey(e => e.TaskLogId).HasName("TaskLog_pkey");

            entity.ToTable("TaskLog");

            entity.Property(e => e.TaskLogId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("taskLogId");
            entity.Property(e => e.ChickenCoopId).HasColumnName("chickenCoopId");
            entity.Property(e => e.EndDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("endDate");
            entity.Property(e => e.StartDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("startDate");
            entity.Property(e => e.Type)
                .HasColumnType("character varying")
                .HasColumnName("type");

            entity.HasOne(d => d.ChickenCoop).WithMany(p => p.TaskLogs)
                .HasForeignKey(d => d.ChickenCoopId)
                .HasConstraintName("TaskLog_chickenCoopId_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("User_pkey");

            entity.ToTable("User");

            entity.Property(e => e.UserId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("userId");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.Avatar)
                .HasColumnType("character varying")
                .HasColumnName("avatar");
            entity.Property(e => e.Cccd)
                .HasColumnType("character varying")
                .HasColumnName("CCCD");
            entity.Property(e => e.DateOfBirth)
                .HasColumnName("dateOfBirth");
            entity.Property(e => e.FullName)
                .HasColumnType("character varying")
                .HasColumnName("fullName");
            entity.Property(e => e.Mail)
                .HasColumnType("character varying")
                .HasColumnName("mail");
            entity.Property(e => e.HashedPassword)
                .HasColumnType("character varying")
                .HasColumnName("hashedPassword");
            entity.Property(e => e.PhoneNumber)
                .HasColumnType("character varying")
                .HasColumnName("phoneNumber");
            entity.Property(e => e.SystemRole)
                .HasConversion<int>()
                .HasColumnType("int")
                .HasColumnName("systemRole");
            entity.Property(e => e.CreatedDate)
                .HasColumnName("createdDate");
            entity.Property(e => e.Status)
                .HasConversion<int>()
                .HasColumnType("int")
                .HasColumnName("status");
        });

        modelBuilder.Entity<RevokedToken>(entity =>
        {
            entity.HasKey(e => e.RevokedTokenId).HasName("RevokedToken_pkey");

            entity.ToTable("RevokedToken");

            entity.Property(e => e.RevokedTokenId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("revokedTokenId");

            entity.Property(e => e.Token)
                .HasColumnType("character varying")
                .HasColumnName("token");

            entity.Property(e => e.TokenType)
                .HasColumnName("tokenType");

            entity.Property(e => e.RevokedAt)
                .HasColumnName("revokedAt");

            entity.Property(e => e.ExpiryDate)
                .HasColumnName("expiryDate");

            entity.Property(e => e.UserId)
                .HasColumnName("userId");

            entity.HasOne(rt => rt.User).WithMany(u => u.RevokedTokens)
                .HasForeignKey(rt => rt.UserId)
                .HasConstraintName("RevokedToken_userId_fkey");
        });


        modelBuilder.Entity<VaccinationEmployee>(entity =>
        {
            entity.HasKey(e => e.VaccinationEmployeeId).HasName("VaccinationEmployee_pkey");

            entity.ToTable("VaccinationEmployee");

            entity.Property(e => e.VaccinationEmployeeId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("vaccinationEmployeeId");
            entity.Property(e => e.Employee).HasColumnName("employee");
            entity.Property(e => e.VaccinationLogId).HasColumnName("vaccinationLogId");

            entity.HasOne(d => d.EmployeeNavigation).WithMany(p => p.VaccinationEmployees)
                .HasForeignKey(d => d.Employee)
                .HasConstraintName("VaccinationEmployee_employee_fkey");

            entity.HasOne(d => d.VaccinationLog).WithMany(p => p.VaccinationEmployees)
                .HasForeignKey(d => d.VaccinationLogId)
                .HasConstraintName("VaccinationEmployee_vaccinationLogId_fkey");
        });

        modelBuilder.Entity<VaccinationLog>(entity =>
        {
            entity.HasKey(e => e.VLogId).HasName("VaccinationLog_pkey");

            entity.ToTable("VaccinationLog");

            entity.Property(e => e.VLogId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("vLogId");
            entity.Property(e => e.Dosage)
                .HasColumnType("character varying")
                .HasColumnName("dosage");
            entity.Property(e => e.FlockId).HasColumnName("flockId");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Reaction).HasColumnName("reaction");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.VaccineId).HasColumnName("vaccineId");

            entity.HasOne(d => d.Flock).WithMany(p => p.VaccinationLogs)
                .HasForeignKey(d => d.FlockId)
                .HasConstraintName("VaccinationLog_flockId_fkey");

            entity.HasOne(d => d.Vaccine).WithMany(p => p.VaccinationLogs)
                .HasForeignKey(d => d.VaccineId)
                .HasConstraintName("VaccinationLog_vaccineId_fkey");
        });

        modelBuilder.Entity<Vaccine>(entity =>
        {
            entity.HasKey(e => e.VaccineId).HasName("Vaccine_pkey");

            entity.ToTable("Vaccine");

            entity.HasIndex(e => e.ProductId, "Vaccine_productId_key").IsUnique();

            entity.Property(e => e.VaccineId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("vaccineId");
            entity.Property(e => e.BatchNumber)
                .HasColumnType("character varying")
                .HasColumnName("batchNumber");
            entity.Property(e => e.DiseaseId).HasColumnName("diseaseId");
            entity.Property(e => e.Dosage)
                .HasColumnType("character varying")
                .HasColumnName("dosage");
            entity.Property(e => e.ExpiryDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("expiryDate");
            entity.Property(e => e.Instructions).HasColumnName("instructions");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.ProductId)
                .IsRequired()
                .HasColumnName("productId");
            entity.Property(e => e.ProductionDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("productionDate");
            entity.Property(e => e.SupplierId).HasColumnName("supplierId");

            entity.HasOne(d => d.Disease).WithMany(p => p.VaccineDiseases)
                .HasForeignKey(d => d.DiseaseId)
                .HasConstraintName("Vaccine_diseaseId_fkey");

            entity.HasOne(d => d.Supplier).WithMany(p => p.VaccineSuppliers)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("Vaccine_supplierId_fkey");
        });

        modelBuilder.Entity<WareTransaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("WareTransaction_pkey");

            entity.ToTable("WareTransaction");

            entity.Property(e => e.TransactionId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("transactionId");
            entity.Property(e => e.LocationFrom).HasColumnName("locationFrom");
            entity.Property(e => e.LocationTo).HasColumnName("locationTo");
            entity.Property(e => e.ProductId).HasColumnName("productId");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Reason).HasColumnName("reason");
            entity.Property(e => e.TransactionDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("transactionDate");
            entity.Property(e => e.TransactionType)
                .HasColumnType("character varying")
                .HasColumnName("transactionType");
            entity.Property(e => e.WareId).HasColumnName("wareId");

            entity.HasOne(d => d.Product).WithMany(p => p.WareTransactions)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("WareTransaction_productId_fkey");

            entity.HasOne(d => d.Ware).WithMany(p => p.WareTransactions)
                .HasForeignKey(d => d.WareId)
                .HasConstraintName("WareTransaction_wareId_fkey");
        });

        modelBuilder.Entity<Warehouse>(entity =>
        {
            entity.HasKey(e => e.WareId).HasName("Warehouse_pkey");

            entity.ToTable("Warehouse");

            entity.Property(e => e.WareId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("wareId");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.FarmId).HasColumnName("farmId");
            entity.Property(e => e.MaxCapacity).HasColumnName("maxCapacity");
            entity.Property(e => e.TotalQuantity).HasColumnName("totalQuantity");
            entity.Property(e => e.TotalWeight).HasColumnName("totalWeight");
            entity.Property(e => e.WarehouseName)
                .HasColumnType("character varying")
                .HasColumnName("warehouseName");

            entity.HasOne(d => d.Farm).WithMany(p => p.Warehouses)
                .HasForeignKey(d => d.FarmId)
                .HasConstraintName("Warehouse_farmId_fkey");
        });

        modelBuilder.Entity<WarehousePermission>(entity =>
        {
            entity.HasKey(e => e.PermissionId).HasName("WarehousePermission_pkey");

            entity.ToTable("WarehousePermission");

            entity.Property(e => e.PermissionId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("permissionId");
            entity.Property(e => e.GrantedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("grantedAt");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.WareId).HasColumnName("wareId");

            entity.HasOne(d => d.User).WithMany(p => p.WarehousePermissions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("WarehousePermission_userId_fkey");

            entity.HasOne(d => d.Ware).WithMany(p => p.WarehousePermissions)
                .HasForeignKey(d => d.WareId)
                .HasConstraintName("WarehousePermission_wareId_fkey");
        });

        modelBuilder.Entity<WarehouseStock>(entity =>
        {
            entity.HasKey(e => e.WareStockId).HasName("WarehouseStock_pkey");

            entity.ToTable("WarehouseStock");

            entity.Property(e => e.WareStockId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("wareStockId");
            entity.Property(e => e.ProductId).HasColumnName("productId");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.WareId).HasColumnName("wareId");

            entity.HasOne(d => d.Product).WithMany(p => p.WarehouseStocks)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("WarehouseStock_productId_fkey");

            entity.HasOne(d => d.Ware).WithMany(p => p.WarehouseStocks)
                .HasForeignKey(d => d.WareId)
                .HasConstraintName("WarehouseStock_wareId_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
