using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CFMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateChickenCoop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Chicken_PurposeId_fkey",
                table: "Chicken");

            migrationBuilder.DropIndex(
                name: "IX_Chicken_PurposeId",
                table: "Chicken");

            migrationBuilder.DropColumn(
                name: "PurposeId",
                table: "Chicken");

            migrationBuilder.AddColumn<Guid>(
                name: "PurposeId",
                table: "ChickenCoop",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChickenCoop_PurposeId",
                table: "ChickenCoop",
                column: "PurposeId");

            migrationBuilder.AddForeignKey(
                name: "Chicken_PurposeId_fkey",
                table: "ChickenCoop",
                column: "PurposeId",
                principalTable: "SubCategory",
                principalColumn: "SubCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Chicken_PurposeId_fkey",
                table: "ChickenCoop");

            migrationBuilder.DropIndex(
                name: "IX_ChickenCoop_PurposeId",
                table: "ChickenCoop");

            migrationBuilder.DropColumn(
                name: "PurposeId",
                table: "ChickenCoop");

            migrationBuilder.AddColumn<Guid>(
                name: "PurposeId",
                table: "Chicken",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Chicken_PurposeId",
                table: "Chicken",
                column: "PurposeId");

            migrationBuilder.AddForeignKey(
                name: "Chicken_PurposeId_fkey",
                table: "Chicken",
                column: "PurposeId",
                principalTable: "SubCategory",
                principalColumn: "SubCategoryId");
        }
    }
}
