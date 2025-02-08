using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CFMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddUserLogRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "breed",
                columns: table => new
                {
                    breedid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    name = table.Column<string>(type: "text", nullable: false),
                    img = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("breed_pkey", x => x.breedid);
                });

            migrationBuilder.CreateTable(
                name: "disease",
                columns: table => new
                {
                    diseaseid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    diseasetype = table.Column<string>(type: "text", nullable: true),
                    cause = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("disease_pkey", x => x.diseaseid);
                });

            migrationBuilder.CreateTable(
                name: "equipment",
                columns: table => new
                {
                    equipmentid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    equipmentname = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    type = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    purchasedate = table.Column<DateOnly>(type: "date", nullable: false),
                    warrantyperiod = table.Column<TimeSpan>(type: "interval", nullable: true),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    cost = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: true),
                    note = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("equipment_pkey", x => x.equipmentid);
                });

            migrationBuilder.CreateTable(
                name: "feedschedule",
                columns: table => new
                {
                    feedscheduleid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    feedtime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    feedamount = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    notes = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("feedschedule_pkey", x => x.feedscheduleid);
                });

            migrationBuilder.CreateTable(
                name: "healthcriteria",
                columns: table => new
                {
                    criteriaid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    name = table.Column<string>(type: "text", nullable: false),
                    characteristic = table.Column<string>(type: "text", nullable: false),
                    unit = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("healthcriteria_pkey", x => x.criteriaid);
                });

            migrationBuilder.CreateTable(
                name: "purpose",
                columns: table => new
                {
                    purposeid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("purpose_pkey", x => x.purposeid);
                });

            migrationBuilder.CreateTable(
                name: "reason",
                columns: table => new
                {
                    reasonid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("reason_pkey", x => x.reasonid);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    roleid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    rolename = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    permission = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("roles_pkey", x => x.roleid);
                });

            migrationBuilder.CreateTable(
                name: "supplier",
                columns: table => new
                {
                    supplierid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    contactinformation = table.Column<string>(type: "text", nullable: false),
                    address = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("supplier_pkey", x => x.supplierid);
                });

            migrationBuilder.CreateTable(
                name: "time_keeping_types",
                columns: table => new
                {
                    timetypeid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    typename = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    unitsalary = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("time_keeping_types_pkey", x => x.timetypeid);
                });

            migrationBuilder.CreateTable(
                name: "flock",
                columns: table => new
                {
                    flockid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    name = table.Column<string>(type: "text", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    startdate = table.Column<DateOnly>(type: "date", nullable: false),
                    enddate = table.Column<DateOnly>(type: "date", nullable: true),
                    status = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    purposeid = table.Column<Guid>(type: "uuid", nullable: false),
                    breedid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("flock_pkey", x => x.flockid);
                    table.ForeignKey(
                        name: "fk_flock_breed",
                        column: x => x.breedid,
                        principalTable: "breed",
                        principalColumn: "breedid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_flock_purpose",
                        column: x => x.purposeid,
                        principalTable: "purpose",
                        principalColumn: "purposeid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    userid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    fullname = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    dateofbirth = table.Column<DateOnly>(type: "date", nullable: false),
                    startdate = table.Column<DateOnly>(type: "date", nullable: false),
                    status = table.Column<bool>(type: "boolean", nullable: true, defaultValue: true),
                    address = table.Column<string>(type: "text", nullable: true),
                    cccd = table.Column<string>(type: "character varying(12)", maxLength: 12, nullable: false),
                    roleid = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("users_pkey", x => x.userid);
                    table.ForeignKey(
                        name: "fk_users_role",
                        column: x => x.roleid,
                        principalTable: "roles",
                        principalColumn: "roleid",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "food",
                columns: table => new
                {
                    foodid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    name = table.Column<string>(type: "text", nullable: false),
                    expirydate = table.Column<DateOnly>(type: "date", nullable: false),
                    ingredients = table.Column<string>(type: "text", nullable: false),
                    usage = table.Column<string>(type: "text", nullable: true),
                    notes = table.Column<string>(type: "text", nullable: true),
                    supplierid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("food_pkey", x => x.foodid);
                    table.ForeignKey(
                        name: "food_supplierid_fkey",
                        column: x => x.supplierid,
                        principalTable: "supplier",
                        principalColumn: "supplierid",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    productid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    productcode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    productname = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    storagelocation = table.Column<string>(type: "text", nullable: false),
                    unit = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    minstock = table.Column<int>(type: "integer", nullable: true),
                    maxstock = table.Column<int>(type: "integer", nullable: true),
                    supplierid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("product_pkey", x => x.productid);
                    table.ForeignKey(
                        name: "product_supplierid_fkey",
                        column: x => x.supplierid,
                        principalTable: "supplier",
                        principalColumn: "supplierid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "vaccine",
                columns: table => new
                {
                    vaccineid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    name = table.Column<string>(type: "text", nullable: false),
                    notes = table.Column<string>(type: "text", nullable: true),
                    productiondate = table.Column<DateOnly>(type: "date", nullable: false),
                    expirydate = table.Column<DateOnly>(type: "date", nullable: false),
                    dosage = table.Column<string>(type: "text", nullable: false),
                    instructions = table.Column<string>(type: "text", nullable: true),
                    batchnumber = table.Column<string>(type: "text", nullable: false),
                    supplierid = table.Column<Guid>(type: "uuid", nullable: false),
                    diseaseid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("vaccine_pkey", x => x.vaccineid);
                    table.ForeignKey(
                        name: "vaccine_diseaseid_fkey",
                        column: x => x.diseaseid,
                        principalTable: "disease",
                        principalColumn: "diseaseid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "vaccine_supplierid_fkey",
                        column: x => x.supplierid,
                        principalTable: "supplier",
                        principalColumn: "supplierid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "water",
                columns: table => new
                {
                    waterid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    name = table.Column<string>(type: "text", nullable: false),
                    ingredients = table.Column<string>(type: "text", nullable: false),
                    mixingratio = table.Column<string>(type: "text", nullable: false),
                    usage = table.Column<string>(type: "text", nullable: true),
                    instructions = table.Column<string>(type: "text", nullable: true),
                    targetaudience = table.Column<string>(type: "text", nullable: true),
                    expirydate = table.Column<DateOnly>(type: "date", nullable: false),
                    notes = table.Column<string>(type: "text", nullable: true),
                    supplierid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("water_pkey", x => x.waterid);
                    table.ForeignKey(
                        name: "water_supplierid_fkey",
                        column: x => x.supplierid,
                        principalTable: "supplier",
                        principalColumn: "supplierid",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "healthlog",
                columns: table => new
                {
                    hlogid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    logdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    notes = table.Column<string>(type: "text", nullable: true),
                    flockid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("healthlog_pkey", x => x.hlogid);
                    table.ForeignKey(
                        name: "healthlog_flockid_fkey",
                        column: x => x.flockid,
                        principalTable: "flock",
                        principalColumn: "flockid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "quantitylog",
                columns: table => new
                {
                    qlogid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    logdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    notes = table.Column<string>(type: "text", nullable: true),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    logtype = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    flockid = table.Column<Guid>(type: "uuid", nullable: false),
                    reasonid = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("quantitylog_pkey", x => x.qlogid);
                    table.ForeignKey(
                        name: "quantitylog_flockid_fkey",
                        column: x => x.flockid,
                        principalTable: "flock",
                        principalColumn: "flockid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "quantitylog_reasonid_fkey",
                        column: x => x.reasonid,
                        principalTable: "reason",
                        principalColumn: "reasonid",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "farm",
                columns: table => new
                {
                    farmid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    farmname = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    farmcode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    userid = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    address = table.Column<string>(type: "text", nullable: true),
                    area = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true),
                    scale = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    phonenumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    website = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    farmimage = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("farm_pkey", x => x.farmid);
                    table.ForeignKey(
                        name: "farm_userid_fkey",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "performancestatistic",
                columns: table => new
                {
                    perstaid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    userid = table.Column<Guid>(type: "uuid", nullable: false),
                    totaltask = table.Column<int>(type: "integer", nullable: true, defaultValue: 0),
                    completedtask = table.Column<int>(type: "integer", nullable: true, defaultValue: 0),
                    delaytask = table.Column<int>(type: "integer", nullable: true, defaultValue: 0),
                    completionrate = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: true, computedColumnSql: "\nCASE\n    WHEN (totaltask > 0) THEN (((completedtask)::numeric * 100.0) / (totaltask)::numeric)\n    ELSE (0)::numeric\nEND", stored: true),
                    performancerating = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    note = table.Column<string>(type: "text", nullable: true),
                    rangetime = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("performancestatistic_pkey", x => x.perstaid);
                    table.ForeignKey(
                        name: "performancestatistic_userid_fkey",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "salary",
                columns: table => new
                {
                    salaryid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    userid = table.Column<Guid>(type: "uuid", nullable: false),
                    basicsalary = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    bonus = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: true, defaultValueSql: "0"),
                    deduction = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: true, defaultValueSql: "0"),
                    totalhoursworked = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: true, defaultValueSql: "0"),
                    overtimehours = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: true, defaultValueSql: "0"),
                    finalsalary = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: true, computedColumnSql: "((basicsalary + bonus) - deduction)", stored: true),
                    status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    salarymonth = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("salary_pkey", x => x.salaryid);
                    table.ForeignKey(
                        name: "salary_userid_fkey",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "task",
                columns: table => new
                {
                    taskid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    taskname = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    tasktype = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    createddate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "now()"),
                    duedate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    location = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    userid = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("task_pkey", x => x.taskid);
                    table.ForeignKey(
                        name: "task_userid_fkey",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "time_keeping",
                columns: table => new
                {
                    timekeepingid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    workdate = table.Column<DateOnly>(type: "date", nullable: false),
                    endtime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    userid = table.Column<Guid>(type: "uuid", nullable: false),
                    timekeepingtype = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("time_keeping_pkey", x => x.timekeepingid);
                    table.ForeignKey(
                        name: "time_keeping_timekeepingtype_fkey",
                        column: x => x.timekeepingtype,
                        principalTable: "time_keeping_types",
                        principalColumn: "timetypeid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "time_keeping_userid_fkey",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "expireddamaged",
                columns: table => new
                {
                    edproductid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    reason = table.Column<string>(type: "text", nullable: false),
                    productid = table.Column<Guid>(type: "uuid", nullable: false),
                    actiontaken = table.Column<string>(type: "text", nullable: false),
                    recorddate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("expireddamaged_pkey", x => x.edproductid);
                    table.ForeignKey(
                        name: "expireddamaged_productid_fkey",
                        column: x => x.productid,
                        principalTable: "product",
                        principalColumn: "productid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "inventoryaudit",
                columns: table => new
                {
                    auditid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    auditdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    userid = table.Column<Guid>(type: "uuid", nullable: false),
                    productid = table.Column<Guid>(type: "uuid", nullable: false),
                    systemquantity = table.Column<int>(type: "integer", nullable: false),
                    actualquantity = table.Column<int>(type: "integer", nullable: false),
                    condition = table.Column<string>(type: "text", nullable: false),
                    reason = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("inventoryaudit_pkey", x => x.auditid);
                    table.ForeignKey(
                        name: "inventoryaudit_productid_fkey",
                        column: x => x.productid,
                        principalTable: "product",
                        principalColumn: "productid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "inventoryaudit_userid_fkey",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "vaccinationlog",
                columns: table => new
                {
                    vlogid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    vaccinationdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    dosage = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: false),
                    notes = table.Column<string>(type: "text", nullable: true),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    vaccineid = table.Column<Guid>(type: "uuid", nullable: false),
                    flockid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("vaccinationlog_pkey", x => x.vlogid);
                    table.ForeignKey(
                        name: "vaccinationlog_flockid_fkey",
                        column: x => x.flockid,
                        principalTable: "flock",
                        principalColumn: "flockid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "vaccinationlog_vaccineid_fkey",
                        column: x => x.vaccineid,
                        principalTable: "vaccine",
                        principalColumn: "vaccineid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "nutrition",
                columns: table => new
                {
                    nutritionid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    targetaudience = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    developmentstage = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    foodid = table.Column<Guid>(type: "uuid", nullable: false),
                    waterid = table.Column<Guid>(type: "uuid", nullable: false),
                    feedscheduleid = table.Column<Guid>(type: "uuid", nullable: false),
                    flockid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("nutrition_pkey", x => x.nutritionid);
                    table.ForeignKey(
                        name: "nutrition_feedscheduleid_fkey",
                        column: x => x.feedscheduleid,
                        principalTable: "feedschedule",
                        principalColumn: "feedscheduleid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "nutrition_flockid_fkey",
                        column: x => x.flockid,
                        principalTable: "flock",
                        principalColumn: "flockid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "nutrition_foodid_fkey",
                        column: x => x.foodid,
                        principalTable: "food",
                        principalColumn: "foodid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "nutrition_waterid_fkey",
                        column: x => x.waterid,
                        principalTable: "water",
                        principalColumn: "waterid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HealthlogUser",
                columns: table => new
                {
                    HealthLogsHlogid = table.Column<Guid>(type: "uuid", nullable: false),
                    UsersUserid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthlogUser", x => new { x.HealthLogsHlogid, x.UsersUserid });
                    table.ForeignKey(
                        name: "FK_HealthlogUser_healthlog_HealthLogsHlogid",
                        column: x => x.HealthLogsHlogid,
                        principalTable: "healthlog",
                        principalColumn: "hlogid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HealthlogUser_users_UsersUserid",
                        column: x => x.UsersUserid,
                        principalTable: "users",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "userhealthlog",
                columns: table => new
                {
                    hlogid = table.Column<Guid>(type: "uuid", nullable: false),
                    userid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "userhealthlog_hlogid_fkey",
                        column: x => x.hlogid,
                        principalTable: "healthlog",
                        principalColumn: "hlogid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "userhealthlog_userid_fkey",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuantitylogUser",
                columns: table => new
                {
                    QuantityLogsQlogid = table.Column<Guid>(type: "uuid", nullable: false),
                    UsersUserid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuantitylogUser", x => new { x.QuantityLogsQlogid, x.UsersUserid });
                    table.ForeignKey(
                        name: "FK_QuantitylogUser_quantitylog_QuantityLogsQlogid",
                        column: x => x.QuantityLogsQlogid,
                        principalTable: "quantitylog",
                        principalColumn: "qlogid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuantitylogUser_users_UsersUserid",
                        column: x => x.UsersUserid,
                        principalTable: "users",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "userquantitylog",
                columns: table => new
                {
                    qlogid = table.Column<Guid>(type: "uuid", nullable: false),
                    userid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "userquantitylog_qlogid_fkey",
                        column: x => x.qlogid,
                        principalTable: "quantitylog",
                        principalColumn: "qlogid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "userquantitylog_userid_fkey",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "breadingarea",
                columns: table => new
                {
                    breadingareaid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    breadingareacode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    breadingareaname = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    mealsperday = table.Column<int>(type: "integer", nullable: true),
                    humidity = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: true),
                    temperature = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: true),
                    weight = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true),
                    image = table.Column<string>(type: "text", nullable: true),
                    notes = table.Column<string>(type: "text", nullable: true),
                    area = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true),
                    covered = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false),
                    farmid = table.Column<Guid>(type: "uuid", nullable: false),
                    breadingpurpose = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("breadingarea_pkey", x => x.breadingareaid);
                    table.ForeignKey(
                        name: "breadingarea_farmid_fkey",
                        column: x => x.farmid,
                        principalTable: "farm",
                        principalColumn: "farmid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "assignment",
                columns: table => new
                {
                    assignmentid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    taskid = table.Column<Guid>(type: "uuid", nullable: false),
                    userid = table.Column<Guid>(type: "uuid", nullable: false),
                    assigneddate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    completeddate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    note = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("assignment_pkey", x => x.assignmentid);
                    table.ForeignKey(
                        name: "assignment_taskid_fkey",
                        column: x => x.taskid,
                        principalTable: "task",
                        principalColumn: "taskid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "assignment_userid_fkey",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "dailytask",
                columns: table => new
                {
                    dtaskid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    taskid = table.Column<Guid>(type: "uuid", nullable: false),
                    taskdate = table.Column<DateOnly>(type: "date", nullable: false),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    note = table.Column<string>(type: "text", nullable: true),
                    priority = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("dailytask_pkey", x => x.dtaskid);
                    table.ForeignKey(
                        name: "dailytask_taskid_fkey",
                        column: x => x.taskid,
                        principalTable: "task",
                        principalColumn: "taskid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "harvesttask",
                columns: table => new
                {
                    htaskid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    taskid = table.Column<Guid>(type: "uuid", nullable: false),
                    harvesttype = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    totalquantity = table.Column<int>(type: "integer", nullable: false),
                    damagedquantity = table.Column<int>(type: "integer", nullable: false),
                    goodquantity = table.Column<int>(type: "integer", nullable: false),
                    harvestdate = table.Column<DateOnly>(type: "date", nullable: false),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    note = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("harvesttask_pkey", x => x.htaskid);
                    table.ForeignKey(
                        name: "harvesttask_taskid_fkey",
                        column: x => x.taskid,
                        principalTable: "task",
                        principalColumn: "taskid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "workschedule",
                columns: table => new
                {
                    workscheduleid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    userid = table.Column<Guid>(type: "uuid", nullable: false),
                    taskid = table.Column<Guid>(type: "uuid", nullable: false),
                    workdate = table.Column<DateOnly>(type: "date", nullable: false),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    colorcode = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("workschedule_pkey", x => x.workscheduleid);
                    table.ForeignKey(
                        name: "workschedule_taskid_fkey",
                        column: x => x.taskid,
                        principalTable: "task",
                        principalColumn: "taskid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "workschedule_userid_fkey",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "uservaccinationlog",
                columns: table => new
                {
                    vlogid = table.Column<Guid>(type: "uuid", nullable: false),
                    userid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "uservaccinationlog_userid_fkey",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "uservaccinationlog_vlogid_fkey",
                        column: x => x.vlogid,
                        principalTable: "vaccinationlog",
                        principalColumn: "vlogid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserVaccinationlog",
                columns: table => new
                {
                    UsersUserid = table.Column<Guid>(type: "uuid", nullable: false),
                    VaccinationLogsVlogid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserVaccinationlog", x => new { x.UsersUserid, x.VaccinationLogsVlogid });
                    table.ForeignKey(
                        name: "FK_UserVaccinationlog_users_UsersUserid",
                        column: x => x.UsersUserid,
                        principalTable: "users",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserVaccinationlog_vaccinationlog_VaccinationLogsVlogid",
                        column: x => x.VaccinationLogsVlogid,
                        principalTable: "vaccinationlog",
                        principalColumn: "vlogid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "breadingequipment",
                columns: table => new
                {
                    breadingequipmentid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    breadingareaid = table.Column<Guid>(type: "uuid", nullable: false),
                    equipmentid = table.Column<Guid>(type: "uuid", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: true),
                    assigneddate = table.Column<DateOnly>(type: "date", nullable: false),
                    maintaindate = table.Column<DateOnly>(type: "date", nullable: true),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    note = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("breadingequipment_pkey", x => x.breadingequipmentid);
                    table.ForeignKey(
                        name: "breadingequipment_breadingareaid_fkey",
                        column: x => x.breadingareaid,
                        principalTable: "breadingarea",
                        principalColumn: "breadingareaid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "breadingequipment_equipmentid_fkey",
                        column: x => x.equipmentid,
                        principalTable: "equipment",
                        principalColumn: "equipmentid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "chickenbatch",
                columns: table => new
                {
                    chickenbatchid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    numberofchicken = table.Column<int>(type: "integer", nullable: false),
                    startdate = table.Column<DateOnly>(type: "date", nullable: false),
                    enddate = table.Column<DateOnly>(type: "date", nullable: true),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    note = table.Column<string>(type: "text", nullable: true),
                    breadingareaid = table.Column<Guid>(type: "uuid", nullable: false),
                    flockid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("chickenbatch_pkey", x => x.chickenbatchid);
                    table.ForeignKey(
                        name: "chickenbatch_breadingareaid_fkey",
                        column: x => x.breadingareaid,
                        principalTable: "breadingarea",
                        principalColumn: "breadingareaid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "chickenbatch_flockid_fkey",
                        column: x => x.flockid,
                        principalTable: "flock",
                        principalColumn: "flockid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "exportedproduct",
                columns: table => new
                {
                    eproductid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    productcode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    reason = table.Column<string>(type: "text", nullable: true),
                    exporteddate = table.Column<DateOnly>(type: "date", nullable: false),
                    expireddate = table.Column<DateOnly>(type: "date", nullable: true),
                    storagelocation = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    chickenbatchid = table.Column<Guid>(type: "uuid", nullable: false),
                    farmid = table.Column<Guid>(type: "uuid", nullable: false),
                    productid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("exportedproduct_pkey", x => x.eproductid);
                    table.ForeignKey(
                        name: "exportedproduct_chickenbatchid_fkey",
                        column: x => x.chickenbatchid,
                        principalTable: "chickenbatch",
                        principalColumn: "chickenbatchid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "exportedproduct_farmid_fkey",
                        column: x => x.farmid,
                        principalTable: "farm",
                        principalColumn: "farmid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "exportedproduct_productid_fkey",
                        column: x => x.productid,
                        principalTable: "product",
                        principalColumn: "productid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_assignment_taskid",
                table: "assignment",
                column: "taskid");

            migrationBuilder.CreateIndex(
                name: "IX_assignment_userid",
                table: "assignment",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "breadingarea_breadingareacode_key",
                table: "breadingarea",
                column: "breadingareacode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_breadingarea_farmid",
                table: "breadingarea",
                column: "farmid");

            migrationBuilder.CreateIndex(
                name: "IX_breadingequipment_breadingareaid",
                table: "breadingequipment",
                column: "breadingareaid");

            migrationBuilder.CreateIndex(
                name: "IX_breadingequipment_equipmentid",
                table: "breadingequipment",
                column: "equipmentid");

            migrationBuilder.CreateIndex(
                name: "IX_chickenbatch_breadingareaid",
                table: "chickenbatch",
                column: "breadingareaid");

            migrationBuilder.CreateIndex(
                name: "IX_chickenbatch_flockid",
                table: "chickenbatch",
                column: "flockid");

            migrationBuilder.CreateIndex(
                name: "IX_dailytask_taskid",
                table: "dailytask",
                column: "taskid");

            migrationBuilder.CreateIndex(
                name: "IX_expireddamaged_productid",
                table: "expireddamaged",
                column: "productid");

            migrationBuilder.CreateIndex(
                name: "exportedproduct_productcode_key",
                table: "exportedproduct",
                column: "productcode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_exportedproduct_chickenbatchid",
                table: "exportedproduct",
                column: "chickenbatchid");

            migrationBuilder.CreateIndex(
                name: "IX_exportedproduct_farmid",
                table: "exportedproduct",
                column: "farmid");

            migrationBuilder.CreateIndex(
                name: "IX_exportedproduct_productid",
                table: "exportedproduct",
                column: "productid");

            migrationBuilder.CreateIndex(
                name: "farm_farmcode_key",
                table: "farm",
                column: "farmcode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_farm_userid",
                table: "farm",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_flock_breedid",
                table: "flock",
                column: "breedid");

            migrationBuilder.CreateIndex(
                name: "IX_flock_purposeid",
                table: "flock",
                column: "purposeid");

            migrationBuilder.CreateIndex(
                name: "IX_food_supplierid",
                table: "food",
                column: "supplierid");

            migrationBuilder.CreateIndex(
                name: "IX_harvesttask_taskid",
                table: "harvesttask",
                column: "taskid");

            migrationBuilder.CreateIndex(
                name: "IX_healthlog_flockid",
                table: "healthlog",
                column: "flockid");

            migrationBuilder.CreateIndex(
                name: "IX_HealthlogUser_UsersUserid",
                table: "HealthlogUser",
                column: "UsersUserid");

            migrationBuilder.CreateIndex(
                name: "IX_inventoryaudit_productid",
                table: "inventoryaudit",
                column: "productid");

            migrationBuilder.CreateIndex(
                name: "IX_inventoryaudit_userid",
                table: "inventoryaudit",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_nutrition_feedscheduleid",
                table: "nutrition",
                column: "feedscheduleid");

            migrationBuilder.CreateIndex(
                name: "IX_nutrition_flockid",
                table: "nutrition",
                column: "flockid");

            migrationBuilder.CreateIndex(
                name: "IX_nutrition_foodid",
                table: "nutrition",
                column: "foodid");

            migrationBuilder.CreateIndex(
                name: "IX_nutrition_waterid",
                table: "nutrition",
                column: "waterid");

            migrationBuilder.CreateIndex(
                name: "IX_performancestatistic_userid",
                table: "performancestatistic",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_product_supplierid",
                table: "product",
                column: "supplierid");

            migrationBuilder.CreateIndex(
                name: "product_productcode_key",
                table: "product",
                column: "productcode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_quantitylog_flockid",
                table: "quantitylog",
                column: "flockid");

            migrationBuilder.CreateIndex(
                name: "IX_quantitylog_reasonid",
                table: "quantitylog",
                column: "reasonid");

            migrationBuilder.CreateIndex(
                name: "IX_QuantitylogUser_UsersUserid",
                table: "QuantitylogUser",
                column: "UsersUserid");

            migrationBuilder.CreateIndex(
                name: "roles_rolename_key",
                table: "roles",
                column: "rolename",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_salary_userid",
                table: "salary",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_task_userid",
                table: "task",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_time_keeping_timekeepingtype",
                table: "time_keeping",
                column: "timekeepingtype");

            migrationBuilder.CreateIndex(
                name: "IX_time_keeping_userid",
                table: "time_keeping",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "time_keeping_types_typename_key",
                table: "time_keeping_types",
                column: "typename",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_userhealthlog_hlogid",
                table: "userhealthlog",
                column: "hlogid");

            migrationBuilder.CreateIndex(
                name: "IX_userhealthlog_userid",
                table: "userhealthlog",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_userquantitylog_qlogid",
                table: "userquantitylog",
                column: "qlogid");

            migrationBuilder.CreateIndex(
                name: "IX_userquantitylog_userid",
                table: "userquantitylog",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_users_roleid",
                table: "users",
                column: "roleid");

            migrationBuilder.CreateIndex(
                name: "users_cccd_key",
                table: "users",
                column: "cccd",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_uservaccinationlog_userid",
                table: "uservaccinationlog",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_uservaccinationlog_vlogid",
                table: "uservaccinationlog",
                column: "vlogid");

            migrationBuilder.CreateIndex(
                name: "IX_UserVaccinationlog_VaccinationLogsVlogid",
                table: "UserVaccinationlog",
                column: "VaccinationLogsVlogid");

            migrationBuilder.CreateIndex(
                name: "IX_vaccinationlog_flockid",
                table: "vaccinationlog",
                column: "flockid");

            migrationBuilder.CreateIndex(
                name: "IX_vaccinationlog_vaccineid",
                table: "vaccinationlog",
                column: "vaccineid");

            migrationBuilder.CreateIndex(
                name: "IX_vaccine_diseaseid",
                table: "vaccine",
                column: "diseaseid");

            migrationBuilder.CreateIndex(
                name: "IX_vaccine_supplierid",
                table: "vaccine",
                column: "supplierid");

            migrationBuilder.CreateIndex(
                name: "IX_water_supplierid",
                table: "water",
                column: "supplierid");

            migrationBuilder.CreateIndex(
                name: "IX_workschedule_taskid",
                table: "workschedule",
                column: "taskid");

            migrationBuilder.CreateIndex(
                name: "IX_workschedule_userid",
                table: "workschedule",
                column: "userid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "assignment");

            migrationBuilder.DropTable(
                name: "breadingequipment");

            migrationBuilder.DropTable(
                name: "dailytask");

            migrationBuilder.DropTable(
                name: "expireddamaged");

            migrationBuilder.DropTable(
                name: "exportedproduct");

            migrationBuilder.DropTable(
                name: "harvesttask");

            migrationBuilder.DropTable(
                name: "healthcriteria");

            migrationBuilder.DropTable(
                name: "HealthlogUser");

            migrationBuilder.DropTable(
                name: "inventoryaudit");

            migrationBuilder.DropTable(
                name: "nutrition");

            migrationBuilder.DropTable(
                name: "performancestatistic");

            migrationBuilder.DropTable(
                name: "QuantitylogUser");

            migrationBuilder.DropTable(
                name: "salary");

            migrationBuilder.DropTable(
                name: "time_keeping");

            migrationBuilder.DropTable(
                name: "userhealthlog");

            migrationBuilder.DropTable(
                name: "userquantitylog");

            migrationBuilder.DropTable(
                name: "uservaccinationlog");

            migrationBuilder.DropTable(
                name: "UserVaccinationlog");

            migrationBuilder.DropTable(
                name: "workschedule");

            migrationBuilder.DropTable(
                name: "equipment");

            migrationBuilder.DropTable(
                name: "chickenbatch");

            migrationBuilder.DropTable(
                name: "product");

            migrationBuilder.DropTable(
                name: "feedschedule");

            migrationBuilder.DropTable(
                name: "food");

            migrationBuilder.DropTable(
                name: "water");

            migrationBuilder.DropTable(
                name: "time_keeping_types");

            migrationBuilder.DropTable(
                name: "healthlog");

            migrationBuilder.DropTable(
                name: "quantitylog");

            migrationBuilder.DropTable(
                name: "vaccinationlog");

            migrationBuilder.DropTable(
                name: "task");

            migrationBuilder.DropTable(
                name: "breadingarea");

            migrationBuilder.DropTable(
                name: "reason");

            migrationBuilder.DropTable(
                name: "flock");

            migrationBuilder.DropTable(
                name: "vaccine");

            migrationBuilder.DropTable(
                name: "farm");

            migrationBuilder.DropTable(
                name: "breed");

            migrationBuilder.DropTable(
                name: "purpose");

            migrationBuilder.DropTable(
                name: "disease");

            migrationBuilder.DropTable(
                name: "supplier");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "roles");
        }
    }
}
