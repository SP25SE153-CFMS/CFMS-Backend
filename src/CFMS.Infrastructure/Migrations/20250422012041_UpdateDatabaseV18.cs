using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CFMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseV18 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BatchNumber",
                table: "InventoryRequest");

            migrationBuilder.AddColumn<int>(
                name: "BatchNumber",
                table: "InventoryReceipt",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BatchNumber",
                table: "InventoryReceipt");

            migrationBuilder.AddColumn<int>(
                name: "BatchNumber",
                table: "InventoryRequest",
                type: "integer",
                nullable: true);
        }
    }
}
