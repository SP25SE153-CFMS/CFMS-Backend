using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CFMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseV12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TaskResource_ResourceId",
                table: "TaskResource",
                column: "ResourceId");

            migrationBuilder.AddForeignKey(
                name: "TaskResource_ResourceId_fkey",
                table: "TaskResource",
                column: "ResourceId",
                principalTable: "Resource",
                principalColumn: "ResourceId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "TaskResource_ResourceId_fkey",
                table: "TaskResource");

            migrationBuilder.DropIndex(
                name: "IX_TaskResource_ResourceId",
                table: "TaskResource");
        }
    }
}
