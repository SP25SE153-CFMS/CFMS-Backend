using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CFMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseV20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserId",
                table: "Notification",
                type: "uuid",
                nullable: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedWhen",
                table: "Notification",
                type: "timestamp without time zone",
                nullable: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedWhen",
                table: "Notification",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Notification",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "LastEditedByUserId",
                table: "Notification",
                type: "uuid",
                nullable: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastEditedWhen",
                table: "Notification",
                type: "timestamp without time zone",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_Notification_CreatedByUserId",
                table: "Notification",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_LastEditedByUserId",
                table: "Notification",
                column: "LastEditedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_User_CreatedByUserId",
                table: "Notification",
                column: "CreatedByUserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_User_LastEditedByUserId",
                table: "Notification",
                column: "LastEditedByUserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notification_User_CreatedByUserId",
                table: "Notification");

            migrationBuilder.DropForeignKey(
                name: "FK_Notification_User_LastEditedByUserId",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_Notification_CreatedByUserId",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_Notification_LastEditedByUserId",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "CreatedWhen",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "DeletedWhen",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "LastEditedByUserId",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "LastEditedWhen",
                table: "Notification");
        }
    }
}
