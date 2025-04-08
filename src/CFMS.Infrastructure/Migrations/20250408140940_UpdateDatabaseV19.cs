using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CFMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseV19 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AreaUnitId",
                table: "BreedingArea",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BreedingArea_AreaUnitId",
                table: "BreedingArea",
                column: "AreaUnitId");

            migrationBuilder.AddForeignKey(
                name: "BreedingArea_AreaUnitId_fkey",
                table: "BreedingArea",
                column: "AreaUnitId",
                principalTable: "SubCategory",
                principalColumn: "SubCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "BreedingArea_AreaUnitId_fkey",
                table: "BreedingArea");

            migrationBuilder.DropIndex(
                name: "IX_BreedingArea_AreaUnitId",
                table: "BreedingArea");

            migrationBuilder.DropColumn(
                name: "AreaUnitId",
                table: "BreedingArea");
        }
    }
}
