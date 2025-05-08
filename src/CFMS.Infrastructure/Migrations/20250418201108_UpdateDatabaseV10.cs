using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CFMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseV10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            ALTER TABLE ""TaskRequest""
            ALTER COLUMN ""ImageUrl"" TYPE text[] 
            USING ARRAY[""ImageUrl""];
        ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            ALTER TABLE ""TaskRequest""
            ALTER COLUMN ""ImageUrl"" TYPE text
            USING ARRAY[""ImageUrl""];
        ");
        }
    }
}
