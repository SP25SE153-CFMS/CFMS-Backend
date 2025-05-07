using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CFMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseV43 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskHarvest_ChickenCoop_ChickenCoopId",
                table: "TaskHarvest");

            migrationBuilder.DropIndex(
                name: "IX_TaskHarvest_ChickenCoopId",
                table: "TaskHarvest");

            migrationBuilder.DropColumn(
                name: "ChickenCoopId",
                table: "TaskHarvest");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "FK_TaskHarvest_ChickenCoop_ChickenCoopId",
                table: "TaskHarvest",
                column: "ChickenCoopId",
                principalTable: "ChickenCoop",
                principalColumn: "ChickenCoopId");
        }
    }
}
