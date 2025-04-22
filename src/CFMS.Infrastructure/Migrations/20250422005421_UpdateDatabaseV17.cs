using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CFMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseV17 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BatchNumber",
                table: "InventoryReceiptDetail");

            migrationBuilder.AddColumn<int>(
                name: "BatchNumber",
                table: "InventoryRequest",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BatchNumber",
                table: "InventoryRequest");

            migrationBuilder.AddColumn<int>(
                name: "BatchNumber",
                table: "InventoryReceiptDetail",
                type: "integer",
                nullable: true);
        }
    }
}
