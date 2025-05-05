using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CFMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseV32 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HealthLog_Resource_ResourceId",
                table: "HealthLog");

            migrationBuilder.DropIndex(
                name: "IX_HealthLog_ResourceId",
                table: "HealthLog");

            migrationBuilder.DropColumn(
                name: "ActualItemAmount",
                table: "HealthLog");

            migrationBuilder.DropColumn(
                name: "ResourceId",
                table: "HealthLog");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "HealthLog");

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
                name: "IX_HealthLogDetail_ResourceId",
                table: "HealthLogDetail",
                column: "ResourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_HealthLogDetail_Resource_ResourceId",
                table: "HealthLogDetail",
                column: "ResourceId",
                principalTable: "Resource",
                principalColumn: "ResourceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HealthLogDetail_Resource_ResourceId",
                table: "HealthLogDetail");

            migrationBuilder.DropIndex(
                name: "IX_HealthLogDetail_ResourceId",
                table: "HealthLogDetail");

            migrationBuilder.DropColumn(
                name: "ActualItemAmount",
                table: "HealthLogDetail");

            migrationBuilder.DropColumn(
                name: "ResourceId",
                table: "HealthLogDetail");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "HealthLogDetail");

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
                name: "IX_HealthLog_ResourceId",
                table: "HealthLog",
                column: "ResourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_HealthLog_Resource_ResourceId",
                table: "HealthLog",
                column: "ResourceId",
                principalTable: "Resource",
                principalColumn: "ResourceId");
        }
    }
}
