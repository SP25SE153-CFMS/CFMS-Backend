using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CFMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseV23 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuantityLogDetail",
                columns: table => new
                {
                    QuantityLogDetailId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    QuantityLogId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: true),
                    Gender = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("QuantityLogDetail_pkey", x => x.QuantityLogDetailId);
                    table.ForeignKey(
                        name: "QuantityLogDetail_QuantityLogId_fkey",
                        column: x => x.QuantityLogId,
                        principalTable: "QuantityLog",
                        principalColumn: "QuantityLogId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuantityLogDetail_QuantityLogId",
                table: "QuantityLogDetail",
                column: "QuantityLogId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuantityLogDetail");
        }
    }
}
