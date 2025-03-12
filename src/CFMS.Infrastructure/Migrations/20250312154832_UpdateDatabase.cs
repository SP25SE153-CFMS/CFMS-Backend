using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CFMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    userId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    fullName = table.Column<string>(type: "character varying", nullable: true),
                    phoneNumber = table.Column<string>(type: "character varying", nullable: true),
                    mail = table.Column<string>(type: "character varying", nullable: true),
                    avatar = table.Column<string>(type: "character varying", nullable: true),
                    dateOfBirth = table.Column<DateOnly>(type: "date", nullable: true),
                    startDate = table.Column<DateOnly>(type: "date", nullable: true),
                    status = table.Column<string>(type: "text", nullable: true),
                    address = table.Column<string>(type: "text", nullable: true),
                    CCCD = table.Column<string>(type: "character varying", nullable: true),
                    roleName = table.Column<string>(type: "character varying", nullable: true),
                    hashedPassword = table.Column<string>(type: "character varying", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("User_pkey", x => x.userId);
                });

            migrationBuilder.CreateTable(
                name: "Attendance",
                columns: table => new
                {
                    attendanceId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    userId = table.Column<Guid>(type: "uuid", nullable: true),
                    workDate = table.Column<DateOnly>(type: "date", nullable: true),
                    checkIn = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    checkOut = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Attendance_pkey", x => x.attendanceId);
                    table.ForeignKey(
                        name: "Attendance_userId_fkey",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "userId");
                    table.ForeignKey(
                        name: "FK_Attendance_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attendance_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    categoryId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    categoryType = table.Column<string>(type: "character varying", nullable: true),
                    categoryCode = table.Column<string>(type: "character varying", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Category_pkey", x => x.categoryId);
                    table.ForeignKey(
                        name: "FK_Category_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Category_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Equipment",
                columns: table => new
                {
                    equipmentId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    equipmentCode = table.Column<string>(type: "character varying", nullable: true),
                    equipmentName = table.Column<string>(type: "character varying", nullable: true),
                    purchaseDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    warrantyPeriod = table.Column<int>(type: "integer", nullable: true),
                    status = table.Column<string>(type: "text", nullable: true),
                    cost = table.Column<double>(type: "double precision", nullable: true),
                    quantity = table.Column<int>(type: "integer", nullable: true),
                    specifications = table.Column<string>(type: "character varying", nullable: true),
                    startDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    endDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    productId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Equipment_pkey", x => x.equipmentId);
                    table.UniqueConstraint("AK_Equipment_productId", x => x.productId);
                    table.ForeignKey(
                        name: "FK_Equipment_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Equipment_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Farm",
                columns: table => new
                {
                    farmId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    farmName = table.Column<string>(type: "character varying", nullable: true),
                    farmCode = table.Column<string>(type: "character varying", nullable: true),
                    address = table.Column<string>(type: "character varying", nullable: true),
                    area = table.Column<double>(type: "double precision", nullable: true),
                    scale = table.Column<int>(type: "integer", nullable: true),
                    phoneNumber = table.Column<string>(type: "character varying", nullable: true),
                    website = table.Column<string>(type: "character varying", nullable: true),
                    farmImage = table.Column<string>(type: "character varying", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Farm_pkey", x => x.farmId);
                    table.ForeignKey(
                        name: "FK_Farm_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Farm_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Farm_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "userId");
                });

            migrationBuilder.CreateTable(
                name: "FeedSchedule",
                columns: table => new
                {
                    feedScheduleId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    feedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    feedAmount = table.Column<double>(type: "double precision", nullable: true),
                    notes = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("FeedSchedule_pkey", x => x.feedScheduleId);
                    table.ForeignKey(
                        name: "FK_FeedSchedule_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FeedSchedule_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Food",
                columns: table => new
                {
                    foodId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    name = table.Column<string>(type: "character varying", nullable: true),
                    notes = table.Column<string>(type: "character varying", nullable: true),
                    ingredients = table.Column<string>(type: "character varying", nullable: true),
                    productionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    expiryDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    productId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Food_pkey", x => x.foodId);
                    table.UniqueConstraint("AK_Food_productId", x => x.productId);
                    table.ForeignKey(
                        name: "FK_Food_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Food_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    notificationId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    notificationName = table.Column<string>(type: "character varying", nullable: true),
                    type = table.Column<string>(type: "character varying", nullable: true),
                    content = table.Column<string>(type: "text", nullable: true),
                    isRead = table.Column<bool>(type: "boolean", nullable: true),
                    userId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Notification_pkey", x => x.notificationId);
                    table.ForeignKey(
                        name: "FK_Notification_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notification_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Notification_userId_fkey",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "userId");
                });

            migrationBuilder.CreateTable(
                name: "Performance",
                columns: table => new
                {
                    perId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    userId = table.Column<Guid>(type: "uuid", nullable: true),
                    totalTask = table.Column<int>(type: "integer", nullable: true),
                    completedTask = table.Column<int>(type: "integer", nullable: true),
                    delayTask = table.Column<int>(type: "integer", nullable: true),
                    completionRate = table.Column<double>(type: "double precision", nullable: true),
                    performanceRating = table.Column<double>(type: "double precision", nullable: true),
                    note = table.Column<string>(type: "text", nullable: true),
                    rangeTime = table.Column<string>(type: "character varying", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Performance_pkey", x => x.perId);
                    table.ForeignKey(
                        name: "FK_Performance_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Performance_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Performance_userId_fkey",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "userId");
                });

            migrationBuilder.CreateTable(
                name: "RevokedToken",
                columns: table => new
                {
                    revokedTokenId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    token = table.Column<string>(type: "character varying", nullable: false),
                    tokenType = table.Column<int>(type: "integer", nullable: false),
                    revokedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    expiryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    userId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("RevokedToken_pkey", x => x.revokedTokenId);
                    table.ForeignKey(
                        name: "FK_RevokedToken_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RevokedToken_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "RevokedToken_userId_fkey",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "userId");
                });

            migrationBuilder.CreateTable(
                name: "Salary",
                columns: table => new
                {
                    salaryId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    userId = table.Column<Guid>(type: "uuid", nullable: true),
                    basicSalary = table.Column<double>(type: "double precision", nullable: true),
                    bonus = table.Column<double>(type: "double precision", nullable: true),
                    deduction = table.Column<double>(type: "double precision", nullable: true),
                    final = table.Column<double>(type: "double precision", nullable: true),
                    status = table.Column<string>(type: "text", nullable: true),
                    totalHoursWorked = table.Column<int>(type: "integer", nullable: true),
                    overTimeHours = table.Column<int>(type: "integer", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Salary_pkey", x => x.salaryId);
                    table.ForeignKey(
                        name: "FK_Salary_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Salary_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Salary_userId_fkey",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "userId");
                });

            migrationBuilder.CreateTable(
                name: "Task",
                columns: table => new
                {
                    taskId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    taskName = table.Column<string>(type: "character varying", nullable: true),
                    taskType = table.Column<string>(type: "character varying", nullable: true),
                    status = table.Column<string>(type: "text", nullable: true),
                    location = table.Column<string>(type: "character varying", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Task_pkey", x => x.taskId);
                    table.ForeignKey(
                        name: "FK_Task_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Task_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubCategory",
                columns: table => new
                {
                    subCategoryId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    subCategoryName = table.Column<string>(type: "character varying", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<string>(type: "text", nullable: true),
                    dataType = table.Column<string>(type: "character varying", nullable: true),
                    createdDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    categoryId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SubCategory_pkey", x => x.subCategoryId);
                    table.ForeignKey(
                        name: "FK_SubCategory_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubCategory_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "SubCategory_categoryId_fkey",
                        column: x => x.categoryId,
                        principalTable: "Category",
                        principalColumn: "categoryId");
                });

            migrationBuilder.CreateTable(
                name: "BreedingArea",
                columns: table => new
                {
                    breedingAreaId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    breedingAreaCode = table.Column<string>(type: "character varying", nullable: true),
                    breedingAreaName = table.Column<string>(type: "character varying", nullable: true),
                    mealsPerDay = table.Column<int>(type: "integer", nullable: true),
                    image = table.Column<string>(type: "character varying", nullable: true),
                    notes = table.Column<string>(type: "text", nullable: true),
                    farmId = table.Column<Guid>(type: "uuid", nullable: true),
                    Area = table.Column<double>(type: "double precision", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("BreedingArea_pkey", x => x.breedingAreaId);
                    table.ForeignKey(
                        name: "BreedingArea_farmId_fkey",
                        column: x => x.farmId,
                        principalTable: "Farm",
                        principalColumn: "farmId");
                    table.ForeignKey(
                        name: "FK_BreedingArea_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BreedingArea_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FarmEmployee",
                columns: table => new
                {
                    farmEmployeeId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    farmId = table.Column<Guid>(type: "uuid", nullable: true),
                    employeeId = table.Column<Guid>(type: "uuid", nullable: true),
                    startDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    endDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    status = table.Column<int>(type: "integer", nullable: true),
                    roleName = table.Column<string>(type: "character varying", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("FarmEmployee_pkey", x => x.farmEmployeeId);
                    table.ForeignKey(
                        name: "FK_FarmEmployee_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FarmEmployee_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FarmEmployee_employeeId_fkey",
                        column: x => x.employeeId,
                        principalTable: "User",
                        principalColumn: "userId");
                    table.ForeignKey(
                        name: "FarmEmployee_farmId_fkey",
                        column: x => x.farmId,
                        principalTable: "Farm",
                        principalColumn: "farmId");
                });

            migrationBuilder.CreateTable(
                name: "Warehouse",
                columns: table => new
                {
                    wareId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    farmId = table.Column<Guid>(type: "uuid", nullable: true),
                    warehouseName = table.Column<string>(type: "character varying", nullable: true),
                    maxCapacity = table.Column<double>(type: "double precision", nullable: true),
                    totalWeight = table.Column<double>(type: "double precision", nullable: true),
                    totalQuantity = table.Column<int>(type: "integer", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Warehouse_pkey", x => x.wareId);
                    table.ForeignKey(
                        name: "FK_Warehouse_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Warehouse_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Warehouse_farmId_fkey",
                        column: x => x.farmId,
                        principalTable: "Farm",
                        principalColumn: "farmId");
                });

            migrationBuilder.CreateTable(
                name: "Nutrition",
                columns: table => new
                {
                    nutritionId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    name = table.Column<string>(type: "character varying", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    targetAudience = table.Column<string>(type: "character varying", nullable: true),
                    developmentStage = table.Column<string>(type: "character varying", nullable: true),
                    foodId = table.Column<Guid>(type: "uuid", nullable: true),
                    feedScheduleId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Nutrition_pkey", x => x.nutritionId);
                    table.ForeignKey(
                        name: "FK_Nutrition_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Nutrition_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Nutrition_feedScheduleId_fkey",
                        column: x => x.feedScheduleId,
                        principalTable: "FeedSchedule",
                        principalColumn: "feedScheduleId");
                    table.ForeignKey(
                        name: "Nutrition_foodId_fkey",
                        column: x => x.foodId,
                        principalTable: "Food",
                        principalColumn: "foodId");
                });

            migrationBuilder.CreateTable(
                name: "Assignment",
                columns: table => new
                {
                    assignmentId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    taskId = table.Column<Guid>(type: "uuid", nullable: true),
                    userId = table.Column<Guid>(type: "uuid", nullable: true),
                    assignedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deadlineDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    completedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    status = table.Column<string>(type: "text", nullable: true),
                    note = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Assignment_pkey", x => x.assignmentId);
                    table.ForeignKey(
                        name: "Assignment_taskId_fkey",
                        column: x => x.taskId,
                        principalTable: "Task",
                        principalColumn: "taskId");
                    table.ForeignKey(
                        name: "Assignment_userId_fkey",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "userId");
                    table.ForeignKey(
                        name: "FK_Assignment_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Assignment_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DailyTask",
                columns: table => new
                {
                    dTaskId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    taskId = table.Column<Guid>(type: "uuid", nullable: true),
                    taskDate = table.Column<DateOnly>(type: "date", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    itemId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("DailyTask_pkey", x => x.dTaskId);
                    table.ForeignKey(
                        name: "DailyTask_taskId_fkey",
                        column: x => x.taskId,
                        principalTable: "Task",
                        principalColumn: "taskId");
                    table.ForeignKey(
                        name: "FK_DailyTask_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DailyTask_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HarvestProduct",
                columns: table => new
                {
                    harvestProductId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    harvestProductName = table.Column<string>(type: "character varying", nullable: true),
                    quantity = table.Column<int>(type: "integer", nullable: true),
                    note = table.Column<string>(type: "text", nullable: true),
                    unitId = table.Column<Guid>(type: "uuid", nullable: true),
                    productId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("HarvestProduct_pkey", x => x.harvestProductId);
                    table.UniqueConstraint("AK_HarvestProduct_productId", x => x.productId);
                    table.ForeignKey(
                        name: "FK_HarvestProduct_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HarvestProduct_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "HarvestProduct_unitId_fkey",
                        column: x => x.unitId,
                        principalTable: "SubCategory",
                        principalColumn: "subCategoryId");
                });

            migrationBuilder.CreateTable(
                name: "Request",
                columns: table => new
                {
                    requestId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    requestTypeId = table.Column<Guid>(type: "uuid", nullable: true),
                    status = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    isEmergency = table.Column<bool>(type: "boolean", nullable: true),
                    approvedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    approvedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserId1 = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Request_pkey", x => x.requestId);
                    table.ForeignKey(
                        name: "FK_Request_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Request_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Request_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "userId");
                    table.ForeignKey(
                        name: "FK_Request_User_UserId1",
                        column: x => x.UserId1,
                        principalTable: "User",
                        principalColumn: "userId");
                    table.ForeignKey(
                        name: "Request_approvedBy_fkey",
                        column: x => x.approvedBy,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Request_requestTypeId_fkey",
                        column: x => x.requestTypeId,
                        principalTable: "SubCategory",
                        principalColumn: "subCategoryId");
                });

            migrationBuilder.CreateTable(
                name: "TaskEvaluation",
                columns: table => new
                {
                    taskEvalId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    categoryId = table.Column<Guid>(type: "uuid", nullable: true),
                    taskId = table.Column<Guid>(type: "uuid", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    totalCriteria = table.Column<int>(type: "integer", nullable: true),
                    passedCriteria = table.Column<int>(type: "integer", nullable: true),
                    failedCriteria = table.Column<int>(type: "integer", nullable: true),
                    overallResult = table.Column<string>(type: "character varying", nullable: true),
                    taskType = table.Column<string>(type: "character varying", nullable: true),
                    staffName = table.Column<string>(type: "character varying", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("TaskEvaluation_pkey", x => x.taskEvalId);
                    table.ForeignKey(
                        name: "FK_TaskEvaluation_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskEvaluation_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "TaskEvaluation_categoryId_fkey",
                        column: x => x.categoryId,
                        principalTable: "SubCategory",
                        principalColumn: "subCategoryId");
                    table.ForeignKey(
                        name: "TaskEvaluation_taskId_fkey",
                        column: x => x.taskId,
                        principalTable: "Task",
                        principalColumn: "taskId");
                });

            migrationBuilder.CreateTable(
                name: "Vaccine",
                columns: table => new
                {
                    vaccineId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    name = table.Column<string>(type: "character varying", nullable: true),
                    notes = table.Column<string>(type: "text", nullable: true),
                    productionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    expiryDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    dosage = table.Column<string>(type: "character varying", nullable: true),
                    instructions = table.Column<string>(type: "text", nullable: true),
                    batchNumber = table.Column<string>(type: "character varying", nullable: true),
                    supplierId = table.Column<Guid>(type: "uuid", nullable: true),
                    diseaseId = table.Column<Guid>(type: "uuid", nullable: true),
                    productId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Vaccine_pkey", x => x.vaccineId);
                    table.UniqueConstraint("AK_Vaccine_productId", x => x.productId);
                    table.ForeignKey(
                        name: "FK_Vaccine_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vaccine_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Vaccine_diseaseId_fkey",
                        column: x => x.diseaseId,
                        principalTable: "SubCategory",
                        principalColumn: "subCategoryId");
                    table.ForeignKey(
                        name: "Vaccine_supplierId_fkey",
                        column: x => x.supplierId,
                        principalTable: "SubCategory",
                        principalColumn: "subCategoryId");
                });

            migrationBuilder.CreateTable(
                name: "ChickenCoop",
                columns: table => new
                {
                    chickenCoopId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    chickenCoopCode = table.Column<string>(type: "character varying", nullable: true),
                    chickenCoopName = table.Column<string>(type: "character varying", nullable: true),
                    capacity = table.Column<int>(type: "integer", nullable: true),
                    area = table.Column<int>(type: "integer", nullable: true),
                    status = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "character varying", nullable: true),
                    purposeId = table.Column<Guid>(type: "uuid", nullable: true),
                    breedingAreaId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ChickenCoop_pkey", x => x.chickenCoopId);
                    table.ForeignKey(
                        name: "ChickenCoop_breedingAreaId_fkey",
                        column: x => x.breedingAreaId,
                        principalTable: "BreedingArea",
                        principalColumn: "breedingAreaId");
                    table.ForeignKey(
                        name: "ChickenCoop_purposeId_fkey",
                        column: x => x.purposeId,
                        principalTable: "SubCategory",
                        principalColumn: "subCategoryId");
                    table.ForeignKey(
                        name: "FK_ChickenCoop_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChickenCoop_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WarehousePermission",
                columns: table => new
                {
                    permissionId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    wareId = table.Column<Guid>(type: "uuid", nullable: true),
                    userId = table.Column<Guid>(type: "uuid", nullable: true),
                    grantedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("WarehousePermission_pkey", x => x.permissionId);
                    table.ForeignKey(
                        name: "WarehousePermission_userId_fkey",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "userId");
                    table.ForeignKey(
                        name: "WarehousePermission_wareId_fkey",
                        column: x => x.wareId,
                        principalTable: "Warehouse",
                        principalColumn: "wareId");
                });

            migrationBuilder.CreateTable(
                name: "HarvestTask",
                columns: table => new
                {
                    hTaskId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    taskId = table.Column<Guid>(type: "uuid", nullable: true),
                    harvestType = table.Column<string>(type: "character varying", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    quantityTypeId = table.Column<Guid>(type: "uuid", nullable: true),
                    harvestDate = table.Column<DateOnly>(type: "date", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("HarvestTask_pkey", x => x.hTaskId);
                    table.ForeignKey(
                        name: "FK_HarvestTask_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HarvestTask_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "HarvestTask_quantityTypeId_fkey",
                        column: x => x.quantityTypeId,
                        principalTable: "HarvestProduct",
                        principalColumn: "harvestProductId");
                    table.ForeignKey(
                        name: "HarvestTask_taskId_fkey",
                        column: x => x.taskId,
                        principalTable: "Task",
                        principalColumn: "taskId");
                });

            migrationBuilder.CreateTable(
                name: "RequestDetails",
                columns: table => new
                {
                    detailId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    requestId = table.Column<Guid>(type: "uuid", nullable: true),
                    expectedQuantity = table.Column<int>(type: "integer", nullable: true),
                    price = table.Column<int>(type: "integer", nullable: true),
                    itemId = table.Column<Guid>(type: "uuid", nullable: true),
                    locationFrom = table.Column<string>(type: "character varying", nullable: true),
                    locationTo = table.Column<string>(type: "character varying", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("RequestDetails_pkey", x => x.detailId);
                    table.ForeignKey(
                        name: "FK_RequestDetails_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestDetails_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "RequestDetails_requestId_fkey",
                        column: x => x.requestId,
                        principalTable: "Request",
                        principalColumn: "requestId");
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    productId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    productTypeId = table.Column<Guid>(type: "uuid", nullable: true),
                    quantity = table.Column<int>(type: "integer", nullable: true),
                    unit = table.Column<string>(type: "character varying", nullable: true),
                    package = table.Column<string>(type: "character varying", nullable: true),
                    usage = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Product_pkey", x => x.productId);
                    table.ForeignKey(
                        name: "FK_Product_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Product_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Product_productId_fkey",
                        column: x => x.productId,
                        principalTable: "HarvestProduct",
                        principalColumn: "productId");
                    table.ForeignKey(
                        name: "Product_productId_fkey1",
                        column: x => x.productId,
                        principalTable: "Equipment",
                        principalColumn: "productId");
                    table.ForeignKey(
                        name: "Product_productId_fkey2",
                        column: x => x.productId,
                        principalTable: "Vaccine",
                        principalColumn: "productId");
                    table.ForeignKey(
                        name: "Product_productId_fkey3",
                        column: x => x.productId,
                        principalTable: "Food",
                        principalColumn: "productId");
                    table.ForeignKey(
                        name: "Product_productTypeId_fkey",
                        column: x => x.productTypeId,
                        principalTable: "SubCategory",
                        principalColumn: "subCategoryId");
                });

            migrationBuilder.CreateTable(
                name: "ChickenBatch",
                columns: table => new
                {
                    chickenBatchId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    chickenCoopId = table.Column<Guid>(type: "uuid", nullable: true),
                    name = table.Column<string>(type: "character varying", nullable: true),
                    startDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    endDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    note = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<int>(type: "integer", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ChickenBatch_pkey", x => x.chickenBatchId);
                    table.ForeignKey(
                        name: "ChickenBatch_chickenCoopId_fkey",
                        column: x => x.chickenCoopId,
                        principalTable: "ChickenCoop",
                        principalColumn: "chickenCoopId");
                    table.ForeignKey(
                        name: "FK_ChickenBatch_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChickenBatch_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CoopEquipment",
                columns: table => new
                {
                    coopEquipmentId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    chickenCoopId = table.Column<Guid>(type: "uuid", nullable: true),
                    equipmentId = table.Column<Guid>(type: "uuid", nullable: true),
                    quantity = table.Column<int>(type: "integer", nullable: true),
                    assignedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    maintainDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    status = table.Column<string>(type: "text", nullable: true),
                    note = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("CoopEquipment_pkey", x => x.coopEquipmentId);
                    table.ForeignKey(
                        name: "CoopEquipment_chickenCoopId_fkey",
                        column: x => x.chickenCoopId,
                        principalTable: "ChickenCoop",
                        principalColumn: "chickenCoopId");
                    table.ForeignKey(
                        name: "CoopEquipment_equipmentId_fkey",
                        column: x => x.equipmentId,
                        principalTable: "Equipment",
                        principalColumn: "equipmentId");
                    table.ForeignKey(
                        name: "FK_CoopEquipment_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoopEquipment_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HarvestLog",
                columns: table => new
                {
                    harvestLogId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    chickenCoopId = table.Column<Guid>(type: "uuid", nullable: true),
                    date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    type = table.Column<string>(type: "character varying", nullable: true),
                    total = table.Column<int>(type: "integer", nullable: true),
                    note = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("HarvestLog_pkey", x => x.harvestLogId);
                    table.ForeignKey(
                        name: "FK_HarvestLog_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HarvestLog_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "HarvestLog_chickenCoopId_fkey",
                        column: x => x.chickenCoopId,
                        principalTable: "ChickenCoop",
                        principalColumn: "chickenCoopId");
                });

            migrationBuilder.CreateTable(
                name: "TaskLog",
                columns: table => new
                {
                    taskLogId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    type = table.Column<string>(type: "character varying", nullable: true),
                    chickenCoopId = table.Column<Guid>(type: "uuid", nullable: true),
                    startDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    endDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("TaskLog_pkey", x => x.taskLogId);
                    table.ForeignKey(
                        name: "FK_TaskLog_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskLog_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "TaskLog_chickenCoopId_fkey",
                        column: x => x.chickenCoopId,
                        principalTable: "ChickenCoop",
                        principalColumn: "chickenCoopId");
                });

            migrationBuilder.CreateTable(
                name: "StockReceipt",
                columns: table => new
                {
                    stockRepId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    detailId = table.Column<Guid>(type: "uuid", nullable: true),
                    stockReceiptType = table.Column<string>(type: "character varying", nullable: true),
                    itemType = table.Column<string>(type: "character varying", nullable: true),
                    actualQuantity = table.Column<int>(type: "integer", nullable: true),
                    locationFrom = table.Column<string>(type: "character varying", nullable: true),
                    locationTo = table.Column<string>(type: "character varying", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("StockReceipt_pkey", x => x.stockRepId);
                    table.ForeignKey(
                        name: "FK_StockReceipt_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockReceipt_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "StockReceipt_detailId_fkey",
                        column: x => x.detailId,
                        principalTable: "RequestDetails",
                        principalColumn: "detailId");
                });

            migrationBuilder.CreateTable(
                name: "WarehouseStock",
                columns: table => new
                {
                    wareStockId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    wareId = table.Column<Guid>(type: "uuid", nullable: true),
                    productId = table.Column<Guid>(type: "uuid", nullable: true),
                    quantity = table.Column<int>(type: "integer", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("WarehouseStock_pkey", x => x.wareStockId);
                    table.ForeignKey(
                        name: "FK_WarehouseStock_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WarehouseStock_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "WarehouseStock_productId_fkey",
                        column: x => x.productId,
                        principalTable: "Product",
                        principalColumn: "productId");
                    table.ForeignKey(
                        name: "WarehouseStock_wareId_fkey",
                        column: x => x.wareId,
                        principalTable: "Warehouse",
                        principalColumn: "wareId");
                });

            migrationBuilder.CreateTable(
                name: "WareTransaction",
                columns: table => new
                {
                    transactionId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    wareId = table.Column<Guid>(type: "uuid", nullable: true),
                    productId = table.Column<Guid>(type: "uuid", nullable: true),
                    quantity = table.Column<int>(type: "integer", nullable: true),
                    transactionType = table.Column<string>(type: "character varying", nullable: true),
                    reason = table.Column<string>(type: "text", nullable: true),
                    transactionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    locationFrom = table.Column<Guid>(type: "uuid", nullable: true),
                    locationTo = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("WareTransaction_pkey", x => x.transactionId);
                    table.ForeignKey(
                        name: "FK_WareTransaction_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WareTransaction_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "WareTransaction_productId_fkey",
                        column: x => x.productId,
                        principalTable: "Product",
                        principalColumn: "productId");
                    table.ForeignKey(
                        name: "WareTransaction_wareId_fkey",
                        column: x => x.wareId,
                        principalTable: "Warehouse",
                        principalColumn: "wareId");
                });

            migrationBuilder.CreateTable(
                name: "Flock",
                columns: table => new
                {
                    flockId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    quantity = table.Column<int>(type: "integer", nullable: true),
                    name = table.Column<string>(type: "character varying", nullable: true),
                    startDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    status = table.Column<int>(type: "integer", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    endDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    avgWeight = table.Column<double>(type: "double precision", nullable: true),
                    mortalityRate = table.Column<double>(type: "double precision", nullable: true),
                    lastHealthCheck = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    gender = table.Column<string>(type: "character varying", nullable: true),
                    purposeId = table.Column<Guid>(type: "uuid", nullable: true),
                    breedId = table.Column<Guid>(type: "uuid", nullable: true),
                    chickenBatchId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Flock_pkey", x => x.flockId);
                    table.ForeignKey(
                        name: "FK_Flock_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Flock_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Flock_breedId_fkey",
                        column: x => x.breedId,
                        principalTable: "SubCategory",
                        principalColumn: "subCategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Flock_chickenBatchId_fkey",
                        column: x => x.chickenBatchId,
                        principalTable: "ChickenBatch",
                        principalColumn: "chickenBatchId");
                    table.ForeignKey(
                        name: "Flock_purposeId_fkey",
                        column: x => x.purposeId,
                        principalTable: "SubCategory",
                        principalColumn: "subCategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HarvestDetail",
                columns: table => new
                {
                    harvestDetailId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    harvestLogId = table.Column<Guid>(type: "uuid", nullable: true),
                    typeProductId = table.Column<Guid>(type: "uuid", nullable: true),
                    quantity = table.Column<int>(type: "integer", nullable: true),
                    note = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("HarvestDetail_pkey", x => x.harvestDetailId);
                    table.ForeignKey(
                        name: "FK_HarvestDetail_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HarvestDetail_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "HarvestDetail_harvestLogId_fkey",
                        column: x => x.harvestLogId,
                        principalTable: "HarvestLog",
                        principalColumn: "harvestLogId");
                    table.ForeignKey(
                        name: "HarvestDetail_typeProductId_fkey",
                        column: x => x.typeProductId,
                        principalTable: "SubCategory",
                        principalColumn: "subCategoryId");
                });

            migrationBuilder.CreateTable(
                name: "TaskDetail",
                columns: table => new
                {
                    taskDetailId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    taskLogId = table.Column<Guid>(type: "uuid", nullable: true),
                    typeProductId = table.Column<Guid>(type: "uuid", nullable: true),
                    quantity = table.Column<int>(type: "integer", nullable: true),
                    note = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("TaskDetail_pkey", x => x.taskDetailId);
                    table.ForeignKey(
                        name: "FK_TaskDetail_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskDetail_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "TaskDetail_taskLogId_fkey",
                        column: x => x.taskLogId,
                        principalTable: "TaskLog",
                        principalColumn: "taskLogId");
                    table.ForeignKey(
                        name: "TaskDetail_typeProductId_fkey",
                        column: x => x.typeProductId,
                        principalTable: "SubCategory",
                        principalColumn: "subCategoryId");
                });

            migrationBuilder.CreateTable(
                name: "FlockNutrition",
                columns: table => new
                {
                    flockNutritionId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    flockId = table.Column<Guid>(type: "uuid", nullable: true),
                    nutritionId = table.Column<Guid>(type: "uuid", nullable: true),
                    startDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    endDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("FlockNutrition_pkey", x => x.flockNutritionId);
                    table.ForeignKey(
                        name: "FK_FlockNutrition_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FlockNutrition_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FlockNutrition_flockId_fkey",
                        column: x => x.flockId,
                        principalTable: "Flock",
                        principalColumn: "flockId");
                    table.ForeignKey(
                        name: "FlockNutrition_nutritionId_fkey",
                        column: x => x.nutritionId,
                        principalTable: "Nutrition",
                        principalColumn: "nutritionId");
                });

            migrationBuilder.CreateTable(
                name: "HealthLog",
                columns: table => new
                {
                    hLogId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    startDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    endDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    notes = table.Column<string>(type: "text", nullable: true),
                    flockId = table.Column<Guid>(type: "uuid", nullable: true),
                    staffId = table.Column<Guid>(type: "uuid", nullable: true),
                    location = table.Column<string>(type: "character varying", nullable: true),
                    temperature = table.Column<double>(type: "double precision", nullable: true),
                    humidity = table.Column<double>(type: "double precision", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("HealthLog_pkey", x => x.hLogId);
                    table.ForeignKey(
                        name: "FK_HealthLog_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HealthLog_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "HealthLog_flockId_fkey",
                        column: x => x.flockId,
                        principalTable: "Flock",
                        principalColumn: "flockId");
                });

            migrationBuilder.CreateTable(
                name: "QuantityLog",
                columns: table => new
                {
                    qLogId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    logDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    notes = table.Column<string>(type: "text", nullable: true),
                    quantity = table.Column<int>(type: "integer", nullable: true),
                    img = table.Column<string>(type: "character varying", nullable: true),
                    logType = table.Column<string>(type: "character varying", nullable: true),
                    flockId = table.Column<Guid>(type: "uuid", nullable: true),
                    reasonId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("QuantityLog_pkey", x => x.qLogId);
                    table.ForeignKey(
                        name: "FK_QuantityLog_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuantityLog_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuantityLog_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "userId");
                    table.ForeignKey(
                        name: "QuantityLog_flockId_fkey",
                        column: x => x.flockId,
                        principalTable: "Flock",
                        principalColumn: "flockId");
                    table.ForeignKey(
                        name: "QuantityLog_reasonId_fkey",
                        column: x => x.reasonId,
                        principalTable: "SubCategory",
                        principalColumn: "subCategoryId");
                });

            migrationBuilder.CreateTable(
                name: "VaccinationLog",
                columns: table => new
                {
                    vLogId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    vaccineId = table.Column<Guid>(type: "uuid", nullable: true),
                    flockId = table.Column<Guid>(type: "uuid", nullable: true),
                    dosage = table.Column<string>(type: "character varying", nullable: true),
                    notes = table.Column<string>(type: "text", nullable: true),
                    quantity = table.Column<int>(type: "integer", nullable: true),
                    status = table.Column<string>(type: "text", nullable: true),
                    reaction = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("VaccinationLog_pkey", x => x.vLogId);
                    table.ForeignKey(
                        name: "FK_VaccinationLog_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VaccinationLog_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "VaccinationLog_flockId_fkey",
                        column: x => x.flockId,
                        principalTable: "Flock",
                        principalColumn: "flockId");
                    table.ForeignKey(
                        name: "VaccinationLog_vaccineId_fkey",
                        column: x => x.vaccineId,
                        principalTable: "Vaccine",
                        principalColumn: "vaccineId");
                });

            migrationBuilder.CreateTable(
                name: "HealthLogDetail",
                columns: table => new
                {
                    logDetailId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    hLogId = table.Column<Guid>(type: "uuid", nullable: true),
                    criteriaId = table.Column<Guid>(type: "uuid", nullable: true),
                    result = table.Column<string>(type: "text", nullable: true),
                    checkedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("HealthLogDetail_pkey", x => x.logDetailId);
                    table.ForeignKey(
                        name: "FK_HealthLogDetail_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HealthLogDetail_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HealthLogDetail_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "userId");
                    table.ForeignKey(
                        name: "HealthLogDetail_criteriaId_fkey",
                        column: x => x.criteriaId,
                        principalTable: "SubCategory",
                        principalColumn: "subCategoryId");
                    table.ForeignKey(
                        name: "HealthLogDetail_hLogId_fkey",
                        column: x => x.hLogId,
                        principalTable: "HealthLog",
                        principalColumn: "hLogId");
                });

            migrationBuilder.CreateTable(
                name: "VaccinationEmployee",
                columns: table => new
                {
                    vaccinationEmployeeId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    employee = table.Column<Guid>(type: "uuid", nullable: true),
                    vaccinationLogId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("VaccinationEmployee_pkey", x => x.vaccinationEmployeeId);
                    table.ForeignKey(
                        name: "VaccinationEmployee_employee_fkey",
                        column: x => x.employee,
                        principalTable: "User",
                        principalColumn: "userId");
                    table.ForeignKey(
                        name: "VaccinationEmployee_vaccinationLogId_fkey",
                        column: x => x.vaccinationLogId,
                        principalTable: "VaccinationLog",
                        principalColumn: "vLogId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assignment_CreatedByUserId",
                table: "Assignment",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignment_LastEditedByUserId",
                table: "Assignment",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignment_taskId",
                table: "Assignment",
                column: "taskId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignment_userId",
                table: "Assignment",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_CreatedByUserId",
                table: "Attendance",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_LastEditedByUserId",
                table: "Attendance",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_userId",
                table: "Attendance",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_BreedingArea_CreatedByUserId",
                table: "BreedingArea",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BreedingArea_farmId",
                table: "BreedingArea",
                column: "farmId");

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
                name: "IX_ChickenBatch_chickenCoopId",
                table: "ChickenBatch",
                column: "chickenCoopId");

            migrationBuilder.CreateIndex(
                name: "IX_ChickenBatch_CreatedByUserId",
                table: "ChickenBatch",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ChickenBatch_LastEditedByUserId",
                table: "ChickenBatch",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ChickenCoop_breedingAreaId",
                table: "ChickenCoop",
                column: "breedingAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_ChickenCoop_CreatedByUserId",
                table: "ChickenCoop",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ChickenCoop_LastEditedByUserId",
                table: "ChickenCoop",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ChickenCoop_purposeId",
                table: "ChickenCoop",
                column: "purposeId");

            migrationBuilder.CreateIndex(
                name: "IX_CoopEquipment_chickenCoopId",
                table: "CoopEquipment",
                column: "chickenCoopId");

            migrationBuilder.CreateIndex(
                name: "IX_CoopEquipment_CreatedByUserId",
                table: "CoopEquipment",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CoopEquipment_equipmentId",
                table: "CoopEquipment",
                column: "equipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CoopEquipment_LastEditedByUserId",
                table: "CoopEquipment",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyTask_CreatedByUserId",
                table: "DailyTask",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyTask_LastEditedByUserId",
                table: "DailyTask",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyTask_taskId",
                table: "DailyTask",
                column: "taskId");

            migrationBuilder.CreateIndex(
                name: "Equipment_productId_key",
                table: "Equipment",
                column: "productId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_CreatedByUserId",
                table: "Equipment",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_LastEditedByUserId",
                table: "Equipment",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Farm_CreatedByUserId",
                table: "Farm",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Farm_LastEditedByUserId",
                table: "Farm",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Farm_UserId",
                table: "Farm",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FarmEmployee_CreatedByUserId",
                table: "FarmEmployee",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FarmEmployee_employeeId",
                table: "FarmEmployee",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_FarmEmployee_farmId",
                table: "FarmEmployee",
                column: "farmId");

            migrationBuilder.CreateIndex(
                name: "IX_FarmEmployee_LastEditedByUserId",
                table: "FarmEmployee",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedSchedule_CreatedByUserId",
                table: "FeedSchedule",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedSchedule_LastEditedByUserId",
                table: "FeedSchedule",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Flock_breedId",
                table: "Flock",
                column: "breedId");

            migrationBuilder.CreateIndex(
                name: "IX_Flock_chickenBatchId",
                table: "Flock",
                column: "chickenBatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Flock_CreatedByUserId",
                table: "Flock",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Flock_LastEditedByUserId",
                table: "Flock",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Flock_purposeId",
                table: "Flock",
                column: "purposeId");

            migrationBuilder.CreateIndex(
                name: "IX_FlockNutrition_CreatedByUserId",
                table: "FlockNutrition",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FlockNutrition_flockId",
                table: "FlockNutrition",
                column: "flockId");

            migrationBuilder.CreateIndex(
                name: "IX_FlockNutrition_LastEditedByUserId",
                table: "FlockNutrition",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FlockNutrition_nutritionId",
                table: "FlockNutrition",
                column: "nutritionId");

            migrationBuilder.CreateIndex(
                name: "Food_productId_key",
                table: "Food",
                column: "productId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Food_CreatedByUserId",
                table: "Food",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Food_LastEditedByUserId",
                table: "Food",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_HarvestDetail_CreatedByUserId",
                table: "HarvestDetail",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_HarvestDetail_harvestLogId",
                table: "HarvestDetail",
                column: "harvestLogId");

            migrationBuilder.CreateIndex(
                name: "IX_HarvestDetail_LastEditedByUserId",
                table: "HarvestDetail",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_HarvestDetail_typeProductId",
                table: "HarvestDetail",
                column: "typeProductId");

            migrationBuilder.CreateIndex(
                name: "IX_HarvestLog_chickenCoopId",
                table: "HarvestLog",
                column: "chickenCoopId");

            migrationBuilder.CreateIndex(
                name: "IX_HarvestLog_CreatedByUserId",
                table: "HarvestLog",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_HarvestLog_LastEditedByUserId",
                table: "HarvestLog",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "HarvestProduct_productId_key",
                table: "HarvestProduct",
                column: "productId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HarvestProduct_CreatedByUserId",
                table: "HarvestProduct",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_HarvestProduct_LastEditedByUserId",
                table: "HarvestProduct",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_HarvestProduct_unitId",
                table: "HarvestProduct",
                column: "unitId");

            migrationBuilder.CreateIndex(
                name: "IX_HarvestTask_CreatedByUserId",
                table: "HarvestTask",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_HarvestTask_LastEditedByUserId",
                table: "HarvestTask",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_HarvestTask_quantityTypeId",
                table: "HarvestTask",
                column: "quantityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_HarvestTask_taskId",
                table: "HarvestTask",
                column: "taskId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthLog_CreatedByUserId",
                table: "HealthLog",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthLog_flockId",
                table: "HealthLog",
                column: "flockId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthLog_LastEditedByUserId",
                table: "HealthLog",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthLogDetail_CreatedByUserId",
                table: "HealthLogDetail",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthLogDetail_criteriaId",
                table: "HealthLogDetail",
                column: "criteriaId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthLogDetail_hLogId",
                table: "HealthLogDetail",
                column: "hLogId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthLogDetail_LastEditedByUserId",
                table: "HealthLogDetail",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthLogDetail_UserId",
                table: "HealthLogDetail",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_CreatedByUserId",
                table: "Notification",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_LastEditedByUserId",
                table: "Notification",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_userId",
                table: "Notification",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Nutrition_CreatedByUserId",
                table: "Nutrition",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Nutrition_feedScheduleId",
                table: "Nutrition",
                column: "feedScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Nutrition_foodId",
                table: "Nutrition",
                column: "foodId");

            migrationBuilder.CreateIndex(
                name: "IX_Nutrition_LastEditedByUserId",
                table: "Nutrition",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Performance_CreatedByUserId",
                table: "Performance",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Performance_LastEditedByUserId",
                table: "Performance",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Performance_userId",
                table: "Performance",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CreatedByUserId",
                table: "Product",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_LastEditedByUserId",
                table: "Product",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_productTypeId",
                table: "Product",
                column: "productTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_QuantityLog_CreatedByUserId",
                table: "QuantityLog",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_QuantityLog_flockId",
                table: "QuantityLog",
                column: "flockId");

            migrationBuilder.CreateIndex(
                name: "IX_QuantityLog_LastEditedByUserId",
                table: "QuantityLog",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_QuantityLog_reasonId",
                table: "QuantityLog",
                column: "reasonId");

            migrationBuilder.CreateIndex(
                name: "IX_QuantityLog_UserId",
                table: "QuantityLog",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_approvedBy",
                table: "Request",
                column: "approvedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Request_CreatedByUserId",
                table: "Request",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_LastEditedByUserId",
                table: "Request",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_requestTypeId",
                table: "Request",
                column: "requestTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_UserId",
                table: "Request",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_UserId1",
                table: "Request",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_RequestDetails_CreatedByUserId",
                table: "RequestDetails",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestDetails_LastEditedByUserId",
                table: "RequestDetails",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestDetails_requestId",
                table: "RequestDetails",
                column: "requestId");

            migrationBuilder.CreateIndex(
                name: "IX_RevokedToken_CreatedByUserId",
                table: "RevokedToken",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RevokedToken_LastEditedByUserId",
                table: "RevokedToken",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RevokedToken_userId",
                table: "RevokedToken",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Salary_CreatedByUserId",
                table: "Salary",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Salary_LastEditedByUserId",
                table: "Salary",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Salary_userId",
                table: "Salary",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_StockReceipt_CreatedByUserId",
                table: "StockReceipt",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StockReceipt_detailId",
                table: "StockReceipt",
                column: "detailId");

            migrationBuilder.CreateIndex(
                name: "IX_StockReceipt_LastEditedByUserId",
                table: "StockReceipt",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategory_categoryId",
                table: "SubCategory",
                column: "categoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategory_CreatedByUserId",
                table: "SubCategory",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategory_LastEditedByUserId",
                table: "SubCategory",
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
                name: "IX_TaskDetail_CreatedByUserId",
                table: "TaskDetail",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskDetail_LastEditedByUserId",
                table: "TaskDetail",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskDetail_taskLogId",
                table: "TaskDetail",
                column: "taskLogId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskDetail_typeProductId",
                table: "TaskDetail",
                column: "typeProductId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskEvaluation_categoryId",
                table: "TaskEvaluation",
                column: "categoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskEvaluation_CreatedByUserId",
                table: "TaskEvaluation",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskEvaluation_LastEditedByUserId",
                table: "TaskEvaluation",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskEvaluation_taskId",
                table: "TaskEvaluation",
                column: "taskId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskLog_chickenCoopId",
                table: "TaskLog",
                column: "chickenCoopId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskLog_CreatedByUserId",
                table: "TaskLog",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskLog_LastEditedByUserId",
                table: "TaskLog",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_VaccinationEmployee_employee",
                table: "VaccinationEmployee",
                column: "employee");

            migrationBuilder.CreateIndex(
                name: "IX_VaccinationEmployee_vaccinationLogId",
                table: "VaccinationEmployee",
                column: "vaccinationLogId");

            migrationBuilder.CreateIndex(
                name: "IX_VaccinationLog_CreatedByUserId",
                table: "VaccinationLog",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_VaccinationLog_flockId",
                table: "VaccinationLog",
                column: "flockId");

            migrationBuilder.CreateIndex(
                name: "IX_VaccinationLog_LastEditedByUserId",
                table: "VaccinationLog",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_VaccinationLog_vaccineId",
                table: "VaccinationLog",
                column: "vaccineId");

            migrationBuilder.CreateIndex(
                name: "IX_Vaccine_CreatedByUserId",
                table: "Vaccine",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Vaccine_diseaseId",
                table: "Vaccine",
                column: "diseaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Vaccine_LastEditedByUserId",
                table: "Vaccine",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Vaccine_supplierId",
                table: "Vaccine",
                column: "supplierId");

            migrationBuilder.CreateIndex(
                name: "Vaccine_productId_key",
                table: "Vaccine",
                column: "productId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Warehouse_CreatedByUserId",
                table: "Warehouse",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouse_farmId",
                table: "Warehouse",
                column: "farmId");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouse_LastEditedByUserId",
                table: "Warehouse",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehousePermission_userId",
                table: "WarehousePermission",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehousePermission_wareId",
                table: "WarehousePermission",
                column: "wareId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseStock_CreatedByUserId",
                table: "WarehouseStock",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseStock_LastEditedByUserId",
                table: "WarehouseStock",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseStock_productId",
                table: "WarehouseStock",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseStock_wareId",
                table: "WarehouseStock",
                column: "wareId");

            migrationBuilder.CreateIndex(
                name: "IX_WareTransaction_CreatedByUserId",
                table: "WareTransaction",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WareTransaction_LastEditedByUserId",
                table: "WareTransaction",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WareTransaction_productId",
                table: "WareTransaction",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_WareTransaction_wareId",
                table: "WareTransaction",
                column: "wareId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assignment");

            migrationBuilder.DropTable(
                name: "Attendance");

            migrationBuilder.DropTable(
                name: "CoopEquipment");

            migrationBuilder.DropTable(
                name: "DailyTask");

            migrationBuilder.DropTable(
                name: "FarmEmployee");

            migrationBuilder.DropTable(
                name: "FlockNutrition");

            migrationBuilder.DropTable(
                name: "HarvestDetail");

            migrationBuilder.DropTable(
                name: "HarvestTask");

            migrationBuilder.DropTable(
                name: "HealthLogDetail");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "Performance");

            migrationBuilder.DropTable(
                name: "QuantityLog");

            migrationBuilder.DropTable(
                name: "RevokedToken");

            migrationBuilder.DropTable(
                name: "Salary");

            migrationBuilder.DropTable(
                name: "StockReceipt");

            migrationBuilder.DropTable(
                name: "TaskDetail");

            migrationBuilder.DropTable(
                name: "TaskEvaluation");

            migrationBuilder.DropTable(
                name: "VaccinationEmployee");

            migrationBuilder.DropTable(
                name: "WarehousePermission");

            migrationBuilder.DropTable(
                name: "WarehouseStock");

            migrationBuilder.DropTable(
                name: "WareTransaction");

            migrationBuilder.DropTable(
                name: "Nutrition");

            migrationBuilder.DropTable(
                name: "HarvestLog");

            migrationBuilder.DropTable(
                name: "HealthLog");

            migrationBuilder.DropTable(
                name: "RequestDetails");

            migrationBuilder.DropTable(
                name: "TaskLog");

            migrationBuilder.DropTable(
                name: "Task");

            migrationBuilder.DropTable(
                name: "VaccinationLog");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Warehouse");

            migrationBuilder.DropTable(
                name: "FeedSchedule");

            migrationBuilder.DropTable(
                name: "Request");

            migrationBuilder.DropTable(
                name: "Flock");

            migrationBuilder.DropTable(
                name: "HarvestProduct");

            migrationBuilder.DropTable(
                name: "Equipment");

            migrationBuilder.DropTable(
                name: "Vaccine");

            migrationBuilder.DropTable(
                name: "Food");

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
