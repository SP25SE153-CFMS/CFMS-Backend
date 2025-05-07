using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CFMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseV36 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FeedLog_UnitId_fkey",
                table: "FeedLog");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "VaccineLog");

            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "VaccineLog",
                newName: "Note");

            migrationBuilder.RenameColumn(
                name: "UnitId",
                table: "FeedLog",
                newName: "SubCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_FeedLog_UnitId",
                table: "FeedLog",
                newName: "IX_FeedLog_SubCategoryId");

            migrationBuilder.AddColumn<decimal>(
                name: "ActualVaccineAmount",
                table: "VaccineLog",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ResourceId",
                table: "VaccineLog",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "VccineDate",
                table: "VaccineLog",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VaccineLog_ResourceId",
                table: "VaccineLog",
                column: "ResourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_FeedLog_SubCategory_SubCategoryId",
                table: "FeedLog",
                column: "SubCategoryId",
                principalTable: "SubCategory",
                principalColumn: "SubCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_VaccineLog_Resource_ResourceId",
                table: "VaccineLog",
                column: "ResourceId",
                principalTable: "Resource",
                principalColumn: "ResourceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeedLog_SubCategory_SubCategoryId",
                table: "FeedLog");

            migrationBuilder.DropForeignKey(
                name: "FK_VaccineLog_Resource_ResourceId",
                table: "VaccineLog");

            migrationBuilder.DropIndex(
                name: "IX_VaccineLog_ResourceId",
                table: "VaccineLog");

            migrationBuilder.DropColumn(
                name: "ActualVaccineAmount",
                table: "VaccineLog");

            migrationBuilder.DropColumn(
                name: "ResourceId",
                table: "VaccineLog");

            migrationBuilder.DropColumn(
                name: "VccineDate",
                table: "VaccineLog");

            migrationBuilder.RenameColumn(
                name: "Note",
                table: "VaccineLog",
                newName: "Notes");

            migrationBuilder.RenameColumn(
                name: "SubCategoryId",
                table: "FeedLog",
                newName: "UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_FeedLog_SubCategoryId",
                table: "FeedLog",
                newName: "IX_FeedLog_UnitId");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "VaccineLog",
                type: "integer",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FeedLog_UnitId_fkey",
                table: "FeedLog",
                column: "UnitId",
                principalTable: "SubCategory",
                principalColumn: "SubCategoryId");
        }
    }
}
