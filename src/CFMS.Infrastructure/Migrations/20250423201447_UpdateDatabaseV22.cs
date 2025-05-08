using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CFMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseV22 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ResourceId",
                table: "FeedLog",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FeedLog_ResourceId",
                table: "FeedLog",
                column: "ResourceId");

            migrationBuilder.AddForeignKey(
                name: "FeedLog_ResourceId_fkey",
                table: "FeedLog",
                column: "ResourceId",
                principalTable: "Resource",
                principalColumn: "ResourceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FeedLog_ResourceId_fkey",
                table: "FeedLog");

            migrationBuilder.DropIndex(
                name: "IX_FeedLog_ResourceId",
                table: "FeedLog");

            migrationBuilder.DropColumn(
                name: "ResourceId",
                table: "FeedLog");
        }
    }
}
