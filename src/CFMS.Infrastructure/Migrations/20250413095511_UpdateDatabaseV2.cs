using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CFMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ParentBatchId",
                table: "ChickenBatch",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChickenBatch_ParentBatchId",
                table: "ChickenBatch",
                column: "ParentBatchId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChickenBatch_ChickenBatch_ParentBatchId",
                table: "ChickenBatch",
                column: "ParentBatchId",
                principalTable: "ChickenBatch",
                principalColumn: "ChickenBatchId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChickenBatch_ChickenBatch_ParentBatchId",
                table: "ChickenBatch");

            migrationBuilder.DropIndex(
                name: "IX_ChickenBatch_ParentBatchId",
                table: "ChickenBatch");

            migrationBuilder.DropColumn(
                name: "ParentBatchId",
                table: "ChickenBatch");
        }
    }
}
