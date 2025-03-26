using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CFMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLatestDbV5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Warehouse_StorageTypeId_fkey",
                table: "Warehouse");

            migrationBuilder.RenameColumn(
                name: "StorageTypeId",
                table: "Warehouse",
                newName: "ResourceTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Warehouse_StorageTypeId",
                table: "Warehouse",
                newName: "IX_Warehouse_ResourceTypeId");

            migrationBuilder.AddForeignKey(
                name: "Warehouse_ResourceTypeId_fkey",
                table: "Warehouse",
                column: "ResourceTypeId",
                principalTable: "SubCategory",
                principalColumn: "SubCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Warehouse_ResourceTypeId_fkey",
                table: "Warehouse");

            migrationBuilder.RenameColumn(
                name: "ResourceTypeId",
                table: "Warehouse",
                newName: "StorageTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Warehouse_ResourceTypeId",
                table: "Warehouse",
                newName: "IX_Warehouse_StorageTypeId");

            migrationBuilder.AddForeignKey(
                name: "Warehouse_StorageTypeId_fkey",
                table: "Warehouse",
                column: "StorageTypeId",
                principalTable: "SubCategory",
                principalColumn: "SubCategoryId");
        }
    }
}
