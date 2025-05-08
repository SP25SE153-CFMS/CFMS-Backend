using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CFMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseV31 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                table: "HealthLog",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ResourceId",
                table: "HealthLog",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UnitId",
                table: "HealthLog",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VaccineLog_ResourceId",
                table: "VaccineLog",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthLog_ResourceId",
                table: "HealthLog",
                column: "ResourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_HealthLog_Resource_ResourceId",
                table: "HealthLog",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HealthLog_Resource_ResourceId",
                table: "HealthLog");

            migrationBuilder.DropForeignKey(
                name: "FK_VaccineLog_Resource_ResourceId",
                table: "VaccineLog");

            migrationBuilder.DropIndex(
                name: "IX_VaccineLog_ResourceId",
                table: "VaccineLog");

            migrationBuilder.DropIndex(
                name: "IX_HealthLog_ResourceId",
                table: "HealthLog");

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
                table: "HealthLog");

            migrationBuilder.DropColumn(
                name: "ResourceId",
                table: "HealthLog");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "HealthLog");
        }
    }
}
