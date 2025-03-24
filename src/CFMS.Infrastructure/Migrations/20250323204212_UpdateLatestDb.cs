using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CFMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLatestDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    FullName = table.Column<string>(type: "character varying", nullable: true),
                    PhoneNumber = table.Column<string>(type: "character varying", nullable: true),
                    Mail = table.Column<string>(type: "character varying", nullable: true),
                    Avatar = table.Column<string>(type: "character varying", nullable: true),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: true, defaultValue: 1),
                    Address = table.Column<string>(type: "text", nullable: true),
                    CCCD = table.Column<string>(type: "character varying", nullable: true),
                    GoogleId = table.Column<string>(type: "character varying", nullable: true),
                    SystemRole = table.Column<int>(type: "integer", nullable: true),
                    HashedPassword = table.Column<string>(type: "character varying", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("User_pkey", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    CategoryName = table.Column<string>(type: "character varying", nullable: true),
                    CategoryType = table.Column<string>(type: "character varying", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: true, defaultValue: 1),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Category_pkey", x => x.CategoryId);
                    table.ForeignKey(
                        name: "FK_Category_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Category_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Farm",
                columns: table => new
                {
                    FarmId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    FarmName = table.Column<string>(type: "character varying", nullable: true),
                    FarmCode = table.Column<string>(type: "character varying", nullable: true),
                    Address = table.Column<string>(type: "character varying", nullable: true),
                    Area = table.Column<decimal>(type: "numeric", nullable: true),
                    Scale = table.Column<int>(type: "integer", nullable: true),
                    PhoneNumber = table.Column<string>(type: "character varying", nullable: true),
                    Website = table.Column<string>(type: "character varying", nullable: true),
                    ImageUrl = table.Column<string>(type: "character varying", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Farm_pkey", x => x.FarmId);
                    table.ForeignKey(
                        name: "FK_Farm_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Farm_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    NotificationId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    NotificationName = table.Column<string>(type: "character varying", nullable: true),
                    NotificationType = table.Column<string>(type: "character varying", nullable: true),
                    Content = table.Column<string>(type: "text", nullable: true),
                    IsRead = table.Column<int>(type: "integer", nullable: true, defaultValue: 0),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Notification_pkey", x => x.NotificationId);
                    table.ForeignKey(
                        name: "Notification_UserId_fkey",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NutritionPlan",
                columns: table => new
                {
                    NutritionPlanId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Name = table.Column<string>(type: "character varying", nullable: true),
                    Description = table.Column<string>(type: "character varying", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("NutritionPlan_pkey", x => x.NutritionPlanId);
                    table.ForeignKey(
                        name: "FK_NutritionPlan_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NutritionPlan_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RevokedToken",
                columns: table => new
                {
                    RevokedTokenId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Token = table.Column<string>(type: "character varying", nullable: false),
                    TokenType = table.Column<int>(type: "integer", nullable: false),
                    RevokedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ExpiryDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("RevokedToken_pkey", x => x.RevokedTokenId);
                    table.ForeignKey(
                        name: "RevokedToken_UserId_fkey",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shift",
                columns: table => new
                {
                    ShiftId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    ShiftName = table.Column<string>(type: "character varying", nullable: true),
                    StartTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EndTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Shift_pkey", x => x.ShiftId);
                    table.ForeignKey(
                        name: "FK_Shift_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shift_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Supplier",
                columns: table => new
                {
                    SupplierId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    ResourceSupplierId = table.Column<Guid>(type: "uuid", nullable: true),
                    SupplierCode = table.Column<string>(type: "character varying", nullable: true),
                    Address = table.Column<string>(type: "character varying", nullable: true),
                    PhoneNumber = table.Column<string>(type: "character varying", nullable: true),
                    BankAccount = table.Column<string>(type: "character varying", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: true, defaultValue: 1),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Supplier_pkey", x => x.SupplierId);
                    table.ForeignKey(
                        name: "FK_Supplier_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Supplier_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskSchedule",
                columns: table => new
                {
                    TaskScheduleId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Frequency = table.Column<int>(type: "integer", nullable: true),
                    NextWorkDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastWorkDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("TaskSchedule_pkey", x => x.TaskScheduleId);
                    table.ForeignKey(
                        name: "FK_TaskSchedule_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskSchedule_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubCategory",
                columns: table => new
                {
                    SubCategoryId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    SubCategoryName = table.Column<string>(type: "character varying", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: true, defaultValue: 1),
                    DataType = table.Column<string>(type: "character varying", nullable: true),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SubCategory_pkey", x => x.SubCategoryId);
                    table.ForeignKey(
                        name: "FK_SubCategory_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubCategory_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "SubCategory_CategoryId_fkey",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BreedingArea",
                columns: table => new
                {
                    BreedingAreaId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    BreedingAreaCode = table.Column<string>(type: "character varying", nullable: true),
                    BreedingAreaName = table.Column<string>(type: "character varying", nullable: true),
                    ImageUrl = table.Column<string>(type: "character varying", nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    FarmId = table.Column<Guid>(type: "uuid", nullable: true),
                    Area = table.Column<decimal>(type: "numeric", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: true, defaultValue: 1),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("BreedingArea_pkey", x => x.BreedingAreaId);
                    table.ForeignKey(
                        name: "BreedingArea_FarmId_fkey",
                        column: x => x.FarmId,
                        principalTable: "Farm",
                        principalColumn: "FarmId");
                    table.ForeignKey(
                        name: "FK_BreedingArea_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BreedingArea_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FarmEmployee",
                columns: table => new
                {
                    FarmEmployeeId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    FarmId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: true, defaultValue: 1),
                    FarmRole = table.Column<int>(type: "integer", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("FarmEmployee_pkey", x => x.FarmEmployeeId);
                    table.ForeignKey(
                        name: "FK_FarmEmployee_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FarmEmployee_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FarmEmployee_FarmId_fkey",
                        column: x => x.FarmId,
                        principalTable: "Farm",
                        principalColumn: "FarmId");
                    table.ForeignKey(
                        name: "FarmEmployee_UserId_fkey",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "ShiftSchedule",
                columns: table => new
                {
                    ShiftScheduleId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    ShiftId = table.Column<Guid>(type: "uuid", nullable: true),
                    Date = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ShiftSchedule_pkey", x => x.ShiftScheduleId);
                    table.ForeignKey(
                        name: "ShiftSchedule_ShiftId_fkey",
                        column: x => x.ShiftId,
                        principalTable: "Shift",
                        principalColumn: "ShiftId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "EvaluationTemplate",
                columns: table => new
                {
                    EvaluationTemplateId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    TemplateName = table.Column<string>(type: "character varying", nullable: true),
                    TemplateTypeId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("EvaluationTemplate_pkey", x => x.EvaluationTemplateId);
                    table.ForeignKey(
                        name: "EvaluationTemplate_TemplateTypeId_fkey",
                        column: x => x.TemplateTypeId,
                        principalTable: "SubCategory",
                        principalColumn: "SubCategoryId");
                    table.ForeignKey(
                        name: "FK_EvaluationTemplate_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EvaluationTemplate_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FeedSession",
                columns: table => new
                {
                    FeedSessionId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    NutritionPlanId = table.Column<Guid>(type: "uuid", nullable: true),
                    FeedingTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    FeedAmount = table.Column<decimal>(type: "numeric", nullable: true),
                    UnitId = table.Column<Guid>(type: "uuid", nullable: true),
                    Note = table.Column<string>(type: "character varying", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("FeedSession_pkey", x => x.FeedSessionId);
                    table.ForeignKey(
                        name: "FK_FeedSession_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeedSession_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FeedSession_NutritionPlanId_fkey",
                        column: x => x.NutritionPlanId,
                        principalTable: "NutritionPlan",
                        principalColumn: "NutritionPlanId");
                    table.ForeignKey(
                        name: "FeedSession_UnitId_fkey",
                        column: x => x.UnitId,
                        principalTable: "SubCategory",
                        principalColumn: "SubCategoryId");
                });

            migrationBuilder.CreateTable(
                name: "GrowthStage",
                columns: table => new
                {
                    GrowthStageId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    StageName = table.Column<string>(type: "character varying", nullable: true),
                    ChickenType = table.Column<Guid>(type: "uuid", nullable: true),
                    MinAgeWeek = table.Column<int>(type: "integer", nullable: true),
                    MaxAgeWeek = table.Column<int>(type: "integer", nullable: true),
                    Description = table.Column<string>(type: "character varying", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("GrowthStage_pkey", x => x.GrowthStageId);
                    table.ForeignKey(
                        name: "FK_GrowthStage_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GrowthStage_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "GrowthStage_ChickenType_fkey",
                        column: x => x.ChickenType,
                        principalTable: "SubCategory",
                        principalColumn: "SubCategoryId");
                });

            migrationBuilder.CreateTable(
                name: "Request",
                columns: table => new
                {
                    RequestId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    RequestTypeId = table.Column<Guid>(type: "uuid", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: true, defaultValue: 1),
                    ApprovedById = table.Column<Guid>(type: "uuid", nullable: true),
                    ApprovedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Request_pkey", x => x.RequestId);
                    table.ForeignKey(
                        name: "FK_Request_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Request_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Request_ApprovedById_fkey",
                        column: x => x.ApprovedById,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "Request_RequestTypeId_fkey",
                        column: x => x.RequestTypeId,
                        principalTable: "SubCategory",
                        principalColumn: "SubCategoryId");
                });

            migrationBuilder.CreateTable(
                name: "Resource",
                columns: table => new
                {
                    ResourceId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    ResourceTypeId = table.Column<Guid>(type: "uuid", nullable: true),
                    Description = table.Column<string>(type: "character varying", nullable: true),
                    UnitId = table.Column<Guid>(type: "uuid", nullable: true),
                    PackageId = table.Column<Guid>(type: "uuid", nullable: true),
                    PackageSize = table.Column<decimal>(type: "numeric", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Resource_pkey", x => x.ResourceId);
                    table.ForeignKey(
                        name: "FK_Resource_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Resource_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "Resource_PackageId_fkey",
                        column: x => x.PackageId,
                        principalTable: "SubCategory",
                        principalColumn: "SubCategoryId");
                    table.ForeignKey(
                        name: "Resource_ResourceTypeId_fkey",
                        column: x => x.ResourceTypeId,
                        principalTable: "SubCategory",
                        principalColumn: "SubCategoryId");
                    table.ForeignKey(
                        name: "Resource_UnitId_fkey",
                        column: x => x.UnitId,
                        principalTable: "SubCategory",
                        principalColumn: "SubCategoryId");
                });

            migrationBuilder.CreateTable(
                name: "Task",
                columns: table => new
                {
                    TaskId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    TaskName = table.Column<string>(type: "character varying", nullable: true),
                    TaskTypeId = table.Column<Guid>(type: "uuid", nullable: true),
                    Description = table.Column<string>(type: "character varying", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Task_pkey", x => x.TaskId);
                    table.ForeignKey(
                        name: "FK_Task_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Task_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Task_TaskTypeId_fkey",
                        column: x => x.TaskTypeId,
                        principalTable: "SubCategory",
                        principalColumn: "SubCategoryId");
                });

            migrationBuilder.CreateTable(
                name: "Warehouse",
                columns: table => new
                {
                    WareId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    FarmId = table.Column<Guid>(type: "uuid", nullable: true),
                    StorageTypeId = table.Column<Guid>(type: "uuid", nullable: true),
                    WarehouseName = table.Column<string>(type: "character varying", nullable: true),
                    MaxQuantity = table.Column<int>(type: "integer", nullable: true),
                    MaxWeight = table.Column<decimal>(type: "numeric", nullable: true),
                    CurrentQuantity = table.Column<int>(type: "integer", nullable: true),
                    CurrentWeight = table.Column<decimal>(type: "numeric", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: true, defaultValue: 1),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Warehouse_pkey", x => x.WareId);
                    table.ForeignKey(
                        name: "FK_Warehouse_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Warehouse_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Warehouse_FarmId_fkey",
                        column: x => x.FarmId,
                        principalTable: "Farm",
                        principalColumn: "FarmId");
                    table.ForeignKey(
                        name: "Warehouse_StorageTypeId_fkey",
                        column: x => x.StorageTypeId,
                        principalTable: "SubCategory",
                        principalColumn: "SubCategoryId");
                });

            migrationBuilder.CreateTable(
                name: "ChickenCoop",
                columns: table => new
                {
                    ChickenCoopId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    ChickenCoopCode = table.Column<string>(type: "character varying", nullable: true),
                    ChickenCoopName = table.Column<string>(type: "character varying", nullable: true),
                    MaxQuantity = table.Column<int>(type: "integer", nullable: true),
                    Area = table.Column<int>(type: "integer", nullable: true),
                    Density = table.Column<decimal>(type: "numeric", nullable: true),
                    DensityUnitId = table.Column<Guid>(type: "uuid", nullable: true),
                    CurrentQuantity = table.Column<int>(type: "integer", nullable: true),
                    Description = table.Column<string>(type: "character varying", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: true, defaultValue: 1),
                    PurposeId = table.Column<Guid>(type: "uuid", nullable: true),
                    BreedingAreaId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ChickenCoop_pkey", x => x.ChickenCoopId);
                    table.ForeignKey(
                        name: "ChickenCoop_BreedingAreaId_fkey",
                        column: x => x.BreedingAreaId,
                        principalTable: "BreedingArea",
                        principalColumn: "BreedingAreaId");
                    table.ForeignKey(
                        name: "ChickenCoop_DensityUnitId_fkey",
                        column: x => x.DensityUnitId,
                        principalTable: "SubCategory",
                        principalColumn: "SubCategoryId");
                    table.ForeignKey(
                        name: "ChickenCoop_PurposeId_fkey",
                        column: x => x.PurposeId,
                        principalTable: "SubCategory",
                        principalColumn: "SubCategoryId");
                    table.ForeignKey(
                        name: "FK_ChickenCoop_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChickenCoop_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TemplateCriteria",
                columns: table => new
                {
                    TemplateCriteriaId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    TemplateName = table.Column<string>(type: "character varying", nullable: true),
                    EvaluationTemplateId = table.Column<Guid>(type: "uuid", nullable: true),
                    CriteriaId = table.Column<Guid>(type: "uuid", nullable: true),
                    ExpectedCondition = table.Column<string>(type: "character varying", nullable: true),
                    ExpectedUnit = table.Column<string>(type: "character varying", nullable: true),
                    ExpectedValue = table.Column<string>(type: "character varying", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("TemplateCriteria_pkey", x => x.TemplateCriteriaId);
                    table.ForeignKey(
                        name: "FK_TemplateCriteria_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TemplateCriteria_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "TemplateCriteria_CriteriaId_fkey",
                        column: x => x.CriteriaId,
                        principalTable: "SubCategory",
                        principalColumn: "SubCategoryId");
                    table.ForeignKey(
                        name: "TemplateCriteria_EvaluationTemplateId_fkey",
                        column: x => x.EvaluationTemplateId,
                        principalTable: "EvaluationTemplate",
                        principalColumn: "EvaluationTemplateId");
                });

            migrationBuilder.CreateTable(
                name: "GrowthNutrition",
                columns: table => new
                {
                    GrowthNutritionId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    NutritionPlanId = table.Column<Guid>(type: "uuid", nullable: true),
                    GrowthStageId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("GrowthNutrition_pkey", x => x.GrowthNutritionId);
                    table.ForeignKey(
                        name: "FK_GrowthNutrition_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GrowthNutrition_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "GrowthNutrition_GrowthStageId_fkey",
                        column: x => x.GrowthStageId,
                        principalTable: "GrowthStage",
                        principalColumn: "GrowthStageId");
                    table.ForeignKey(
                        name: "GrowthNutrition_NutritionPlanId_fkey",
                        column: x => x.NutritionPlanId,
                        principalTable: "NutritionPlan",
                        principalColumn: "NutritionPlanId");
                });

            migrationBuilder.CreateTable(
                name: "TaskRequest",
                columns: table => new
                {
                    TaskRequestId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    RequestId = table.Column<Guid>(type: "uuid", nullable: true),
                    TaskTypeId = table.Column<Guid>(type: "uuid", nullable: true),
                    Priority = table.Column<int>(type: "integer", nullable: true),
                    Description = table.Column<string>(type: "character varying", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("TaskRequest_pkey", x => x.TaskRequestId);
                    table.ForeignKey(
                        name: "TaskRequest_RequestId_fkey",
                        column: x => x.RequestId,
                        principalTable: "Request",
                        principalColumn: "RequestId");
                    table.ForeignKey(
                        name: "TaskRequest_TaskTypeId_fkey",
                        column: x => x.TaskTypeId,
                        principalTable: "SubCategory",
                        principalColumn: "SubCategoryId");
                });

            migrationBuilder.CreateTable(
                name: "Equipment",
                columns: table => new
                {
                    EquipmentId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    EquipmentCode = table.Column<string>(type: "character varying", nullable: true),
                    EquipmentName = table.Column<string>(type: "character varying", nullable: true),
                    Material = table.Column<string>(type: "character varying", nullable: true),
                    Usage = table.Column<string>(type: "character varying", nullable: true),
                    Warranty = table.Column<int>(type: "integer", nullable: true),
                    Size = table.Column<decimal>(type: "numeric", nullable: true),
                    SizeUnitId = table.Column<Guid>(type: "uuid", nullable: true),
                    Weight = table.Column<decimal>(type: "numeric", nullable: true),
                    WeightUnitId = table.Column<Guid>(type: "uuid", nullable: true),
                    PurchaseDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Equipment_pkey", x => x.EquipmentId);
                    table.ForeignKey(
                        name: "Equipment_EquipmentId_fkey",
                        column: x => x.EquipmentId,
                        principalTable: "Resource",
                        principalColumn: "ResourceId");
                    table.ForeignKey(
                        name: "Equipment_SizeUnitId_fkey",
                        column: x => x.SizeUnitId,
                        principalTable: "SubCategory",
                        principalColumn: "SubCategoryId");
                    table.ForeignKey(
                        name: "Equipment_WeightUnitId_fkey",
                        column: x => x.WeightUnitId,
                        principalTable: "SubCategory",
                        principalColumn: "SubCategoryId");
                    table.ForeignKey(
                        name: "FK_Equipment_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Equipment_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Food",
                columns: table => new
                {
                    FoodId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    FoodCode = table.Column<string>(type: "character varying", nullable: true),
                    FoodName = table.Column<string>(type: "character varying", nullable: true),
                    Note = table.Column<string>(type: "character varying", nullable: true),
                    ProductionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ExpiryDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Food_pkey", x => x.FoodId);
                    table.ForeignKey(
                        name: "FK_Food_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Food_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Food_FoodId_fkey",
                        column: x => x.FoodId,
                        principalTable: "Resource",
                        principalColumn: "ResourceId");
                });

            migrationBuilder.CreateTable(
                name: "Medicine",
                columns: table => new
                {
                    MedicineId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Usage = table.Column<string>(type: "character varying", nullable: true),
                    DosageForm = table.Column<string>(type: "text", nullable: true),
                    StorageCondition = table.Column<string>(type: "character varying", nullable: true),
                    DiseaseId = table.Column<Guid>(type: "uuid", nullable: true),
                    ProductionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ExpiryDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Medicine_pkey", x => x.MedicineId);
                    table.ForeignKey(
                        name: "FK_Medicine_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Medicine_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "Medicine_DiseaseId_fkey",
                        column: x => x.DiseaseId,
                        principalTable: "SubCategory",
                        principalColumn: "SubCategoryId");
                    table.ForeignKey(
                        name: "Medicine_MedicineId_fkey",
                        column: x => x.MedicineId,
                        principalTable: "Resource",
                        principalColumn: "ResourceId");
                });

            migrationBuilder.CreateTable(
                name: "ResourceSupplier",
                columns: table => new
                {
                    ResourceSupplierId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    ResourceId = table.Column<Guid>(type: "uuid", nullable: true),
                    Description = table.Column<string>(type: "character varying", nullable: true),
                    SupplierId = table.Column<Guid>(type: "uuid", nullable: true),
                    Price = table.Column<decimal>(type: "numeric", nullable: true),
                    UnitPriceId = table.Column<Guid>(type: "uuid", nullable: true),
                    PackagePriceId = table.Column<Guid>(type: "uuid", nullable: true),
                    PackageSizePrice = table.Column<decimal>(type: "numeric", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ResourceSupplier_pkey", x => x.ResourceSupplierId);
                    table.ForeignKey(
                        name: "FK_ResourceSupplier_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResourceSupplier_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "ResourceSupplier_PackagePriceId_fkey",
                        column: x => x.PackagePriceId,
                        principalTable: "SubCategory",
                        principalColumn: "SubCategoryId");
                    table.ForeignKey(
                        name: "ResourceSupplier_ResourceId_fkey",
                        column: x => x.ResourceId,
                        principalTable: "Resource",
                        principalColumn: "ResourceId");
                    table.ForeignKey(
                        name: "ResourceSupplier_SupplierId_fkey",
                        column: x => x.SupplierId,
                        principalTable: "Supplier",
                        principalColumn: "SupplierId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "ResourceSupplier_UnitPriceId_fkey",
                        column: x => x.UnitPriceId,
                        principalTable: "SubCategory",
                        principalColumn: "SubCategoryId");
                });

            migrationBuilder.CreateTable(
                name: "Assignment",
                columns: table => new
                {
                    AssignmentId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    TaskId = table.Column<Guid>(type: "uuid", nullable: true),
                    AssignedToId = table.Column<Guid>(type: "uuid", nullable: true),
                    AssignedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ShiftScheduleId = table.Column<Guid>(type: "uuid", nullable: true),
                    TaskScheduleId = table.Column<Guid>(type: "uuid", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: true, defaultValue: 1),
                    Note = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Assignment_pkey", x => x.AssignmentId);
                    table.ForeignKey(
                        name: "Assignment_AssignedToId_fkey",
                        column: x => x.AssignedToId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "Assignment_TaskScheduleId_fkey",
                        column: x => x.TaskScheduleId,
                        principalTable: "TaskSchedule",
                        principalColumn: "TaskScheduleId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "Assignment_taskId_fkey",
                        column: x => x.TaskId,
                        principalTable: "Task",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assignment_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Assignment_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "ShiftSchedule_ShiftScheduleId_fkey",
                        column: x => x.ShiftScheduleId,
                        principalTable: "ShiftSchedule",
                        principalColumn: "ShiftScheduleId");
                });

            migrationBuilder.CreateTable(
                name: "TaskHarvest",
                columns: table => new
                {
                    TaskHarvestId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    TaskId = table.Column<Guid>(type: "uuid", nullable: false),
                    HarvestTypeId = table.Column<Guid>(type: "uuid", nullable: true),
                    Quantity = table.Column<int>(type: "integer", nullable: true),
                    UnitId = table.Column<Guid>(type: "uuid", nullable: true),
                    Quality = table.Column<string>(type: "character varying", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("TaskHarvest_pkey", x => x.TaskHarvestId);
                    table.ForeignKey(
                        name: "TaskHarvest_HarvestTypeId_fkey",
                        column: x => x.HarvestTypeId,
                        principalTable: "SubCategory",
                        principalColumn: "SubCategoryId");
                    table.ForeignKey(
                        name: "TaskHarvest_TaskId_fkey",
                        column: x => x.TaskId,
                        principalTable: "Task",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskResource",
                columns: table => new
                {
                    TaskResourceId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    TaskId = table.Column<Guid>(type: "uuid", nullable: false),
                    ResourceId = table.Column<Guid>(type: "uuid", nullable: false),
                    ResourceTypeId = table.Column<Guid>(type: "uuid", nullable: true),
                    Quantity = table.Column<int>(type: "integer", nullable: true),
                    UnitId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("TaskResource_pkey", x => x.TaskResourceId);
                    table.ForeignKey(
                        name: "FK_TaskResource_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskResource_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "TaskResource_ResourceTypeId_fkey",
                        column: x => x.ResourceTypeId,
                        principalTable: "SubCategory",
                        principalColumn: "SubCategoryId");
                    table.ForeignKey(
                        name: "TaskResource_TaskId_fkey",
                        column: x => x.TaskId,
                        principalTable: "Task",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryRequest",
                columns: table => new
                {
                    InventoryRequestId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    RequestId = table.Column<Guid>(type: "uuid", nullable: true),
                    InventoryRequestTypeId = table.Column<Guid>(type: "uuid", nullable: true),
                    WareFromId = table.Column<Guid>(type: "uuid", nullable: true),
                    WareToId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("InventoryRequest_pkey", x => x.InventoryRequestId);
                    table.ForeignKey(
                        name: "FK_InventoryRequest_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryRequest_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "InventoryRequest_InventoryRequestTypeId_fkey",
                        column: x => x.InventoryRequestTypeId,
                        principalTable: "SubCategory",
                        principalColumn: "SubCategoryId");
                    table.ForeignKey(
                        name: "InventoryRequest_RequestId_fkey",
                        column: x => x.RequestId,
                        principalTable: "Request",
                        principalColumn: "RequestId");
                    table.ForeignKey(
                        name: "InventoryRequest_WareFromId_fkey",
                        column: x => x.WareFromId,
                        principalTable: "Warehouse",
                        principalColumn: "WareId");
                    table.ForeignKey(
                        name: "InventoryRequest_WareToId_fkey",
                        column: x => x.WareToId,
                        principalTable: "Warehouse",
                        principalColumn: "WareId");
                });

            migrationBuilder.CreateTable(
                name: "WarePermission",
                columns: table => new
                {
                    PermissionId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    WareId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    PermissionLevel = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    GrantedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("WarePermission_pkey", x => x.PermissionId);
                    table.ForeignKey(
                        name: "FK_WarePermission_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WarePermission_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "WarePermission_UserId_fkey",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "WarePermission_WareId_fkey",
                        column: x => x.WareId,
                        principalTable: "Warehouse",
                        principalColumn: "WareId");
                });

            migrationBuilder.CreateTable(
                name: "WareStock",
                columns: table => new
                {
                    WareStockId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    WareId = table.Column<Guid>(type: "uuid", nullable: true),
                    ResourceId = table.Column<Guid>(type: "uuid", nullable: true),
                    Quantity = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    UnitId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("WareStock_pkey", x => x.WareStockId);
                    table.ForeignKey(
                        name: "WareStock_ResourceId_fkey",
                        column: x => x.ResourceId,
                        principalTable: "Resource",
                        principalColumn: "ResourceId");
                    table.ForeignKey(
                        name: "WareStock_WareId_fkey",
                        column: x => x.WareId,
                        principalTable: "Warehouse",
                        principalColumn: "WareId");
                });

            migrationBuilder.CreateTable(
                name: "WareTransaction",
                columns: table => new
                {
                    TransactionId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    WareId = table.Column<Guid>(type: "uuid", nullable: true),
                    ResourceId = table.Column<Guid>(type: "uuid", nullable: true),
                    Quantity = table.Column<int>(type: "integer", nullable: true),
                    UnitId = table.Column<Guid>(type: "uuid", nullable: true),
                    BatchNumber = table.Column<int>(type: "integer", nullable: true),
                    TransactionType = table.Column<Guid>(type: "uuid", nullable: true),
                    Reason = table.Column<string>(type: "text", nullable: true),
                    TransactionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LocationFromId = table.Column<Guid>(type: "uuid", nullable: true),
                    LocationToId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("WareTransaction_pkey", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_WareTransaction_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WareTransaction_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "WareTransaction_LocationFromId_fkey",
                        column: x => x.LocationFromId,
                        principalTable: "Warehouse",
                        principalColumn: "WareId");
                    table.ForeignKey(
                        name: "WareTransaction_LocationToId_fkey",
                        column: x => x.LocationToId,
                        principalTable: "Warehouse",
                        principalColumn: "WareId");
                    table.ForeignKey(
                        name: "WareTransaction_TransactionType_fkey",
                        column: x => x.TransactionType,
                        principalTable: "SubCategory",
                        principalColumn: "SubCategoryId");
                    table.ForeignKey(
                        name: "WareTransaction_WareId_fkey",
                        column: x => x.WareId,
                        principalTable: "Warehouse",
                        principalColumn: "WareId");
                });

            migrationBuilder.CreateTable(
                name: "ChickenBatch",
                columns: table => new
                {
                    ChickenBatchId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    ChickenCoopId = table.Column<Guid>(type: "uuid", nullable: true),
                    ChickenBatchName = table.Column<string>(type: "character varying", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Note = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: true, defaultValue: 1),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ChickenBatch_pkey", x => x.ChickenBatchId);
                    table.ForeignKey(
                        name: "ChickenBatch_ChickenCoopId_fkey",
                        column: x => x.ChickenCoopId,
                        principalTable: "ChickenCoop",
                        principalColumn: "ChickenCoopId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ChickenBatch_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChickenBatch_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaskLocation",
                columns: table => new
                {
                    TaskLocationId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    TaskId = table.Column<Guid>(type: "uuid", nullable: true),
                    LocationType = table.Column<string>(type: "character varying", nullable: true),
                    LocationId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("TaskLocation_pkey", x => x.TaskLocationId);
                    table.ForeignKey(
                        name: "TaskLocation_LocationId_fkey",
                        column: x => x.LocationId,
                        principalTable: "Warehouse",
                        principalColumn: "WareId");
                    table.ForeignKey(
                        name: "TaskLocation_LocationId_fkey1",
                        column: x => x.LocationId,
                        principalTable: "ChickenCoop",
                        principalColumn: "ChickenCoopId");
                    table.ForeignKey(
                        name: "TaskLocation_TaskId_fkey",
                        column: x => x.TaskId,
                        principalTable: "Task",
                        principalColumn: "TaskId");
                });

            migrationBuilder.CreateTable(
                name: "TaskLog",
                columns: table => new
                {
                    TaskLogId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    TaskId = table.Column<Guid>(type: "uuid", nullable: true),
                    ChickenCoopId = table.Column<Guid>(type: "uuid", nullable: true),
                    CompletedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Note = table.Column<string>(type: "character varying", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("TaskLog_pkey", x => x.TaskLogId);
                    table.ForeignKey(
                        name: "FK_TaskLog_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskLog_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "TaskLog_ChickenCoopId_fkey",
                        column: x => x.ChickenCoopId,
                        principalTable: "ChickenCoop",
                        principalColumn: "ChickenCoopId");
                    table.ForeignKey(
                        name: "TaskLog_TaskId_fkey",
                        column: x => x.TaskId,
                        principalTable: "Task",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CoopEquipment",
                columns: table => new
                {
                    CoopEquipmentId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    ChickenCoopId = table.Column<Guid>(type: "uuid", nullable: true),
                    EquipmentId = table.Column<Guid>(type: "uuid", nullable: true),
                    Quantity = table.Column<int>(type: "integer", nullable: true),
                    AssignedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastMaintenanceDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    NextMaintenanceDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    MaintenanceInterval = table.Column<int>(type: "integer", nullable: false, defaultValue: 30),
                    Status = table.Column<int>(type: "integer", nullable: true, defaultValue: 1),
                    Note = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("CoopEquipment_pkey", x => x.CoopEquipmentId);
                    table.ForeignKey(
                        name: "CoopEquipment_ChickenCoopId_fkey",
                        column: x => x.ChickenCoopId,
                        principalTable: "ChickenCoop",
                        principalColumn: "ChickenCoopId");
                    table.ForeignKey(
                        name: "CoopEquipment_EquipmentId_fkey",
                        column: x => x.EquipmentId,
                        principalTable: "Equipment",
                        principalColumn: "EquipmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoopEquipment_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoopEquipment_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NutritionPlanDetail",
                columns: table => new
                {
                    NutritionPlanDetailId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    NutritionPlanId = table.Column<Guid>(type: "uuid", nullable: true),
                    FoodId = table.Column<Guid>(type: "uuid", nullable: true),
                    UnitId = table.Column<Guid>(type: "uuid", nullable: true),
                    FoodWeight = table.Column<decimal>(type: "numeric", nullable: true),
                    ConsumptionRate = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("NutritionPlanDetail_pkey", x => x.NutritionPlanDetailId);
                    table.ForeignKey(
                        name: "NutritionPlanDetail_FoodId_fkey",
                        column: x => x.FoodId,
                        principalTable: "Food",
                        principalColumn: "FoodId");
                    table.ForeignKey(
                        name: "NutritionPlanDetail_NutritionPlanId_fkey",
                        column: x => x.NutritionPlanId,
                        principalTable: "NutritionPlan",
                        principalColumn: "NutritionPlanId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "NutritionPlanDetail_UnitId_fkey",
                        column: x => x.UnitId,
                        principalTable: "SubCategory",
                        principalColumn: "SubCategoryId");
                });

            migrationBuilder.CreateTable(
                name: "InventoryReceipt",
                columns: table => new
                {
                    InventoryReceiptId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    InventoryRequestId = table.Column<Guid>(type: "uuid", nullable: true),
                    ReceiptTypeId = table.Column<Guid>(type: "uuid", nullable: true),
                    ReceiptCodeNumber = table.Column<string>(type: "character varying", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: true, defaultValue: 1),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("InventoryReceipt_pkey", x => x.InventoryReceiptId);
                    table.ForeignKey(
                        name: "FK_InventoryReceipt_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryReceipt_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "InventoryReceipt_InventoryRequestId_fkey",
                        column: x => x.InventoryRequestId,
                        principalTable: "InventoryRequest",
                        principalColumn: "InventoryRequestId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "InventoryReceipt_ReceiptTypeId_fkey",
                        column: x => x.ReceiptTypeId,
                        principalTable: "SubCategory",
                        principalColumn: "SubCategoryId");
                });

            migrationBuilder.CreateTable(
                name: "InventoryRequestDetail",
                columns: table => new
                {
                    InventoryRequestDetailId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    InventoryRequestId = table.Column<Guid>(type: "uuid", nullable: true),
                    ResourceId = table.Column<Guid>(type: "uuid", nullable: true),
                    ExpectedQuantity = table.Column<decimal>(type: "numeric", nullable: true),
                    UnitId = table.Column<Guid>(type: "uuid", nullable: true),
                    Reason = table.Column<string>(type: "character varying", nullable: true),
                    ExpectedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Note = table.Column<string>(type: "character varying", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("InventoryRequestDetail_pkey", x => x.InventoryRequestDetailId);
                    table.ForeignKey(
                        name: "InventoryRequestDetail_InventoryRequestId_fkey",
                        column: x => x.InventoryRequestId,
                        principalTable: "InventoryRequest",
                        principalColumn: "InventoryRequestId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "InventoryRequestDetail_ResourceId_fkey",
                        column: x => x.ResourceId,
                        principalTable: "Resource",
                        principalColumn: "ResourceId");
                    table.ForeignKey(
                        name: "InventoryRequestDetail_UnitId_fkey",
                        column: x => x.UnitId,
                        principalTable: "SubCategory",
                        principalColumn: "SubCategoryId");
                });

            migrationBuilder.CreateTable(
                name: "Chicken",
                columns: table => new
                {
                    ChickenId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    ChickenCode = table.Column<string>(type: "character varying", nullable: true),
                    ChickenName = table.Column<string>(type: "character varying", nullable: true),
                    TotalQuantity = table.Column<int>(type: "integer", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: true, defaultValue: 1),
                    ChickenTypeId = table.Column<Guid>(type: "uuid", nullable: true),
                    ChickenBatchId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Chicken_pkey", x => x.ChickenId);
                    table.ForeignKey(
                        name: "Chicken_ChickenBatchId_fkey",
                        column: x => x.ChickenBatchId,
                        principalTable: "ChickenBatch",
                        principalColumn: "ChickenBatchId");
                    table.ForeignKey(
                        name: "Chicken_ChickenTypeId_fkey",
                        column: x => x.ChickenTypeId,
                        principalTable: "SubCategory",
                        principalColumn: "SubCategoryId");
                    table.ForeignKey(
                        name: "FK_Chicken_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Chicken_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FeedLog",
                columns: table => new
                {
                    FeedLogId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    ChickenBatchId = table.Column<Guid>(type: "uuid", nullable: true),
                    FeedingDate = table.Column<DateTime>(name: "FeedingDate  ", type: "timestamp without time zone", nullable: true),
                    ActualFeedAmount = table.Column<decimal>(type: "numeric", nullable: true),
                    UnitId = table.Column<Guid>(type: "uuid", nullable: true),
                    TaskId = table.Column<Guid>(type: "uuid", nullable: true),
                    Note = table.Column<string>(type: "character varying", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("FeedLog_pkey", x => x.FeedLogId);
                    table.ForeignKey(
                        name: "FK_FeedLog_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeedLog_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FeedLog_ChickenBatchId_fkey",
                        column: x => x.ChickenBatchId,
                        principalTable: "ChickenBatch",
                        principalColumn: "ChickenBatchId");
                    table.ForeignKey(
                        name: "FeedLog_TaskId_fkey",
                        column: x => x.TaskId,
                        principalTable: "Task",
                        principalColumn: "TaskId");
                    table.ForeignKey(
                        name: "FeedLog_UnitId_fkey",
                        column: x => x.UnitId,
                        principalTable: "SubCategory",
                        principalColumn: "SubCategoryId");
                });

            migrationBuilder.CreateTable(
                name: "GrowthBatch",
                columns: table => new
                {
                    GrowthBatchId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    ChickenBatchId = table.Column<Guid>(type: "uuid", nullable: true),
                    GrowthStageId = table.Column<Guid>(type: "uuid", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    AvgWeight = table.Column<decimal>(type: "numeric", nullable: true),
                    MortalityRate = table.Column<decimal>(type: "numeric", nullable: true),
                    FeedConsumption = table.Column<decimal>(type: "numeric", nullable: true),
                    Note = table.Column<string>(type: "character varying", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: true, defaultValue: 1),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("GrowthBatch_pkey", x => x.GrowthBatchId);
                    table.ForeignKey(
                        name: "FK_GrowthBatch_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GrowthBatch_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "GrowthBatch_ChickenBatchId_fkey",
                        column: x => x.ChickenBatchId,
                        principalTable: "ChickenBatch",
                        principalColumn: "ChickenBatchId");
                    table.ForeignKey(
                        name: "GrowthBatch_GrowthStageId_fkey",
                        column: x => x.GrowthStageId,
                        principalTable: "GrowthStage",
                        principalColumn: "GrowthStageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HealthLog",
                columns: table => new
                {
                    HealthLogId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Notes = table.Column<string>(type: "character varying", nullable: true),
                    ChickenBatchId = table.Column<Guid>(type: "uuid", nullable: true),
                    TaskId = table.Column<Guid>(type: "uuid", nullable: true),
                    CheckedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Location = table.Column<string>(type: "character varying", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("HealthLog_pkey", x => x.HealthLogId);
                    table.ForeignKey(
                        name: "FK_HealthLog_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HealthLog_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "HealthLog_ChickenBatchId_fkey",
                        column: x => x.ChickenBatchId,
                        principalTable: "ChickenBatch",
                        principalColumn: "ChickenBatchId");
                    table.ForeignKey(
                        name: "HealthLog_TaskId_fkey",
                        column: x => x.TaskId,
                        principalTable: "Task",
                        principalColumn: "TaskId");
                });

            migrationBuilder.CreateTable(
                name: "QuantityLog",
                columns: table => new
                {
                    QuantityLogId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    ChickenBatchId = table.Column<Guid>(type: "uuid", nullable: true),
                    LogDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    Quantity = table.Column<int>(type: "integer", nullable: true),
                    LogType = table.Column<int>(type: "integer", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("QuantityLog_pkey", x => x.QuantityLogId);
                    table.ForeignKey(
                        name: "FK_QuantityLog_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuantityLog_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "QuantityLog_ChickenBatchId_fkey",
                        column: x => x.ChickenBatchId,
                        principalTable: "ChickenBatch",
                        principalColumn: "ChickenBatchId");
                });

            migrationBuilder.CreateTable(
                name: "VaccineLog",
                columns: table => new
                {
                    VaccineLogId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Notes = table.Column<string>(type: "character varying", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: true),
                    Reaction = table.Column<string>(type: "character varying", nullable: true),
                    ChickenBatchId = table.Column<Guid>(type: "uuid", nullable: true),
                    TaskId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("VaccineLog_pkey", x => x.VaccineLogId);
                    table.ForeignKey(
                        name: "FK_VaccineLog_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VaccineLog_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "VaccineLog_ChickenBatchId_fkey",
                        column: x => x.ChickenBatchId,
                        principalTable: "ChickenBatch",
                        principalColumn: "ChickenBatchId");
                    table.ForeignKey(
                        name: "VaccineLog_TaskId_fkey",
                        column: x => x.TaskId,
                        principalTable: "Task",
                        principalColumn: "TaskId");
                });

            migrationBuilder.CreateTable(
                name: "InventoryReceiptDetail",
                columns: table => new
                {
                    InventoryReceiptDetailId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    InventoryReceiptId = table.Column<Guid>(type: "uuid", nullable: true),
                    ActualQuantity = table.Column<decimal>(type: "numeric", nullable: true),
                    ActualDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Note = table.Column<string>(type: "character varying", nullable: true),
                    BatchNumber = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("InventoryReceiptDetail_pkey", x => x.InventoryReceiptDetailId);
                    table.ForeignKey(
                        name: "InventoryReceiptDetail_InventoryReceiptId_fkey",
                        column: x => x.InventoryReceiptId,
                        principalTable: "InventoryReceipt",
                        principalColumn: "InventoryReceiptId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChickenDetail",
                columns: table => new
                {
                    ChickenDetailId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    ChickenId = table.Column<Guid>(type: "uuid", nullable: true),
                    Weight = table.Column<decimal>(type: "numeric", nullable: true),
                    Quantity = table.Column<int>(type: "integer", nullable: true),
                    Gender = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ChickenDetail_pkey", x => x.ChickenDetailId);
                    table.ForeignKey(
                        name: "ChickenDetail_ChickenId_fkey",
                        column: x => x.ChickenId,
                        principalTable: "Chicken",
                        principalColumn: "ChickenId");
                });

            migrationBuilder.CreateTable(
                name: "ChickenNutrition",
                columns: table => new
                {
                    ChickenNutritionId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    NutritionPlanId = table.Column<Guid>(type: "uuid", nullable: true),
                    ChickenId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ChickenNutrition_pkey", x => x.ChickenNutritionId);
                    table.ForeignKey(
                        name: "ChickenNutrition_ChickenId_fkey",
                        column: x => x.ChickenId,
                        principalTable: "Chicken",
                        principalColumn: "ChickenId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "ChickenNutrition_NutritionPlanId_fkey",
                        column: x => x.NutritionPlanId,
                        principalTable: "NutritionPlan",
                        principalColumn: "NutritionPlanId");
                    table.ForeignKey(
                        name: "FK_ChickenNutrition_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChickenNutrition_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EvaluatedTarget",
                columns: table => new
                {
                    EvaluatedTargetId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    TargetTypeId = table.Column<Guid>(type: "uuid", nullable: true),
                    TargetId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("EvaluatedTarget_pkey", x => x.EvaluatedTargetId);
                    table.ForeignKey(
                        name: "EvaluatedTarget_TargetId_fkey",
                        column: x => x.TargetId,
                        principalTable: "Task",
                        principalColumn: "TaskId");
                    table.ForeignKey(
                        name: "EvaluatedTarget_TargetId_fkey1",
                        column: x => x.TargetId,
                        principalTable: "Chicken",
                        principalColumn: "ChickenId");
                    table.ForeignKey(
                        name: "EvaluatedTarget_TargetTypeId_fkey",
                        column: x => x.TargetTypeId,
                        principalTable: "SubCategory",
                        principalColumn: "SubCategoryId");
                    table.ForeignKey(
                        name: "FK_EvaluatedTarget_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EvaluatedTarget_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SystemConfig",
                columns: table => new
                {
                    SystemConfigId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    SettingName = table.Column<string>(type: "character varying", nullable: true),
                    SettingValue = table.Column<decimal>(type: "numeric", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    EffectedDateFrom = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EffectedDateTo = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EntityType = table.Column<string>(type: "character varying", nullable: true),
                    EntityId = table.Column<Guid>(type: "uuid", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: true, defaultValue: 1),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SystemConfig_pkey", x => x.SystemConfigId);
                    table.ForeignKey(
                        name: "ChickenCoop_ChickenCoopId_fkey",
                        column: x => x.EntityId,
                        principalTable: "ChickenCoop",
                        principalColumn: "ChickenCoopId");
                    table.ForeignKey(
                        name: "Chicken_ChickenId_fkey",
                        column: x => x.EntityId,
                        principalTable: "Chicken",
                        principalColumn: "ChickenId");
                    table.ForeignKey(
                        name: "FK_SystemConfig_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SystemConfig_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Task_TaskId_fkey",
                        column: x => x.EntityId,
                        principalTable: "Task",
                        principalColumn: "TaskId");
                    table.ForeignKey(
                        name: "Warehouse_WareId_fkey",
                        column: x => x.EntityId,
                        principalTable: "Warehouse",
                        principalColumn: "WareId");
                });

            migrationBuilder.CreateTable(
                name: "HealthLogDetail",
                columns: table => new
                {
                    HealthLogDetailId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    HealthLogId = table.Column<Guid>(type: "uuid", nullable: true),
                    CriteriaId = table.Column<Guid>(type: "uuid", nullable: true),
                    Result = table.Column<string>(type: "character varying", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("HealthLogDetail_pkey", x => x.HealthLogDetailId);
                    table.ForeignKey(
                        name: "HealthLogDetail_CriteriaId_fkey",
                        column: x => x.CriteriaId,
                        principalTable: "SubCategory",
                        principalColumn: "SubCategoryId");
                    table.ForeignKey(
                        name: "HealthLogDetail_HealthLogId_fkey",
                        column: x => x.HealthLogId,
                        principalTable: "HealthLog",
                        principalColumn: "HealthLogId");
                });

            migrationBuilder.CreateTable(
                name: "EvaluationResult",
                columns: table => new
                {
                    EvaluationResultId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    EvaluationTemplateId = table.Column<Guid>(type: "uuid", nullable: true),
                    EvaluatedTargetId = table.Column<Guid>(type: "uuid", nullable: true),
                    EvaluatedDate = table.Column<DateTime>(name: "EvaluatedDate ", type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("EvaluationResult_pkey", x => x.EvaluationResultId);
                    table.ForeignKey(
                        name: "EvaluationResult_EvaluatedTargetId_fkey",
                        column: x => x.EvaluatedTargetId,
                        principalTable: "EvaluatedTarget",
                        principalColumn: "EvaluatedTargetId");
                    table.ForeignKey(
                        name: "EvaluationResult_EvaluationTemplateId_fkey",
                        column: x => x.EvaluationTemplateId,
                        principalTable: "EvaluationTemplate",
                        principalColumn: "EvaluationTemplateId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_EvaluationResult_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EvaluationResult_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EvaluationResultDetail",
                columns: table => new
                {
                    EvaluationResultDetailId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    EvaluationResultId = table.Column<Guid>(type: "uuid", nullable: true),
                    ActualValue = table.Column<string>(type: "character varying", nullable: true),
                    IsPass = table.Column<int>(type: "integer", nullable: true, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("EvaluationResultDetail_pkey", x => x.EvaluationResultDetailId);
                    table.ForeignKey(
                        name: "EvaluationResultDetail_EvaluationResultId_fkey",
                        column: x => x.EvaluationResultId,
                        principalTable: "EvaluationResult",
                        principalColumn: "EvaluationResultId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assignment_AssignedToId",
                table: "Assignment",
                column: "AssignedToId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignment_CreatedByUserId",
                table: "Assignment",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignment_LastEditedByUserId",
                table: "Assignment",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignment_ShiftScheduleId",
                table: "Assignment",
                column: "ShiftScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignment_TaskId",
                table: "Assignment",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignment_TaskScheduleId",
                table: "Assignment",
                column: "TaskScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_BreedingArea_CreatedByUserId",
                table: "BreedingArea",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BreedingArea_FarmId",
                table: "BreedingArea",
                column: "FarmId");

            migrationBuilder.CreateIndex(
                name: "IX_BreedingArea_LastEditedByUserId",
                table: "BreedingArea",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_CreatedByUserId",
                table: "Category",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_LastEditedByUserId",
                table: "Category",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Chicken_ChickenBatchId",
                table: "Chicken",
                column: "ChickenBatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Chicken_ChickenTypeId",
                table: "Chicken",
                column: "ChickenTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Chicken_CreatedByUserId",
                table: "Chicken",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Chicken_LastEditedByUserId",
                table: "Chicken",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ChickenBatch_ChickenCoopId",
                table: "ChickenBatch",
                column: "ChickenCoopId");

            migrationBuilder.CreateIndex(
                name: "IX_ChickenBatch_CreatedByUserId",
                table: "ChickenBatch",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ChickenBatch_LastEditedByUserId",
                table: "ChickenBatch",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ChickenCoop_BreedingAreaId",
                table: "ChickenCoop",
                column: "BreedingAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_ChickenCoop_CreatedByUserId",
                table: "ChickenCoop",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ChickenCoop_DensityUnitId",
                table: "ChickenCoop",
                column: "DensityUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ChickenCoop_LastEditedByUserId",
                table: "ChickenCoop",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ChickenCoop_PurposeId",
                table: "ChickenCoop",
                column: "PurposeId");

            migrationBuilder.CreateIndex(
                name: "IX_ChickenDetail_ChickenId",
                table: "ChickenDetail",
                column: "ChickenId");

            migrationBuilder.CreateIndex(
                name: "IX_ChickenNutrition_ChickenId",
                table: "ChickenNutrition",
                column: "ChickenId");

            migrationBuilder.CreateIndex(
                name: "IX_ChickenNutrition_CreatedByUserId",
                table: "ChickenNutrition",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ChickenNutrition_LastEditedByUserId",
                table: "ChickenNutrition",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ChickenNutrition_NutritionPlanId",
                table: "ChickenNutrition",
                column: "NutritionPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_CoopEquipment_ChickenCoopId",
                table: "CoopEquipment",
                column: "ChickenCoopId");

            migrationBuilder.CreateIndex(
                name: "IX_CoopEquipment_CreatedByUserId",
                table: "CoopEquipment",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CoopEquipment_EquipmentId",
                table: "CoopEquipment",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CoopEquipment_LastEditedByUserId",
                table: "CoopEquipment",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_CreatedByUserId",
                table: "Equipment",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_LastEditedByUserId",
                table: "Equipment",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_SizeUnitId",
                table: "Equipment",
                column: "SizeUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_WeightUnitId",
                table: "Equipment",
                column: "WeightUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluatedTarget_CreatedByUserId",
                table: "EvaluatedTarget",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluatedTarget_LastEditedByUserId",
                table: "EvaluatedTarget",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluatedTarget_TargetId",
                table: "EvaluatedTarget",
                column: "TargetId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluatedTarget_TargetTypeId",
                table: "EvaluatedTarget",
                column: "TargetTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationResult_CreatedByUserId",
                table: "EvaluationResult",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationResult_EvaluatedTargetId",
                table: "EvaluationResult",
                column: "EvaluatedTargetId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationResult_EvaluationTemplateId",
                table: "EvaluationResult",
                column: "EvaluationTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationResult_LastEditedByUserId",
                table: "EvaluationResult",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationResultDetail_EvaluationResultId",
                table: "EvaluationResultDetail",
                column: "EvaluationResultId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationTemplate_CreatedByUserId",
                table: "EvaluationTemplate",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationTemplate_LastEditedByUserId",
                table: "EvaluationTemplate",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationTemplate_TemplateTypeId",
                table: "EvaluationTemplate",
                column: "TemplateTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Farm_CreatedByUserId",
                table: "Farm",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Farm_LastEditedByUserId",
                table: "Farm",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FarmEmployee_CreatedByUserId",
                table: "FarmEmployee",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FarmEmployee_FarmId",
                table: "FarmEmployee",
                column: "FarmId");

            migrationBuilder.CreateIndex(
                name: "IX_FarmEmployee_LastEditedByUserId",
                table: "FarmEmployee",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FarmEmployee_UserId",
                table: "FarmEmployee",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedLog_ChickenBatchId",
                table: "FeedLog",
                column: "ChickenBatchId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedLog_CreatedByUserId",
                table: "FeedLog",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedLog_LastEditedByUserId",
                table: "FeedLog",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedLog_TaskId",
                table: "FeedLog",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedLog_UnitId",
                table: "FeedLog",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedSession_CreatedByUserId",
                table: "FeedSession",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedSession_LastEditedByUserId",
                table: "FeedSession",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedSession_NutritionPlanId",
                table: "FeedSession",
                column: "NutritionPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedSession_UnitId",
                table: "FeedSession",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Food_CreatedByUserId",
                table: "Food",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Food_LastEditedByUserId",
                table: "Food",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_GrowthBatch_ChickenBatchId",
                table: "GrowthBatch",
                column: "ChickenBatchId");

            migrationBuilder.CreateIndex(
                name: "IX_GrowthBatch_CreatedByUserId",
                table: "GrowthBatch",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_GrowthBatch_GrowthStageId",
                table: "GrowthBatch",
                column: "GrowthStageId");

            migrationBuilder.CreateIndex(
                name: "IX_GrowthBatch_LastEditedByUserId",
                table: "GrowthBatch",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_GrowthNutrition_CreatedByUserId",
                table: "GrowthNutrition",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_GrowthNutrition_GrowthStageId",
                table: "GrowthNutrition",
                column: "GrowthStageId");

            migrationBuilder.CreateIndex(
                name: "IX_GrowthNutrition_LastEditedByUserId",
                table: "GrowthNutrition",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_GrowthNutrition_NutritionPlanId",
                table: "GrowthNutrition",
                column: "NutritionPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_GrowthStage_ChickenType",
                table: "GrowthStage",
                column: "ChickenType");

            migrationBuilder.CreateIndex(
                name: "IX_GrowthStage_CreatedByUserId",
                table: "GrowthStage",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_GrowthStage_LastEditedByUserId",
                table: "GrowthStage",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthLog_ChickenBatchId",
                table: "HealthLog",
                column: "ChickenBatchId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthLog_CreatedByUserId",
                table: "HealthLog",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthLog_LastEditedByUserId",
                table: "HealthLog",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthLog_TaskId",
                table: "HealthLog",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthLogDetail_CriteriaId",
                table: "HealthLogDetail",
                column: "CriteriaId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthLogDetail_HealthLogId",
                table: "HealthLogDetail",
                column: "HealthLogId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryReceipt_CreatedByUserId",
                table: "InventoryReceipt",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryReceipt_InventoryRequestId",
                table: "InventoryReceipt",
                column: "InventoryRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryReceipt_LastEditedByUserId",
                table: "InventoryReceipt",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryReceipt_ReceiptTypeId",
                table: "InventoryReceipt",
                column: "ReceiptTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryReceiptDetail_InventoryReceiptId",
                table: "InventoryReceiptDetail",
                column: "InventoryReceiptId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryRequest_CreatedByUserId",
                table: "InventoryRequest",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryRequest_InventoryRequestTypeId",
                table: "InventoryRequest",
                column: "InventoryRequestTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryRequest_LastEditedByUserId",
                table: "InventoryRequest",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryRequest_RequestId",
                table: "InventoryRequest",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryRequest_WareFromId",
                table: "InventoryRequest",
                column: "WareFromId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryRequest_WareToId",
                table: "InventoryRequest",
                column: "WareToId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryRequestDetail_InventoryRequestId",
                table: "InventoryRequestDetail",
                column: "InventoryRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryRequestDetail_ResourceId",
                table: "InventoryRequestDetail",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryRequestDetail_UnitId",
                table: "InventoryRequestDetail",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicine_CreatedByUserId",
                table: "Medicine",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicine_DiseaseId",
                table: "Medicine",
                column: "DiseaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicine_LastEditedByUserId",
                table: "Medicine",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_UserId",
                table: "Notification",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_NutritionPlan_CreatedByUserId",
                table: "NutritionPlan",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_NutritionPlan_LastEditedByUserId",
                table: "NutritionPlan",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_NutritionPlanDetail_FoodId",
                table: "NutritionPlanDetail",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_NutritionPlanDetail_NutritionPlanId",
                table: "NutritionPlanDetail",
                column: "NutritionPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_NutritionPlanDetail_UnitId",
                table: "NutritionPlanDetail",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_QuantityLog_ChickenBatchId",
                table: "QuantityLog",
                column: "ChickenBatchId");

            migrationBuilder.CreateIndex(
                name: "IX_QuantityLog_CreatedByUserId",
                table: "QuantityLog",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_QuantityLog_LastEditedByUserId",
                table: "QuantityLog",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_ApprovedById",
                table: "Request",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_Request_CreatedByUserId",
                table: "Request",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_LastEditedByUserId",
                table: "Request",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_RequestTypeId",
                table: "Request",
                column: "RequestTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Resource_CreatedByUserId",
                table: "Resource",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Resource_LastEditedByUserId",
                table: "Resource",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Resource_PackageId",
                table: "Resource",
                column: "PackageId");

            migrationBuilder.CreateIndex(
                name: "IX_Resource_ResourceTypeId",
                table: "Resource",
                column: "ResourceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Resource_UnitId",
                table: "Resource",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceSupplier_CreatedByUserId",
                table: "ResourceSupplier",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceSupplier_LastEditedByUserId",
                table: "ResourceSupplier",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceSupplier_PackagePriceId",
                table: "ResourceSupplier",
                column: "PackagePriceId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceSupplier_ResourceId",
                table: "ResourceSupplier",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceSupplier_SupplierId",
                table: "ResourceSupplier",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceSupplier_UnitPriceId",
                table: "ResourceSupplier",
                column: "UnitPriceId");

            migrationBuilder.CreateIndex(
                name: "IX_RevokedToken_UserId",
                table: "RevokedToken",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Shift_CreatedByUserId",
                table: "Shift",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Shift_LastEditedByUserId",
                table: "Shift",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftSchedule_ShiftId",
                table: "ShiftSchedule",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategory_CategoryId",
                table: "SubCategory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategory_CreatedByUserId",
                table: "SubCategory",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategory_LastEditedByUserId",
                table: "SubCategory",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_CreatedByUserId",
                table: "Supplier",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_LastEditedByUserId",
                table: "Supplier",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemConfig_CreatedByUserId",
                table: "SystemConfig",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemConfig_EntityId",
                table: "SystemConfig",
                column: "EntityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SystemConfig_LastEditedByUserId",
                table: "SystemConfig",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Task_CreatedByUserId",
                table: "Task",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Task_LastEditedByUserId",
                table: "Task",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Task_TaskTypeId",
                table: "Task",
                column: "TaskTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskHarvest_HarvestTypeId",
                table: "TaskHarvest",
                column: "HarvestTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskHarvest_TaskId",
                table: "TaskHarvest",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskLocation_LocationId",
                table: "TaskLocation",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskLocation_TaskId",
                table: "TaskLocation",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskLog_ChickenCoopId",
                table: "TaskLog",
                column: "ChickenCoopId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskLog_CreatedByUserId",
                table: "TaskLog",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskLog_LastEditedByUserId",
                table: "TaskLog",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskLog_TaskId",
                table: "TaskLog",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskRequest_RequestId",
                table: "TaskRequest",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskRequest_TaskTypeId",
                table: "TaskRequest",
                column: "TaskTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskResource_CreatedByUserId",
                table: "TaskResource",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskResource_LastEditedByUserId",
                table: "TaskResource",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskResource_ResourceTypeId",
                table: "TaskResource",
                column: "ResourceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskResource_TaskId",
                table: "TaskResource",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskSchedule_CreatedByUserId",
                table: "TaskSchedule",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskSchedule_LastEditedByUserId",
                table: "TaskSchedule",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateCriteria_CreatedByUserId",
                table: "TemplateCriteria",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateCriteria_CriteriaId",
                table: "TemplateCriteria",
                column: "CriteriaId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateCriteria_EvaluationTemplateId",
                table: "TemplateCriteria",
                column: "EvaluationTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateCriteria_LastEditedByUserId",
                table: "TemplateCriteria",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_VaccineLog_ChickenBatchId",
                table: "VaccineLog",
                column: "ChickenBatchId");

            migrationBuilder.CreateIndex(
                name: "IX_VaccineLog_CreatedByUserId",
                table: "VaccineLog",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_VaccineLog_LastEditedByUserId",
                table: "VaccineLog",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_VaccineLog_TaskId",
                table: "VaccineLog",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouse_CreatedByUserId",
                table: "Warehouse",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouse_FarmId",
                table: "Warehouse",
                column: "FarmId");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouse_LastEditedByUserId",
                table: "Warehouse",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouse_StorageTypeId",
                table: "Warehouse",
                column: "StorageTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_WarePermission_CreatedByUserId",
                table: "WarePermission",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WarePermission_LastEditedByUserId",
                table: "WarePermission",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WarePermission_UserId",
                table: "WarePermission",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WarePermission_WareId",
                table: "WarePermission",
                column: "WareId");

            migrationBuilder.CreateIndex(
                name: "IX_WareStock_ResourceId",
                table: "WareStock",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_WareStock_WareId",
                table: "WareStock",
                column: "WareId");

            migrationBuilder.CreateIndex(
                name: "IX_WareTransaction_CreatedByUserId",
                table: "WareTransaction",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WareTransaction_LastEditedByUserId",
                table: "WareTransaction",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WareTransaction_LocationFromId",
                table: "WareTransaction",
                column: "LocationFromId");

            migrationBuilder.CreateIndex(
                name: "IX_WareTransaction_LocationToId",
                table: "WareTransaction",
                column: "LocationToId");

            migrationBuilder.CreateIndex(
                name: "IX_WareTransaction_TransactionType",
                table: "WareTransaction",
                column: "TransactionType");

            migrationBuilder.CreateIndex(
                name: "IX_WareTransaction_WareId",
                table: "WareTransaction",
                column: "WareId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assignment");

            migrationBuilder.DropTable(
                name: "ChickenDetail");

            migrationBuilder.DropTable(
                name: "ChickenNutrition");

            migrationBuilder.DropTable(
                name: "CoopEquipment");

            migrationBuilder.DropTable(
                name: "EvaluationResultDetail");

            migrationBuilder.DropTable(
                name: "FarmEmployee");

            migrationBuilder.DropTable(
                name: "FeedLog");

            migrationBuilder.DropTable(
                name: "FeedSession");

            migrationBuilder.DropTable(
                name: "GrowthBatch");

            migrationBuilder.DropTable(
                name: "GrowthNutrition");

            migrationBuilder.DropTable(
                name: "HealthLogDetail");

            migrationBuilder.DropTable(
                name: "InventoryReceiptDetail");

            migrationBuilder.DropTable(
                name: "InventoryRequestDetail");

            migrationBuilder.DropTable(
                name: "Medicine");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "NutritionPlanDetail");

            migrationBuilder.DropTable(
                name: "QuantityLog");

            migrationBuilder.DropTable(
                name: "ResourceSupplier");

            migrationBuilder.DropTable(
                name: "RevokedToken");

            migrationBuilder.DropTable(
                name: "SystemConfig");

            migrationBuilder.DropTable(
                name: "TaskHarvest");

            migrationBuilder.DropTable(
                name: "TaskLocation");

            migrationBuilder.DropTable(
                name: "TaskLog");

            migrationBuilder.DropTable(
                name: "TaskRequest");

            migrationBuilder.DropTable(
                name: "TaskResource");

            migrationBuilder.DropTable(
                name: "TemplateCriteria");

            migrationBuilder.DropTable(
                name: "VaccineLog");

            migrationBuilder.DropTable(
                name: "WarePermission");

            migrationBuilder.DropTable(
                name: "WareStock");

            migrationBuilder.DropTable(
                name: "WareTransaction");

            migrationBuilder.DropTable(
                name: "TaskSchedule");

            migrationBuilder.DropTable(
                name: "ShiftSchedule");

            migrationBuilder.DropTable(
                name: "Equipment");

            migrationBuilder.DropTable(
                name: "EvaluationResult");

            migrationBuilder.DropTable(
                name: "GrowthStage");

            migrationBuilder.DropTable(
                name: "HealthLog");

            migrationBuilder.DropTable(
                name: "InventoryReceipt");

            migrationBuilder.DropTable(
                name: "Food");

            migrationBuilder.DropTable(
                name: "NutritionPlan");

            migrationBuilder.DropTable(
                name: "Supplier");

            migrationBuilder.DropTable(
                name: "Shift");

            migrationBuilder.DropTable(
                name: "EvaluatedTarget");

            migrationBuilder.DropTable(
                name: "EvaluationTemplate");

            migrationBuilder.DropTable(
                name: "InventoryRequest");

            migrationBuilder.DropTable(
                name: "Resource");

            migrationBuilder.DropTable(
                name: "Task");

            migrationBuilder.DropTable(
                name: "Chicken");

            migrationBuilder.DropTable(
                name: "Request");

            migrationBuilder.DropTable(
                name: "Warehouse");

            migrationBuilder.DropTable(
                name: "ChickenBatch");

            migrationBuilder.DropTable(
                name: "ChickenCoop");

            migrationBuilder.DropTable(
                name: "BreedingArea");

            migrationBuilder.DropTable(
                name: "SubCategory");

            migrationBuilder.DropTable(
                name: "Farm");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
