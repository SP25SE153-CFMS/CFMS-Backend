using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CFMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLatestDbV9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "ResourceSupplier_PackagePriceId_fkey",
                table: "ResourceSupplier");

            migrationBuilder.DropForeignKey(
                name: "ResourceSupplier_UnitPriceId_fkey",
                table: "ResourceSupplier");

            migrationBuilder.DropIndex(
                name: "IX_ResourceSupplier_PackagePriceId",
                table: "ResourceSupplier");

            migrationBuilder.DropIndex(
                name: "IX_ResourceSupplier_UnitPriceId",
                table: "ResourceSupplier");

            migrationBuilder.DropColumn(
                name: "PackagePriceId",
                table: "ResourceSupplier");

            migrationBuilder.DropColumn(
                name: "PackageSizePrice",
                table: "ResourceSupplier");

            migrationBuilder.DropColumn(
                name: "UnitPriceId",
                table: "ResourceSupplier");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PackagePriceId",
                table: "ResourceSupplier",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PackageSizePrice",
                table: "ResourceSupplier",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UnitPriceId",
                table: "ResourceSupplier",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResourceSupplier_PackagePriceId",
                table: "ResourceSupplier",
                column: "PackagePriceId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceSupplier_UnitPriceId",
                table: "ResourceSupplier",
                column: "UnitPriceId");

            migrationBuilder.AddForeignKey(
                name: "ResourceSupplier_PackagePriceId_fkey",
                table: "ResourceSupplier",
                column: "PackagePriceId",
                principalTable: "SubCategory",
                principalColumn: "SubCategoryId");

            migrationBuilder.AddForeignKey(
                name: "ResourceSupplier_UnitPriceId_fkey",
                table: "ResourceSupplier",
                column: "UnitPriceId",
                principalTable: "SubCategory",
                principalColumn: "SubCategoryId");
        }
    }
}
