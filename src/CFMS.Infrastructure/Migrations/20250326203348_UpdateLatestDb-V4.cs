using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CFMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLatestDbV4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Material",
                table: "Equipment");

            migrationBuilder.AddColumn<Guid>(
                name: "MaterialId",
                table: "Equipment",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_MaterialId",
                table: "Equipment",
                column: "MaterialId");

            migrationBuilder.AddForeignKey(
                name: "Equipment_MaterialId_fkey",
                table: "Equipment",
                column: "MaterialId",
                principalTable: "SubCategory",
                principalColumn: "SubCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Equipment_MaterialId_fkey",
                table: "Equipment");

            migrationBuilder.DropIndex(
                name: "IX_Equipment_MaterialId",
                table: "Equipment");

            migrationBuilder.DropColumn(
                name: "MaterialId",
                table: "Equipment");

            migrationBuilder.AddColumn<string>(
                name: "Material",
                table: "Equipment",
                type: "character varying",
                nullable: true);
        }
    }
}
