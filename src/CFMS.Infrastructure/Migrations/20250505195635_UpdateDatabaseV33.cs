using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CFMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseV33 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HealthLogDetail_Resource_ResourceId",
                table: "HealthLogDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_VaccineLog_Resource_ResourceId",
                table: "VaccineLog");

            migrationBuilder.DropIndex(
                name: "IX_VaccineLog_ResourceId",
                table: "VaccineLog");

            migrationBuilder.DropIndex(
                name: "IX_HealthLogDetail_ResourceId",
                table: "HealthLogDetail");

            migrationBuilder.DropColumn(
                name: "ActualVaccineAmount",
                table: "VaccineLog");

            migrationBuilder.DropColumn(
                name: "ResourceId",
                table: "VaccineLog");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "VaccineLog");

            migrationBuilder.DropColumn(
                name: "VaccineDate",
                table: "VaccineLog");

            migrationBuilder.DropColumn(
                name: "ActualItemAmount",
                table: "HealthLogDetail");

            migrationBuilder.DropColumn(
                name: "ResourceId",
                table: "HealthLogDetail");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "HealthLogDetail");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ActualVaccineAmount",
                table: "VaccineLog",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ResourceId",
                table: "VaccineLog",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UnitId",
                table: "VaccineLog",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "VaccineDate",
                table: "VaccineLog",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ActualItemAmount",
                table: "HealthLogDetail",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ResourceId",
                table: "HealthLogDetail",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UnitId",
                table: "HealthLogDetail",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VaccineLog_ResourceId",
                table: "VaccineLog",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthLogDetail_ResourceId",
                table: "HealthLogDetail",
                column: "ResourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_HealthLogDetail_Resource_ResourceId",
                table: "HealthLogDetail",
                column: "ResourceId",
                principalTable: "Resource",
                principalColumn: "ResourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_VaccineLog_Resource_ResourceId",
                table: "VaccineLog",
                column: "ResourceId",
                principalTable: "Resource",
                principalColumn: "ResourceId");
        }
    }
}
