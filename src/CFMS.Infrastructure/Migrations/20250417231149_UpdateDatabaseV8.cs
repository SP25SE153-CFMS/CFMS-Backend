using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CFMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseV8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "TaskRequest_TaskTypeId_fkey",
                table: "TaskRequest");

            migrationBuilder.RenameColumn(
                name: "TaskTypeId",
                table: "TaskRequest",
                newName: "SubCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_TaskRequest_TaskTypeId",
                table: "TaskRequest",
                newName: "IX_TaskRequest_SubCategoryId");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "TaskRequest",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "TaskRequest",
                type: "text",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskRequest_SubCategory_SubCategoryId",
                table: "TaskRequest",
                column: "SubCategoryId",
                principalTable: "SubCategory",
                principalColumn: "SubCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskRequest_SubCategory_SubCategoryId",
                table: "TaskRequest");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "TaskRequest");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "TaskRequest");

            migrationBuilder.RenameColumn(
                name: "SubCategoryId",
                table: "TaskRequest",
                newName: "TaskTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_TaskRequest_SubCategoryId",
                table: "TaskRequest",
                newName: "IX_TaskRequest_TaskTypeId");

            migrationBuilder.AddForeignKey(
                name: "TaskRequest_TaskTypeId_fkey",
                table: "TaskRequest",
                column: "TaskTypeId",
                principalTable: "SubCategory",
                principalColumn: "SubCategoryId");
        }
    }
}
