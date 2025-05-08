using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CFMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseV42 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "TaskHarvest_ChickenCoopId_fkey",
                table: "TaskHarvest");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskHarvest_ChickenCoop_ChickenCoopId",
                table: "TaskHarvest",
                column: "ChickenCoopId",
                principalTable: "ChickenCoop",
                principalColumn: "ChickenCoopId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskHarvest_ChickenCoop_ChickenCoopId",
                table: "TaskHarvest");

            migrationBuilder.AddForeignKey(
                name: "TaskHarvest_ChickenCoopId_fkey",
                table: "TaskHarvest",
                column: "ChickenCoopId",
                principalTable: "ChickenCoop",
                principalColumn: "ChickenCoopId");
        }
    }
}
