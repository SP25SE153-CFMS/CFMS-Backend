using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CFMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddUserLogRelationsV3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "evaluationcriteria",
                columns: table => new
                {
                    criteriaid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    criterianame = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    minvalue = table.Column<double>(type: "double precision", nullable: true),
                    maxvalue = table.Column<double>(type: "double precision", nullable: true),
                    unit = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    tasktype = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("evaluationcriteria_pkey", x => x.criteriaid);
                });

            migrationBuilder.CreateTable(
                name: "evaluationsummary",
                columns: table => new
                {
                    summaryid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    taskid = table.Column<Guid>(type: "uuid", nullable: false),
                    tasktype = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    totalcriteria = table.Column<int>(type: "integer", nullable: false),
                    passedcriteria = table.Column<int>(type: "integer", nullable: false),
                    failedcriteria = table.Column<int>(type: "integer", nullable: false),
                    overallresult = table.Column<bool>(type: "boolean", nullable: false),
                    criteriaid = table.Column<Guid>(type: "uuid", nullable: false),
                    userid = table.Column<Guid>(type: "uuid", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    evaluationdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("evaluationsummary_pkey", x => x.summaryid);
                });

            migrationBuilder.CreateTable(
                name: "evaluation_summary_criteria",
                columns: table => new
                {
                    criteriaId = table.Column<Guid>(type: "uuid", nullable: false),
                    summaryId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_evaluation_summary_criteria", x => new { x.criteriaId, x.summaryId });
                    table.ForeignKey(
                        name: "FK_evaluation_summary_criteria_evaluationcriteria_criteriaId",
                        column: x => x.criteriaId,
                        principalTable: "evaluationcriteria",
                        principalColumn: "criteriaid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_evaluation_summary_criteria_evaluationsummary_summaryId",
                        column: x => x.summaryId,
                        principalTable: "evaluationsummary",
                        principalColumn: "summaryid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_summary_log",
                columns: table => new
                {
                    summaryId = table.Column<Guid>(type: "uuid", nullable: false),
                    userId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_summary_log", x => new { x.summaryId, x.userId });
                    table.ForeignKey(
                        name: "FK_user_summary_log_evaluationsummary_summaryId",
                        column: x => x.summaryId,
                        principalTable: "evaluationsummary",
                        principalColumn: "summaryid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_summary_log_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_evaluation_summary_criteria_summaryId",
                table: "evaluation_summary_criteria",
                column: "summaryId");

            migrationBuilder.CreateIndex(
                name: "IX_user_summary_log_userId",
                table: "user_summary_log",
                column: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "evaluation_summary_criteria");

            migrationBuilder.DropTable(
                name: "user_summary_log");

            migrationBuilder.DropTable(
                name: "evaluationcriteria");

            migrationBuilder.DropTable(
                name: "evaluationsummary");
        }
    }
}
