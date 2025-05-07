using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CFMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseV38 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockReceipt_User_LastEditedByUserUserId",
                table: "StockReceipt");

            migrationBuilder.DropIndex(
                name: "IX_StockReceipt_LastEditedByUserUserId",
                table: "StockReceipt");

            migrationBuilder.DropColumn(
                name: "LastEditedByUserUserId",
                table: "StockReceipt");

            migrationBuilder.AddColumn<decimal>(
                name: "CurrentQuantity",
                table: "WareTransaction",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_StockReceipt_CreatedByUserId",
                table: "StockReceipt",
                column: "CreatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockReceipt_User_CreatedByUserId",
                table: "StockReceipt",
                column: "CreatedByUserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockReceipt_User_CreatedByUserId",
                table: "StockReceipt");

            migrationBuilder.DropIndex(
                name: "IX_StockReceipt_CreatedByUserId",
                table: "StockReceipt");

            migrationBuilder.DropColumn(
                name: "CurrentQuantity",
                table: "WareTransaction");

            migrationBuilder.AddColumn<Guid>(
                name: "LastEditedByUserUserId",
                table: "StockReceipt",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_StockReceipt_LastEditedByUserUserId",
                table: "StockReceipt",
                column: "LastEditedByUserUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockReceipt_User_LastEditedByUserUserId",
                table: "StockReceipt",
                column: "LastEditedByUserUserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
