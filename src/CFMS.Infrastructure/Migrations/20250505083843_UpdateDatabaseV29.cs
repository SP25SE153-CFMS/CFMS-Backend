using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CFMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseV29 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Task_FarmId",
                table: "Task",
                column: "FarmId");

            migrationBuilder.AddForeignKey(
                name: "Task_FarmId_fkey",
                table: "Task",
                column: "FarmId",
                principalTable: "Farm",
                principalColumn: "FarmId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Task_FarmId_fkey",
                table: "Task");

            migrationBuilder.DropIndex(
                name: "IX_Task_FarmId",
                table: "Task");
        }
    }
}
