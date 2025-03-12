using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CFMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixRevokedToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RevokedToken_User_CreatedByUserId",
                table: "RevokedToken");

            migrationBuilder.DropForeignKey(
                name: "FK_RevokedToken_User_LastEditedByUserId",
                table: "RevokedToken");

            migrationBuilder.DropIndex(
                name: "IX_RevokedToken_CreatedByUserId",
                table: "RevokedToken");

            migrationBuilder.DropIndex(
                name: "IX_RevokedToken_LastEditedByUserId",
                table: "RevokedToken");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "RevokedToken");

            migrationBuilder.DropColumn(
                name: "CreatedWhen",
                table: "RevokedToken");

            migrationBuilder.DropColumn(
                name: "DeletedWhen",
                table: "RevokedToken");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "RevokedToken");

            migrationBuilder.DropColumn(
                name: "LastEditedByUserId",
                table: "RevokedToken");

            migrationBuilder.DropColumn(
                name: "LastEditedWhen",
                table: "RevokedToken");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserId",
                table: "RevokedToken",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedWhen",
                table: "RevokedToken",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedWhen",
                table: "RevokedToken",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "RevokedToken",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "LastEditedByUserId",
                table: "RevokedToken",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastEditedWhen",
                table: "RevokedToken",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_RevokedToken_CreatedByUserId",
                table: "RevokedToken",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RevokedToken_LastEditedByUserId",
                table: "RevokedToken",
                column: "LastEditedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RevokedToken_User_CreatedByUserId",
                table: "RevokedToken",
                column: "CreatedByUserId",
                principalTable: "User",
                principalColumn: "userId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RevokedToken_User_LastEditedByUserId",
                table: "RevokedToken",
                column: "LastEditedByUserId",
                principalTable: "User",
                principalColumn: "userId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
