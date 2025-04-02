using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CFMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseV13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TaskResource_UnitId",
                table: "TaskResource",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "TaskResource_UnitId_fkey",
                table: "TaskResource",
                column: "UnitId",
                principalTable: "SubCategory",
                principalColumn: "SubCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "TaskResource_UnitId_fkey",
                table: "TaskResource");

            migrationBuilder.DropIndex(
                name: "IX_TaskResource_UnitId",
                table: "TaskResource");
        }
    }
}
