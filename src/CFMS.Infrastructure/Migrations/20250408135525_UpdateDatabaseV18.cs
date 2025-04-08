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
                name: "Weight",
                table: "ChickenDetail");

            migrationBuilder.DropColumn(
                name: "TotalQuantity",
                table: "Chicken");

            migrationBuilder.RenameColumn(
                name: "ChickenBathId",
                table: "ChickenDetail",
                newName: "ChickenBatchId");

            migrationBuilder.RenameIndex(
                name: "IX_ChickenDetail_ChickenBathId",
                table: "ChickenDetail",
                newName: "IX_ChickenDetail_ChickenBatchId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ChickenBatchId",
                table: "ChickenDetail",
                newName: "ChickenBathId");

            migrationBuilder.RenameIndex(
                name: "IX_ChickenDetail_ChickenBatchId",
                table: "ChickenDetail",
                newName: "IX_ChickenDetail_ChickenBathId");

            migrationBuilder.AddColumn<decimal>(
                name: "Weight",
                table: "ChickenDetail",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalQuantity",
                table: "Chicken",
                type: "integer",
                nullable: true);
        }
    }
}
