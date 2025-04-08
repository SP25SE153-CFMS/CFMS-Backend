using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CFMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseV17 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Chicken_ChickenId_fkey",
                table: "ChickenBatch");

            migrationBuilder.AddColumn<Guid>(
                name: "ChickenBathId",
                table: "ChickenDetail",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChickenDetail_ChickenBathId",
                table: "ChickenDetail",
                column: "ChickenBathId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChickenBatch_Chicken_ChickenId",
                table: "ChickenBatch",
                column: "ChickenId",
                principalTable: "Chicken",
                principalColumn: "ChickenId");

            migrationBuilder.AddForeignKey(
                name: "ChickenDetail_ChickenBatchId_fkey",
                table: "ChickenDetail",
                column: "ChickenBathId",
                principalTable: "ChickenBatch",
                principalColumn: "ChickenBatchId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChickenBatch_Chicken_ChickenId",
                table: "ChickenBatch");

            migrationBuilder.DropForeignKey(
                name: "ChickenDetail_ChickenBatchId_fkey",
                table: "ChickenDetail");

            migrationBuilder.DropIndex(
                name: "IX_ChickenDetail_ChickenBathId",
                table: "ChickenDetail");

            migrationBuilder.DropColumn(
                name: "ChickenBathId",
                table: "ChickenDetail");

            migrationBuilder.AddForeignKey(
                name: "Chicken_ChickenId_fkey",
                table: "ChickenBatch",
                column: "ChickenId",
                principalTable: "Chicken",
                principalColumn: "ChickenId");
        }
    }
}
