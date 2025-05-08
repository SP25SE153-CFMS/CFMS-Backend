using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CFMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseV11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IsHidden",
                table: "SubCategory",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHidden",
                table: "SubCategory");
        }
    }
}
