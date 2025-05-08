using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CFMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseV14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quality",
                table: "TaskHarvest");

            migrationBuilder.RenameColumn(
                name: "UnitId",
                table: "TaskHarvest",
                newName: "HarvestProductId");

            migrationBuilder.AlterColumn<decimal>(
                name: "Quantity",
                table: "TaskHarvest",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HarvestProductId",
                table: "TaskHarvest",
                newName: "UnitId");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "TaskHarvest",
                type: "integer",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Quality",
                table: "TaskHarvest",
                type: "character varying",
                nullable: true);
        }
    }
}
