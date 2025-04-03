using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CFMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseV15 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "ShiftSchedule_ShiftScheduleId_fkey",
                table: "ShiftSchedule");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftSchedule_TaskId",
                table: "ShiftSchedule",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "ShiftSchedule_TaskId_fkey",
                table: "ShiftSchedule",
                column: "TaskId",
                principalTable: "Task",
                principalColumn: "TaskId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "ShiftSchedule_TaskId_fkey",
                table: "ShiftSchedule");

            migrationBuilder.DropIndex(
                name: "IX_ShiftSchedule_TaskId",
                table: "ShiftSchedule");

            migrationBuilder.AddForeignKey(
                name: "ShiftSchedule_ShiftScheduleId_fkey",
                table: "ShiftSchedule",
                column: "ShiftScheduleId",
                principalTable: "Task",
                principalColumn: "TaskId",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
