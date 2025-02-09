using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CFMS.Domain.Entities;

public partial class CfmsDbContext : DbContext
{
    public CfmsDbContext()
    {
    }

    public CfmsDbContext(DbContextOptions<CfmsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Assignment> Assignments { get; set; }

    public virtual DbSet<Breadingarea> Breadingareas { get; set; }

    public virtual DbSet<Breadingequipment> Breadingequipments { get; set; }

    public virtual DbSet<Breed> Breeds { get; set; }

    public virtual DbSet<Chickenbatch> Chickenbatches { get; set; }

    public virtual DbSet<Dailytask> Dailytasks { get; set; }

    public virtual DbSet<Disease> Diseases { get; set; }

    public virtual DbSet<Equipment> Equipment { get; set; }

    public virtual DbSet<Evaluationcriterion> Evaluationcriteria { get; set; }

    public virtual DbSet<Evaluationsummary> Evaluationsummaries { get; set; }

    public virtual DbSet<Expireddamaged> Expireddamageds { get; set; }

    public virtual DbSet<Exportedproduct> Exportedproducts { get; set; }

    public virtual DbSet<Farm> Farms { get; set; }

    public virtual DbSet<Feedschedule> Feedschedules { get; set; }

    public virtual DbSet<Flock> Flocks { get; set; }

    public virtual DbSet<Food> Foods { get; set; }

    public virtual DbSet<Harvesttask> Harvesttasks { get; set; }

    public virtual DbSet<Healthcriterion> Healthcriteria { get; set; }

    public virtual DbSet<Healthlog> Healthlogs { get; set; }

    public virtual DbSet<Inventoryaudit> Inventoryaudits { get; set; }

    public virtual DbSet<Nutrition> Nutritions { get; set; }

    public virtual DbSet<Performancestatistic> Performancestatistics { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Purpose> Purposes { get; set; }

    public virtual DbSet<Quantitylog> Quantitylogs { get; set; }

    public virtual DbSet<Reason> Reasons { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Salary> Salaries { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    public virtual DbSet<TimeKeeping> TimeKeepings { get; set; }

    public virtual DbSet<TimeKeepingType> TimeKeepingTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Vaccinationlog> Vaccinationlogs { get; set; }

    public virtual DbSet<Vaccine> Vaccines { get; set; }

    public virtual DbSet<Water> Water { get; set; }

    public virtual DbSet<Workschedule> Workschedules { get; set; }

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
        modelBuilder.Entity<Assignment>(entity =>
        {
            entity.HasKey(e => e.Assignmentid).HasName("assignment_pkey");

            entity.ToTable("assignment");

            entity.HasIndex(e => e.Taskid, "IX_assignment_taskid");

            entity.HasIndex(e => e.Userid, "IX_assignment_userid");

            entity.Property(e => e.Assignmentid)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("assignmentid");
            entity.Property(e => e.Assigneddate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("assigneddate");
            entity.Property(e => e.Completeddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("completeddate");
            entity.Property(e => e.Note).HasColumnName("note");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.Taskid).HasColumnName("taskid");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.Task).WithMany(p => p.Assignments)
                .HasForeignKey(d => d.Taskid)
                .HasConstraintName("assignment_taskid_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Assignments)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("assignment_userid_fkey");
        });

        modelBuilder.Entity<Breadingarea>(entity =>
        {
            entity.HasKey(e => e.Breadingareaid).HasName("breadingarea_pkey");

            entity.ToTable("breadingarea");

            entity.HasIndex(e => e.Farmid, "IX_breadingarea_farmid");

            entity.HasIndex(e => e.Breadingareacode, "breadingarea_breadingareacode_key").IsUnique();

            entity.Property(e => e.Breadingareaid)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("breadingareaid");
            entity.Property(e => e.Area)
                .HasPrecision(10, 2)
                .HasColumnName("area");
            entity.Property(e => e.Breadingareacode)
                .HasMaxLength(50)
                .HasColumnName("breadingareacode");
            entity.Property(e => e.Breadingareaname)
                .HasMaxLength(255)
                .HasColumnName("breadingareaname");
            entity.Property(e => e.Breadingpurpose)
                .HasMaxLength(50)
                .HasColumnName("breadingpurpose");
            entity.Property(e => e.Covered)
                .HasDefaultValue(false)
                .HasColumnName("covered");
            entity.Property(e => e.Farmid).HasColumnName("farmid");
            entity.Property(e => e.Humidity)
                .HasPrecision(5, 2)
                .HasColumnName("humidity");
            entity.Property(e => e.Image).HasColumnName("image");
            entity.Property(e => e.Mealsperday).HasColumnName("mealsperday");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.Temperature)
                .HasPrecision(5, 2)
                .HasColumnName("temperature");
            entity.Property(e => e.Weight)
                .HasPrecision(10, 2)
                .HasColumnName("weight");

            entity.HasOne(d => d.Farm).WithMany(p => p.Breadingareas)
                .HasForeignKey(d => d.Farmid)
                .HasConstraintName("breadingarea_farmid_fkey");
        });

        modelBuilder.Entity<Breadingequipment>(entity =>
        {
            entity.HasKey(e => e.Breadingequipmentid).HasName("breadingequipment_pkey");

            entity.ToTable("breadingequipment");

            entity.HasIndex(e => e.Breadingareaid, "IX_breadingequipment_breadingareaid");

            entity.HasIndex(e => e.Equipmentid, "IX_breadingequipment_equipmentid");

            entity.Property(e => e.Breadingequipmentid)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("breadingequipmentid");
            entity.Property(e => e.Assigneddate).HasColumnName("assigneddate");
            entity.Property(e => e.Breadingareaid).HasColumnName("breadingareaid");
            entity.Property(e => e.Equipmentid).HasColumnName("equipmentid");
            entity.Property(e => e.Maintaindate).HasColumnName("maintaindate");
            entity.Property(e => e.Note).HasColumnName("note");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");

            entity.HasOne(d => d.Breadingarea).WithMany(p => p.Breadingequipments)
                .HasForeignKey(d => d.Breadingareaid)
                .HasConstraintName("breadingequipment_breadingareaid_fkey");

            entity.HasOne(d => d.Equipment).WithMany(p => p.Breadingequipments)
                .HasForeignKey(d => d.Equipmentid)
                .HasConstraintName("breadingequipment_equipmentid_fkey");
        });

        modelBuilder.Entity<Breed>(entity =>
        {
            entity.HasKey(e => e.Breedid).HasName("breed_pkey");

            entity.ToTable("breed");

            entity.Property(e => e.Breedid)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("breedid");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Img).HasColumnName("img");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Chickenbatch>(entity =>
        {
            entity.HasKey(e => e.Chickenbatchid).HasName("chickenbatch_pkey");

            entity.ToTable("chickenbatch");

            entity.HasIndex(e => e.Breadingareaid, "IX_chickenbatch_breadingareaid");

            entity.HasIndex(e => e.Flockid, "IX_chickenbatch_flockid");

            entity.Property(e => e.Chickenbatchid)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("chickenbatchid");
            entity.Property(e => e.Breadingareaid).HasColumnName("breadingareaid");
            entity.Property(e => e.Enddate).HasColumnName("enddate");
            entity.Property(e => e.Flockid).HasColumnName("flockid");
            entity.Property(e => e.Note).HasColumnName("note");
            entity.Property(e => e.Numberofchicken).HasColumnName("numberofchicken");
            entity.Property(e => e.Startdate).HasColumnName("startdate");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");

            entity.HasOne(d => d.Breadingarea).WithMany(p => p.Chickenbatches)
                .HasForeignKey(d => d.Breadingareaid)
                .HasConstraintName("chickenbatch_breadingareaid_fkey");

            entity.HasOne(d => d.Flock).WithMany(p => p.Chickenbatches)
                .HasForeignKey(d => d.Flockid)
                .HasConstraintName("chickenbatch_flockid_fkey");
        });

        modelBuilder.Entity<Dailytask>(entity =>
        {
            entity.HasKey(e => e.Dtaskid).HasName("dailytask_pkey");

            entity.ToTable("dailytask");

            entity.HasIndex(e => e.Taskid, "IX_dailytask_taskid");

            entity.Property(e => e.Dtaskid)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("dtaskid");
            entity.Property(e => e.Note).HasColumnName("note");
            entity.Property(e => e.Priority).HasColumnName("priority");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.Taskdate).HasColumnName("taskdate");
            entity.Property(e => e.Taskid).HasColumnName("taskid");

            entity.HasOne(d => d.Task).WithMany(p => p.Dailytasks)
                .HasForeignKey(d => d.Taskid)
                .HasConstraintName("dailytask_taskid_fkey");
        });

        modelBuilder.Entity<Disease>(entity =>
        {
            entity.HasKey(e => e.Diseaseid).HasName("disease_pkey");

            entity.ToTable("disease");

            entity.Property(e => e.Diseaseid)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("diseaseid");
            entity.Property(e => e.Cause).HasColumnName("cause");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Diseasetype).HasColumnName("diseasetype");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Equipment>(entity =>
        {
            entity.HasKey(e => e.Equipmentid).HasName("equipment_pkey");

            entity.ToTable("equipment");

            entity.Property(e => e.Equipmentid)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("equipmentid");
            entity.Property(e => e.Cost)
                .HasPrecision(12, 2)
                .HasColumnName("cost");
            entity.Property(e => e.Equipmentname)
                .HasMaxLength(255)
                .HasColumnName("equipmentname");
            entity.Property(e => e.Note).HasColumnName("note");
            entity.Property(e => e.Purchasedate).HasColumnName("purchasedate");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.Type)
                .HasMaxLength(100)
                .HasColumnName("type");
            entity.Property(e => e.Warrantyperiod).HasColumnName("warrantyperiod");
        });

        modelBuilder.Entity<Evaluationcriterion>(entity =>
        {
            entity.HasKey(e => e.Criteriaid).HasName("evaluationcriteria_pkey");

            entity.ToTable("evaluationcriteria");

            entity.Property(e => e.Criteriaid)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("criteriaid");
            entity.Property(e => e.Criterianame)
                .HasMaxLength(255)
                .HasColumnName("criterianame");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Maxvalue).HasColumnName("maxvalue");
            entity.Property(e => e.Minvalue).HasColumnName("minvalue");
            entity.Property(e => e.Tasktype)
                .HasMaxLength(50)
                .HasColumnName("tasktype");
            entity.Property(e => e.Unit)
                .HasMaxLength(50)
                .HasColumnName("unit");

            entity.HasMany(v => v.EvaluationSummaries)
                .WithMany(u => u.EvaluationCriterions)
                .UsingEntity<Dictionary<string, object>>(
                    "EvaluationSummaryCriteria",
                    j => j.HasOne<Evaluationsummary>().WithMany().HasForeignKey("summaryId"),
                    j => j.HasOne<Evaluationcriterion>().WithMany().HasForeignKey("criteriaId"),
                    j => j.ToTable("evaluation_summary_criteria")
                );
        });

        modelBuilder.Entity<Evaluationsummary>(entity =>
        {
            entity.HasKey(e => e.Summaryid).HasName("evaluationsummary_pkey");

            entity.ToTable("evaluationsummary");

            entity.Property(e => e.Summaryid)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("summaryid");
            entity.Property(e => e.Criteriaid).HasColumnName("criteriaid");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Evaluationdate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("evaluationdate");
            entity.Property(e => e.Failedcriteria).HasColumnName("failedcriteria");
            entity.Property(e => e.Overallresult).HasColumnName("overallresult");
            entity.Property(e => e.Passedcriteria).HasColumnName("passedcriteria");
            entity.Property(e => e.Taskid).HasColumnName("taskid");
            entity.Property(e => e.Tasktype)
                .HasMaxLength(50)
                .HasColumnName("tasktype");
            entity.Property(e => e.Totalcriteria).HasColumnName("totalcriteria");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasMany(v => v.Users)
                .WithMany(u => u.EvaluationSummaries)
                .UsingEntity<Dictionary<string, object>>(
                    "UserSummary",
                    j => j.HasOne<User>().WithMany().HasForeignKey("userId"),
                    j => j.HasOne<Evaluationsummary>().WithMany().HasForeignKey("summaryId"),
                    j => j.ToTable("user_summary_log")
                );

            entity.HasMany(v => v.EvaluationCriterions)
                .WithMany(u => u.EvaluationSummaries)
                .UsingEntity<Dictionary<string, object>>(
                    "EvaluationSummaryCriteria",
                    j => j.HasOne<Evaluationcriterion>().WithMany().HasForeignKey("criteriaId"),
                    j => j.HasOne<Evaluationsummary>().WithMany().HasForeignKey("summaryId"),
                    j => j.ToTable("evaluation_summary_criteria")
                );
        });

        modelBuilder.Entity<Expireddamaged>(entity =>
        {
            entity.HasKey(e => e.Edproductid).HasName("expireddamaged_pkey");

            entity.ToTable("expireddamaged");

            entity.HasIndex(e => e.Productid, "IX_expireddamaged_productid");

            entity.Property(e => e.Edproductid)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("edproductid");
            entity.Property(e => e.Actiontaken).HasColumnName("actiontaken");
            entity.Property(e => e.Productid).HasColumnName("productid");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Reason).HasColumnName("reason");
            entity.Property(e => e.Recorddate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("recorddate");

            entity.HasOne(d => d.Product).WithMany(p => p.Expireddamageds)
                .HasForeignKey(d => d.Productid)
                .HasConstraintName("expireddamaged_productid_fkey");
        });

        modelBuilder.Entity<Exportedproduct>(entity =>
        {
            entity.HasKey(e => e.Eproductid).HasName("exportedproduct_pkey");

            entity.ToTable("exportedproduct");

            entity.HasIndex(e => e.Chickenbatchid, "IX_exportedproduct_chickenbatchid");

            entity.HasIndex(e => e.Farmid, "IX_exportedproduct_farmid");

            entity.HasIndex(e => e.Productid, "IX_exportedproduct_productid");

            entity.HasIndex(e => e.Productcode, "exportedproduct_productcode_key").IsUnique();

            entity.Property(e => e.Eproductid)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("eproductid");
            entity.Property(e => e.Chickenbatchid).HasColumnName("chickenbatchid");
            entity.Property(e => e.Expireddate).HasColumnName("expireddate");
            entity.Property(e => e.Exporteddate).HasColumnName("exporteddate");
            entity.Property(e => e.Farmid).HasColumnName("farmid");
            entity.Property(e => e.Productcode)
                .HasMaxLength(100)
                .HasColumnName("productcode");
            entity.Property(e => e.Productid).HasColumnName("productid");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Reason).HasColumnName("reason");
            entity.Property(e => e.Storagelocation)
                .HasMaxLength(255)
                .HasColumnName("storagelocation");

            entity.HasOne(d => d.Chickenbatch).WithMany(p => p.Exportedproducts)
                .HasForeignKey(d => d.Chickenbatchid)
                .HasConstraintName("exportedproduct_chickenbatchid_fkey");

            entity.HasOne(d => d.Farm).WithMany(p => p.Exportedproducts)
                .HasForeignKey(d => d.Farmid)
                .HasConstraintName("exportedproduct_farmid_fkey");

            entity.HasOne(d => d.Product).WithMany(p => p.Exportedproducts)
                .HasForeignKey(d => d.Productid)
                .HasConstraintName("exportedproduct_productid_fkey");
        });

        modelBuilder.Entity<Farm>(entity =>
        {
            entity.HasKey(e => e.Farmid).HasName("farm_pkey");

            entity.ToTable("farm");

            entity.HasIndex(e => e.Userid, "IX_farm_userid");

            entity.HasIndex(e => e.Farmcode, "farm_farmcode_key").IsUnique();

            entity.Property(e => e.Farmid)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("farmid");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.Area)
                .HasPrecision(10, 2)
                .HasColumnName("area");
            entity.Property(e => e.Farmcode)
                .HasMaxLength(50)
                .HasColumnName("farmcode");
            entity.Property(e => e.Farmimage).HasColumnName("farmimage");
            entity.Property(e => e.Farmname)
                .HasMaxLength(255)
                .HasColumnName("farmname");
            entity.Property(e => e.Phonenumber)
                .HasMaxLength(20)
                .HasColumnName("phonenumber");
            entity.Property(e => e.Scale)
                .HasMaxLength(100)
                .HasColumnName("scale");
            entity.Property(e => e.Type)
                .HasMaxLength(100)
                .HasColumnName("type");
            entity.Property(e => e.Userid).HasColumnName("userid");
            entity.Property(e => e.Website)
                .HasMaxLength(255)
                .HasColumnName("website");

            entity.HasOne(d => d.User).WithMany(p => p.Farms)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("farm_userid_fkey");
        });

        modelBuilder.Entity<Feedschedule>(entity =>
        {
            entity.HasKey(e => e.Feedscheduleid).HasName("feedschedule_pkey");

            entity.ToTable("feedschedule");

            entity.Property(e => e.Feedscheduleid)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("feedscheduleid");
            entity.Property(e => e.Feedamount)
                .HasPrecision(10, 2)
                .HasColumnName("feedamount");
            entity.Property(e => e.Feedtime).HasColumnName("feedtime");
            entity.Property(e => e.Notes).HasColumnName("notes");
        });

        modelBuilder.Entity<Flock>(entity =>
        {
            entity.HasKey(e => e.Flockid).HasName("flock_pkey");

            entity.ToTable("flock");

            entity.HasIndex(e => e.Breedid, "IX_flock_breedid");

            entity.HasIndex(e => e.Purposeid, "IX_flock_purposeid");

            entity.Property(e => e.Flockid)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("flockid");
            entity.Property(e => e.Breedid).HasColumnName("breedid");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Enddate).HasColumnName("enddate");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Purposeid).HasColumnName("purposeid");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Startdate).HasColumnName("startdate");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.Breed).WithMany(p => p.Flocks)
                .HasForeignKey(d => d.Breedid)
                .HasConstraintName("fk_flock_breed");

            entity.HasOne(d => d.Purpose).WithMany(p => p.Flocks)
                .HasForeignKey(d => d.Purposeid)
                .HasConstraintName("fk_flock_purpose");
        });

        modelBuilder.Entity<Food>(entity =>
        {
            entity.HasKey(e => e.Foodid).HasName("food_pkey");

            entity.ToTable("food");

            entity.HasIndex(e => e.Supplierid, "IX_food_supplierid");

            entity.Property(e => e.Foodid)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("foodid");
            entity.Property(e => e.Expirydate).HasColumnName("expirydate");
            entity.Property(e => e.Ingredients).HasColumnName("ingredients");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.Supplierid).HasColumnName("supplierid");
            entity.Property(e => e.Usage).HasColumnName("usage");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Foods)
                .HasForeignKey(d => d.Supplierid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("food_supplierid_fkey");
        });

        modelBuilder.Entity<Harvesttask>(entity =>
        {
            entity.HasKey(e => e.Htaskid).HasName("harvesttask_pkey");

            entity.ToTable("harvesttask");

            entity.HasIndex(e => e.Taskid, "IX_harvesttask_taskid");

            entity.Property(e => e.Htaskid)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("htaskid");
            entity.Property(e => e.Damagedquantity).HasColumnName("damagedquantity");
            entity.Property(e => e.Goodquantity).HasColumnName("goodquantity");
            entity.Property(e => e.Harvestdate).HasColumnName("harvestdate");
            entity.Property(e => e.Harvesttype)
                .HasMaxLength(100)
                .HasColumnName("harvesttype");
            entity.Property(e => e.Note).HasColumnName("note");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.Taskid).HasColumnName("taskid");
            entity.Property(e => e.Totalquantity).HasColumnName("totalquantity");

            entity.HasOne(d => d.Task).WithMany(p => p.Harvesttasks)
                .HasForeignKey(d => d.Taskid)
                .HasConstraintName("harvesttask_taskid_fkey");
        });

        modelBuilder.Entity<Healthcriterion>(entity =>
        {
            entity.HasKey(e => e.Criteriaid).HasName("healthcriteria_pkey");

            entity.ToTable("healthcriteria");

            entity.Property(e => e.Criteriaid)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("criteriaid");
            entity.Property(e => e.Characteristic).HasColumnName("characteristic");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Unit).HasColumnName("unit");
        });

        modelBuilder.Entity<Healthlog>(entity =>
        {
            entity.HasKey(e => e.Hlogid).HasName("healthlog_pkey");

            entity.ToTable("healthlog");

            entity.HasIndex(e => e.Flockid, "IX_healthlog_flockid");

            entity.Property(e => e.Hlogid)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("hlogid");
            entity.Property(e => e.Flockid).HasColumnName("flockid");
            entity.Property(e => e.Logdate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("logdate");
            entity.Property(e => e.Notes).HasColumnName("notes");

            entity.HasOne(d => d.Flock).WithMany(p => p.Healthlogs)
                .HasForeignKey(d => d.Flockid)
                .HasConstraintName("healthlog_flockid_fkey");

            entity.HasMany(h => h.Users)
                .WithMany(u => u.HealthLogs)
                .UsingEntity<Dictionary<string, object>>(
                    "UserHealthLog",
                    j => j.HasOne<User>().WithMany().HasForeignKey("userId"),
                    j => j.HasOne<Healthlog>().WithMany().HasForeignKey("hLogId"),
                    j => j.ToTable("user_health_log")
                );
        });

        modelBuilder.Entity<Inventoryaudit>(entity =>
        {
            entity.HasKey(e => e.Auditid).HasName("inventoryaudit_pkey");

            entity.ToTable("inventoryaudit");

            entity.HasIndex(e => e.Productid, "IX_inventoryaudit_productid");

            entity.HasIndex(e => e.Userid, "IX_inventoryaudit_userid");

            entity.Property(e => e.Auditid)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("auditid");
            entity.Property(e => e.Actualquantity).HasColumnName("actualquantity");
            entity.Property(e => e.Auditdate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("auditdate");
            entity.Property(e => e.Condition).HasColumnName("condition");
            entity.Property(e => e.Productid).HasColumnName("productid");
            entity.Property(e => e.Reason).HasColumnName("reason");
            entity.Property(e => e.Systemquantity).HasColumnName("systemquantity");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.Product).WithMany(p => p.Inventoryaudits)
                .HasForeignKey(d => d.Productid)
                .HasConstraintName("inventoryaudit_productid_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Inventoryaudits)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("inventoryaudit_userid_fkey");
        });

        modelBuilder.Entity<Nutrition>(entity =>
        {
            entity.HasKey(e => e.Nutritionid).HasName("nutrition_pkey");

            entity.ToTable("nutrition");

            entity.HasIndex(e => e.Feedscheduleid, "IX_nutrition_feedscheduleid");

            entity.HasIndex(e => e.Flockid, "IX_nutrition_flockid");

            entity.HasIndex(e => e.Foodid, "IX_nutrition_foodid");

            entity.HasIndex(e => e.Waterid, "IX_nutrition_waterid");

            entity.Property(e => e.Nutritionid)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("nutritionid");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Developmentstage)
                .HasMaxLength(255)
                .HasColumnName("developmentstage");
            entity.Property(e => e.Feedscheduleid).HasColumnName("feedscheduleid");
            entity.Property(e => e.Flockid).HasColumnName("flockid");
            entity.Property(e => e.Foodid).HasColumnName("foodid");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Targetaudience)
                .HasMaxLength(255)
                .HasColumnName("targetaudience");
            entity.Property(e => e.Waterid).HasColumnName("waterid");

            entity.HasOne(d => d.Feedschedule).WithMany(p => p.Nutritions)
                .HasForeignKey(d => d.Feedscheduleid)
                .HasConstraintName("nutrition_feedscheduleid_fkey");

            entity.HasOne(d => d.Flock).WithMany(p => p.Nutritions)
                .HasForeignKey(d => d.Flockid)
                .HasConstraintName("nutrition_flockid_fkey");

            entity.HasOne(d => d.Food).WithMany(p => p.Nutritions)
                .HasForeignKey(d => d.Foodid)
                .HasConstraintName("nutrition_foodid_fkey");

            entity.HasOne(d => d.Water).WithMany(p => p.Nutritions)
                .HasForeignKey(d => d.Waterid)
                .HasConstraintName("nutrition_waterid_fkey");
        });

        modelBuilder.Entity<Performancestatistic>(entity =>
        {
            entity.HasKey(e => e.Perstaid).HasName("performancestatistic_pkey");

            entity.ToTable("performancestatistic");

            entity.HasIndex(e => e.Userid, "IX_performancestatistic_userid");

            entity.Property(e => e.Perstaid)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("perstaid");
            entity.Property(e => e.Completedtask)
                .HasDefaultValue(0)
                .HasColumnName("completedtask");
            entity.Property(e => e.Completionrate)
                .HasPrecision(5, 2)
                .HasComputedColumnSql("\nCASE\n    WHEN (totaltask > 0) THEN (((completedtask)::numeric * 100.0) / (totaltask)::numeric)\n    ELSE (0)::numeric\nEND", true)
                .HasColumnName("completionrate");
            entity.Property(e => e.Delaytask)
                .HasDefaultValue(0)
                .HasColumnName("delaytask");
            entity.Property(e => e.Note).HasColumnName("note");
            entity.Property(e => e.Performancerating)
                .HasMaxLength(50)
                .HasColumnName("performancerating");
            entity.Property(e => e.Rangetime).HasColumnName("rangetime");
            entity.Property(e => e.Totaltask)
                .HasDefaultValue(0)
                .HasColumnName("totaltask");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.User).WithMany(p => p.Performancestatistics)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("performancestatistic_userid_fkey");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Productid).HasName("product_pkey");

            entity.ToTable("product");

            entity.HasIndex(e => e.Supplierid, "IX_product_supplierid");

            entity.HasIndex(e => e.Productcode, "product_productcode_key").IsUnique();

            entity.Property(e => e.Productid)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("productid");
            entity.Property(e => e.Maxstock).HasColumnName("maxstock");
            entity.Property(e => e.Minstock).HasColumnName("minstock");
            entity.Property(e => e.Productcode)
                .HasMaxLength(50)
                .HasColumnName("productcode");
            entity.Property(e => e.Productname)
                .HasMaxLength(255)
                .HasColumnName("productname");
            entity.Property(e => e.Storagelocation).HasColumnName("storagelocation");
            entity.Property(e => e.Supplierid).HasColumnName("supplierid");
            entity.Property(e => e.Unit)
                .HasMaxLength(50)
                .HasColumnName("unit");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Products)
                .HasForeignKey(d => d.Supplierid)
                .HasConstraintName("product_supplierid_fkey");
        });

        modelBuilder.Entity<Purpose>(entity =>
        {
            entity.HasKey(e => e.Purposeid).HasName("purpose_pkey");

            entity.ToTable("purpose");

            entity.Property(e => e.Purposeid)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("purposeid");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Quantitylog>(entity =>
        {
            entity.HasKey(e => e.Qlogid).HasName("quantitylog_pkey");

            entity.ToTable("quantitylog");

            entity.HasIndex(e => e.Flockid, "IX_quantitylog_flockid");

            entity.HasIndex(e => e.Reasonid, "IX_quantitylog_reasonid");

            entity.Property(e => e.Qlogid)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("qlogid");
            entity.Property(e => e.Flockid).HasColumnName("flockid");
            entity.Property(e => e.Logdate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("logdate");
            entity.Property(e => e.Logtype)
                .HasMaxLength(20)
                .HasColumnName("logtype");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Reasonid).HasColumnName("reasonid");

            entity.HasOne(d => d.Flock).WithMany(p => p.Quantitylogs)
                .HasForeignKey(d => d.Flockid)
                .HasConstraintName("quantitylog_flockid_fkey");

            entity.HasOne(d => d.Reason).WithMany(p => p.Quantitylogs)
                .HasForeignKey(d => d.Reasonid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("quantitylog_reasonid_fkey");

            entity.HasMany(q => q.Users)
                .WithMany(u => u.QuantityLogs)
                .UsingEntity<Dictionary<string, object>>(
                    "UserQuantityLog",
                    j => j.HasOne<User>().WithMany().HasForeignKey("userId"),
                    j => j.HasOne<Quantitylog>().WithMany().HasForeignKey("qLogId"),
                    j => j.ToTable("user_quantity_log")
                );
        });

        modelBuilder.Entity<Reason>(entity =>
        {
            entity.HasKey(e => e.Reasonid).HasName("reason_pkey");

            entity.ToTable("reason");

            entity.Property(e => e.Reasonid)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("reasonid");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Roleid).HasName("roles_pkey");

            entity.ToTable("roles");

            entity.HasIndex(e => e.Rolename, "roles_rolename_key").IsUnique();

            entity.Property(e => e.Roleid)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("roleid");
            entity.Property(e => e.Permission)
                .HasMaxLength(255)
                .HasColumnName("permission");
            entity.Property(e => e.Rolename)
                .HasMaxLength(255)
                .HasColumnName("rolename");
        });

        modelBuilder.Entity<Salary>(entity =>
        {
            entity.HasKey(e => e.Salaryid).HasName("salary_pkey");

            entity.ToTable("salary");

            entity.HasIndex(e => e.Userid, "IX_salary_userid");

            entity.Property(e => e.Salaryid)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("salaryid");
            entity.Property(e => e.Basicsalary)
                .HasPrecision(12, 2)
                .HasColumnName("basicsalary");
            entity.Property(e => e.Bonus)
                .HasPrecision(12, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("bonus");
            entity.Property(e => e.Deduction)
                .HasPrecision(12, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("deduction");
            entity.Property(e => e.Finalsalary)
                .HasPrecision(12, 2)
                .HasComputedColumnSql("((basicsalary + bonus) - deduction)", true)
                .HasColumnName("finalsalary");
            entity.Property(e => e.Overtimehours)
                .HasPrecision(5, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("overtimehours");
            entity.Property(e => e.Salarymonth).HasColumnName("salarymonth");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasColumnName("status");
            entity.Property(e => e.Totalhoursworked)
                .HasPrecision(5, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("totalhoursworked");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.User).WithMany(p => p.Salaries)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("salary_userid_fkey");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.Supplierid).HasName("supplier_pkey");

            entity.ToTable("supplier");

            entity.Property(e => e.Supplierid)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("supplierid");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.Contactinformation).HasColumnName("contactinformation");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.Taskid).HasName("task_pkey");

            entity.ToTable("task");

            entity.HasIndex(e => e.Userid, "IX_task_userid");

            entity.Property(e => e.Taskid)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("taskid");
            entity.Property(e => e.Createddate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createddate");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Duedate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("duedate");
            entity.Property(e => e.Location)
                .HasMaxLength(255)
                .HasColumnName("location");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.Taskname)
                .HasMaxLength(255)
                .HasColumnName("taskname");
            entity.Property(e => e.Tasktype)
                .HasMaxLength(100)
                .HasColumnName("tasktype");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.User).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("task_userid_fkey");
        });

        modelBuilder.Entity<TimeKeeping>(entity =>
        {
            entity.HasKey(e => e.Timekeepingid).HasName("time_keeping_pkey");

            entity.ToTable("time_keeping");

            entity.HasIndex(e => e.Timekeepingtype, "IX_time_keeping_timekeepingtype");

            entity.HasIndex(e => e.Userid, "IX_time_keeping_userid");

            entity.Property(e => e.Timekeepingid)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("timekeepingid");
            entity.Property(e => e.Endtime).HasColumnName("endtime");
            entity.Property(e => e.Timekeepingtype).HasColumnName("timekeepingtype");
            entity.Property(e => e.Userid).HasColumnName("userid");
            entity.Property(e => e.Workdate).HasColumnName("workdate");

            entity.HasOne(d => d.TimekeepingtypeNavigation).WithMany(p => p.TimeKeepings)
                .HasForeignKey(d => d.Timekeepingtype)
                .HasConstraintName("time_keeping_timekeepingtype_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.TimeKeepings)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("time_keeping_userid_fkey");
        });

        modelBuilder.Entity<TimeKeepingType>(entity =>
        {
            entity.HasKey(e => e.Timetypeid).HasName("time_keeping_types_pkey");

            entity.ToTable("time_keeping_types");

            entity.HasIndex(e => e.Typename, "time_keeping_types_typename_key").IsUnique();

            entity.Property(e => e.Timetypeid)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("timetypeid");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Typename)
                .HasMaxLength(255)
                .HasColumnName("typename");
            entity.Property(e => e.Unitsalary)
                .HasPrecision(10, 2)
                .HasColumnName("unitsalary");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Roleid, "IX_users_roleid");

            entity.HasIndex(e => e.Cccd, "users_cccd_key").IsUnique();

            entity.Property(e => e.Userid)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("userid");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.Cccd)
                .HasMaxLength(12)
                .HasColumnName("cccd");
            entity.Property(e => e.Dateofbirth).HasColumnName("dateofbirth");
            entity.Property(e => e.Fullname)
                .HasMaxLength(255)
                .HasColumnName("fullname");
            entity.Property(e => e.Roleid).HasColumnName("roleid");
            entity.Property(e => e.Startdate).HasColumnName("startdate");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.Roleid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_users_role");

            entity.HasMany(u => u.QuantityLogs)
                .WithMany(q => q.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserQuantityLog",
                    j => j.HasOne<Quantitylog>().WithMany().HasForeignKey("qLogId"),
                    j => j.HasOne<User>().WithMany().HasForeignKey("userId"),
                    j => j.ToTable("user_quantity_log")
                );

            entity.HasMany(u => u.VaccinationLogs)
                .WithMany(v => v.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserVaccineLog",
                    j => j.HasOne<Vaccinationlog>().WithMany().HasForeignKey("vLogId"),
                    j => j.HasOne<User>().WithMany().HasForeignKey("userId"),
                    j => j.ToTable("user_vaccine_log")
                );

            entity.HasMany(u => u.HealthLogs)
                .WithMany(h => h.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserHealthLog",
                    j => j.HasOne<Healthlog>().WithMany().HasForeignKey("hLogId"),
                    j => j.HasOne<User>().WithMany().HasForeignKey("userId"),
                    j => j.ToTable("user_health_log")
                );

            entity.HasMany(u => u.EvaluationSummaries)
                .WithMany(q => q.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserSummary",
                    j => j.HasOne<Evaluationsummary>().WithMany().HasForeignKey("summaryId"),
                    j => j.HasOne<User>().WithMany().HasForeignKey("userId"),
                    j => j.ToTable("user_summary_log")
                );
        });

        modelBuilder.Entity<Vaccinationlog>(entity =>
        {
            entity.HasKey(e => e.Vlogid).HasName("vaccinationlog_pkey");

            entity.ToTable("vaccinationlog");

            entity.HasIndex(e => e.Flockid, "IX_vaccinationlog_flockid");

            entity.HasIndex(e => e.Vaccineid, "IX_vaccinationlog_vaccineid");

            entity.Property(e => e.Vlogid)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("vlogid");
            entity.Property(e => e.Dosage)
                .HasPrecision(5, 2)
                .HasColumnName("dosage");
            entity.Property(e => e.Flockid).HasColumnName("flockid");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .HasColumnName("status");
            entity.Property(e => e.Vaccinationdate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("vaccinationdate");
            entity.Property(e => e.Vaccineid).HasColumnName("vaccineid");

            entity.HasOne(d => d.Flock).WithMany(p => p.Vaccinationlogs)
                .HasForeignKey(d => d.Flockid)
                .HasConstraintName("vaccinationlog_flockid_fkey");

            entity.HasOne(d => d.Vaccine).WithMany(p => p.Vaccinationlogs)
                .HasForeignKey(d => d.Vaccineid)
                .HasConstraintName("vaccinationlog_vaccineid_fkey");

            entity.HasMany(v => v.Users)
                .WithMany(u => u.VaccinationLogs)
                .UsingEntity<Dictionary<string, object>>(
                    "UserVaccineLog",
                    j => j.HasOne<User>().WithMany().HasForeignKey("userId"),
                    j => j.HasOne<Vaccinationlog>().WithMany().HasForeignKey("vLogId"),
                    j => j.ToTable("user_vaccine_log")
                );
        });

        modelBuilder.Entity<Vaccine>(entity =>
        {
            entity.HasKey(e => e.Vaccineid).HasName("vaccine_pkey");

            entity.ToTable("vaccine");

            entity.HasIndex(e => e.Diseaseid, "IX_vaccine_diseaseid");

            entity.HasIndex(e => e.Supplierid, "IX_vaccine_supplierid");

            entity.Property(e => e.Vaccineid)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("vaccineid");
            entity.Property(e => e.Batchnumber).HasColumnName("batchnumber");
            entity.Property(e => e.Diseaseid).HasColumnName("diseaseid");
            entity.Property(e => e.Dosage).HasColumnName("dosage");
            entity.Property(e => e.Expirydate).HasColumnName("expirydate");
            entity.Property(e => e.Instructions).HasColumnName("instructions");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.Productiondate).HasColumnName("productiondate");
            entity.Property(e => e.Supplierid).HasColumnName("supplierid");

            entity.HasOne(d => d.Disease).WithMany(p => p.Vaccines)
                .HasForeignKey(d => d.Diseaseid)
                .HasConstraintName("vaccine_diseaseid_fkey");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Vaccines)
                .HasForeignKey(d => d.Supplierid)
                .HasConstraintName("vaccine_supplierid_fkey");
        });

        modelBuilder.Entity<Water>(entity =>
        {
            entity.HasKey(e => e.Waterid).HasName("water_pkey");

            entity.ToTable("water");

            entity.HasIndex(e => e.Supplierid, "IX_water_supplierid");

            entity.Property(e => e.Waterid)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("waterid");
            entity.Property(e => e.Expirydate).HasColumnName("expirydate");
            entity.Property(e => e.Ingredients).HasColumnName("ingredients");
            entity.Property(e => e.Instructions).HasColumnName("instructions");
            entity.Property(e => e.Mixingratio).HasColumnName("mixingratio");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.Supplierid).HasColumnName("supplierid");
            entity.Property(e => e.Targetaudience).HasColumnName("targetaudience");
            entity.Property(e => e.Usage).HasColumnName("usage");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Water)
                .HasForeignKey(d => d.Supplierid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("water_supplierid_fkey");
        });

        modelBuilder.Entity<Workschedule>(entity =>
        {
            entity.HasKey(e => e.Workscheduleid).HasName("workschedule_pkey");

            entity.ToTable("workschedule");

            entity.HasIndex(e => e.Taskid, "IX_workschedule_taskid");

            entity.HasIndex(e => e.Userid, "IX_workschedule_userid");

            entity.Property(e => e.Workscheduleid)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("workscheduleid");
            entity.Property(e => e.Colorcode)
                .HasMaxLength(7)
                .HasColumnName("colorcode");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.Taskid).HasColumnName("taskid");
            entity.Property(e => e.Userid).HasColumnName("userid");
            entity.Property(e => e.Workdate).HasColumnName("workdate");

            entity.HasOne(d => d.Task).WithMany(p => p.Workschedules)
                .HasForeignKey(d => d.Taskid)
                .HasConstraintName("workschedule_taskid_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Workschedules)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("workschedule_userid_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
