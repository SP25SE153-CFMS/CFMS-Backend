using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CFMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseV40 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ToWareId",
                table: "StockReceiptDetail",
                newName: "ToWarehouseId");

            migrationBuilder.AddColumn<Guid>(
                name: "WarehouseWareId",
                table: "StockReceiptDetail",
                type: "uuid",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ToWarehouseId",
                table: "StockReceiptDetail",
                newName: "ToWareId");
        }
    }
}
