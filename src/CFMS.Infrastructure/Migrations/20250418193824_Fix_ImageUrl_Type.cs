using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CFMS.Infrastructure.Migrations
{
    public partial class Fix_ImageUrl_Type : Migration
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
