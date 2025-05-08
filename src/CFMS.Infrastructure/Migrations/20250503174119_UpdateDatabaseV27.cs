using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CFMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseV27 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "TaskRequest",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                table: "TaskRequest");
        }
    }
}
