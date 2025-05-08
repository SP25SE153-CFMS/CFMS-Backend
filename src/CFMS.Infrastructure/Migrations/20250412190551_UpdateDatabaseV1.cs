using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CFMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseV1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FrequencySchedule");

            migrationBuilder.AddColumn<Guid>(
                name: "ChickenId",
                table: "Resource",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "HarvestProductId",
                table: "Resource",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderNum",
                table: "GrowthStage",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "HarvestProduct",
                columns: table => new
                {
                    HarvestProducttId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    HarvestProductName = table.Column<string>(type: "character varying", nullable: false),
                    HarvestProductTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("HarvestProduct_pkey", x => x.HarvestProducttId);
                    table.ForeignKey(
                        name: "FK_HarvestProduct_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HarvestProduct_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "HarvestProduct_HarvestProductTypeId_fkey",
                        column: x => x.HarvestProductTypeId,
                        principalTable: "SubCategory",
                        principalColumn: "SubCategoryId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Resource_ChickenId",
                table: "Resource",
                column: "ChickenId");

            migrationBuilder.CreateIndex(
                name: "IX_Resource_HarvestProductId",
                table: "Resource",
                column: "HarvestProductId");

            migrationBuilder.CreateIndex(
                name: "IX_HarvestProduct_CreatedByUserId",
                table: "HarvestProduct",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_HarvestProduct_HarvestProductTypeId",
                table: "HarvestProduct",
                column: "HarvestProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_HarvestProduct_LastEditedByUserId",
                table: "HarvestProduct",
                column: "LastEditedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Resource_Chicken_ChickenId",
                table: "Resource",
                column: "ChickenId",
                principalTable: "Chicken",
                principalColumn: "ChickenId");

            migrationBuilder.AddForeignKey(
                name: "FK_Resource_HarvestProduct_HarvestProductId",
                table: "Resource",
                column: "HarvestProductId",
                principalTable: "HarvestProduct",
                principalColumn: "HarvestProducttId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resource_Chicken_ChickenId",
                table: "Resource");

            migrationBuilder.DropForeignKey(
                name: "FK_Resource_HarvestProduct_HarvestProductId",
                table: "Resource");

            migrationBuilder.DropTable(
                name: "HarvestProduct");

            migrationBuilder.DropIndex(
                name: "IX_Resource_ChickenId",
                table: "Resource");

            migrationBuilder.DropIndex(
                name: "IX_Resource_HarvestProductId",
                table: "Resource");

            migrationBuilder.DropColumn(
                name: "ChickenId",
                table: "Resource");

            migrationBuilder.DropColumn(
                name: "HarvestProductId",
                table: "Resource");

            migrationBuilder.DropColumn(
                name: "OrderNum",
                table: "GrowthStage");

            migrationBuilder.CreateTable(
                name: "FrequencySchedule",
                columns: table => new
                {
                    FrequencyScheduleId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TimeUnitId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EndWorkDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Frequency = table.Column<int>(type: "integer", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    StartWorkDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    TaskId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("FrequencySchedule_pkey", x => x.FrequencyScheduleId);
                    table.ForeignKey(
                        name: "FK_FrequencySchedule_SubCategory_TimeUnitId",
                        column: x => x.TimeUnitId,
                        principalTable: "SubCategory",
                        principalColumn: "SubCategoryId");
                    table.ForeignKey(
                        name: "FK_FrequencySchedule_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FrequencySchedule_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FrequencySchedule_FrequencyScheduleId_fkey",
                        column: x => x.FrequencyScheduleId,
                        principalTable: "Task",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FrequencySchedule_CreatedByUserId",
                table: "FrequencySchedule",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FrequencySchedule_LastEditedByUserId",
                table: "FrequencySchedule",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FrequencySchedule_TimeUnitId",
                table: "FrequencySchedule",
                column: "TimeUnitId");
        }
    }
}
