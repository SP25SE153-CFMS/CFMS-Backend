using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CFMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLatestDbV8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "InventoryRequest_RequestId_fkey",
                table: "InventoryRequest");

            migrationBuilder.DropColumn(
                name: "ShiftScheduleId",
                table: "Assignment");

            migrationBuilder.DropColumn(
                name: "TaskScheduleId",
                table: "Assignment");

            migrationBuilder.AddForeignKey(
                name: "InventoryRequest_RequestId_fkey",
                table: "InventoryRequest",
                column: "RequestId",
                principalTable: "Request",
                principalColumn: "RequestId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "InventoryRequest_RequestId_fkey",
                table: "InventoryRequest");

            migrationBuilder.AddColumn<Guid>(
                name: "ShiftScheduleId",
                table: "Assignment",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TaskScheduleId",
                table: "Assignment",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "InventoryRequest_RequestId_fkey",
                table: "InventoryRequest",
                column: "RequestId",
                principalTable: "Request",
                principalColumn: "RequestId");
        }
    }
}
