using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CFMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseV41 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ChickenCoopId",
                table: "TaskHarvest",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskHarvest_ChickenCoopId",
                table: "TaskHarvest",
                column: "ChickenCoopId");

            migrationBuilder.AddForeignKey(
                name: "TaskHarvest_ChickenCoopId_fkey",
                table: "TaskHarvest",
                column: "ChickenCoopId",
                principalTable: "ChickenCoop",
                principalColumn: "ChickenCoopId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "TaskHarvest_ChickenCoopId_fkey",
                table: "TaskHarvest");

            migrationBuilder.DropIndex(
                name: "IX_TaskHarvest_ChickenCoopId",
                table: "TaskHarvest");

            migrationBuilder.DropColumn(
                name: "ChickenCoopId",
                table: "TaskHarvest");
        }
    }
}
