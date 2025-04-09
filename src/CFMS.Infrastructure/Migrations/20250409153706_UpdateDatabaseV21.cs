using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CFMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseV21 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CurrentStageId",
                table: "ChickenBatch",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChickenBatch_CurrentStageId",
                table: "ChickenBatch",
                column: "CurrentStageId");

            migrationBuilder.AddForeignKey(
                name: "ChickenBatch_CurrentStageId_fkey",
                table: "ChickenBatch",
                column: "CurrentStageId",
                principalTable: "GrowthStage",
                principalColumn: "GrowthStageId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "ChickenBatch_CurrentStageId_fkey",
                table: "ChickenBatch");

            migrationBuilder.DropIndex(
                name: "IX_ChickenBatch_CurrentStageId",
                table: "ChickenBatch");

            migrationBuilder.DropColumn(
                name: "CurrentStageId",
                table: "ChickenBatch");
        }
    }
}
