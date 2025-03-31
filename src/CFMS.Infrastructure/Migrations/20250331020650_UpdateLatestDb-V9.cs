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
                name: "ResourceSupplier_ResourceId_fkey",
                table: "ResourceSupplier");

            migrationBuilder.DropForeignKey(
                name: "ResourceSupplier_SupplierId_fkey",
                table: "ResourceSupplier");

            migrationBuilder.AddForeignKey(
                name: "ResourceSupplier_ResourceId_fkey",
                table: "ResourceSupplier",
                column: "ResourceId",
                principalTable: "Resource",
                principalColumn: "ResourceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "ResourceSupplier_SupplierId_fkey",
                table: "ResourceSupplier",
                column: "SupplierId",
                principalTable: "Supplier",
                principalColumn: "SupplierId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "ResourceSupplier_ResourceId_fkey",
                table: "ResourceSupplier");

            migrationBuilder.DropForeignKey(
                name: "ResourceSupplier_SupplierId_fkey",
                table: "ResourceSupplier");

            migrationBuilder.AddForeignKey(
                name: "ResourceSupplier_ResourceId_fkey",
                table: "ResourceSupplier",
                column: "ResourceId",
                principalTable: "Resource",
                principalColumn: "ResourceId");

            migrationBuilder.AddForeignKey(
                name: "ResourceSupplier_SupplierId_fkey",
                table: "ResourceSupplier",
                column: "SupplierId",
                principalTable: "Supplier",
                principalColumn: "SupplierId",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
