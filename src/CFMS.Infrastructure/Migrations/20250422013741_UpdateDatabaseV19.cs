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
            migrationBuilder.CreateIndex(
                name: "IX_InventoryReceiptDetail_ResourceId",
                table: "InventoryReceiptDetail",
                column: "ResourceId");

            migrationBuilder.AddForeignKey(
                name: "InventoryReceiptDetail_ResourceId_fkey",
                table: "InventoryReceiptDetail",
                column: "ResourceId",
                principalTable: "Resource",
                principalColumn: "ResourceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "InventoryReceiptDetail_ResourceId_fkey",
                table: "InventoryReceiptDetail");

            migrationBuilder.DropIndex(
                name: "IX_InventoryReceiptDetail_ResourceId",
                table: "InventoryReceiptDetail");
        }
    }
}
