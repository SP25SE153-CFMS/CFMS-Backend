using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CFMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseV10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FarmId",
                table: "Task",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FarmId",
                table: "NutritionPlan",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FarmId",
                table: "GrowthStage",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AreaUnitId",
                table: "Farm",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AreaUnitId",
                table: "ChickenCoop",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FarmId",
                table: "Category",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Farm_AreaUnitId",
                table: "Farm",
                column: "AreaUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ChickenCoop_AreaUnitId",
                table: "ChickenCoop",
                column: "AreaUnitId");

            migrationBuilder.AddForeignKey(
                name: "ChickenCoop_AreaUnitId_fkey",
                table: "ChickenCoop",
                column: "AreaUnitId",
                principalTable: "SubCategory",
                principalColumn: "SubCategoryId");

            migrationBuilder.AddForeignKey(
                name: "Farm_AreaUnitId_fkey",
                table: "Farm",
                column: "AreaUnitId",
                principalTable: "SubCategory",
                principalColumn: "SubCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "ChickenCoop_AreaUnitId_fkey",
                table: "ChickenCoop");

            migrationBuilder.DropForeignKey(
                name: "Farm_AreaUnitId_fkey",
                table: "Farm");

            migrationBuilder.DropIndex(
                name: "IX_Farm_AreaUnitId",
                table: "Farm");

            migrationBuilder.DropIndex(
                name: "IX_ChickenCoop_AreaUnitId",
                table: "ChickenCoop");

            migrationBuilder.DropColumn(
                name: "FarmId",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "FarmId",
                table: "NutritionPlan");

            migrationBuilder.DropColumn(
                name: "FarmId",
                table: "GrowthStage");

            migrationBuilder.DropColumn(
                name: "AreaUnitId",
                table: "Farm");

            migrationBuilder.DropColumn(
                name: "AreaUnitId",
                table: "ChickenCoop");

            migrationBuilder.DropColumn(
                name: "FarmId",
                table: "Category");
        }
    }
}
