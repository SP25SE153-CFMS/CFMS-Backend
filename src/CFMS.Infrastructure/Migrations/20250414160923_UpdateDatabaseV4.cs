using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CFMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseV4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FeedingTime",
                table: "FeedSession");

            migrationBuilder.AddColumn<TimeOnly>(
                name: "EndTime",
                table: "FeedSession",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "StartTime",
                table: "FeedSession",
                type: "time",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "FeedSession");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "FeedSession");

            migrationBuilder.AddColumn<DateTime>(
                name: "FeedingTime",
                table: "FeedSession",
                type: "timestamp without time zone",
                nullable: true);
        }
    }
}
