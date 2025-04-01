using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CFMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLatestDbV11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FarmId",
                table: "ResourceSupplier");

            migrationBuilder.AddColumn<Guid>(
                name: "FarmId",
                table: "Supplier",
                type: "uuid",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FarmId",
                table: "Supplier");

            migrationBuilder.AddColumn<Guid>(
                name: "FarmId",
                table: "ResourceSupplier",
                type: "uuid",
                nullable: true);
        }
    }
}
