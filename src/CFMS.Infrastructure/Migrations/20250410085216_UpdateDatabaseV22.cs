using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CFMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseV22 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FarmId",
                table: "Category");

            migrationBuilder.AddColumn<Guid>(
                name: "FarmId",
                table: "SubCategory",
                type: "uuid",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
