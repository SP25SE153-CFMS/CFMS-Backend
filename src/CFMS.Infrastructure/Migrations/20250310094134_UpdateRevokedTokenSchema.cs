using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CFMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRevokedTokenSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RevokedToken",
                columns: table => new
                {
                    revokedTokenId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    token = table.Column<string>(type: "character varying", nullable: false),
                    tokenType = table.Column<int>(type: "integer", nullable: false),
                    revokedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    expiryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    userId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("RevokedToken_pkey", x => x.revokedTokenId);
                    table.ForeignKey(
                        name: "RevokedToken_userId_fkey",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "userId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RevokedToken_userId",
                table: "RevokedToken",
                column: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RevokedToken");
        }
    }
}
