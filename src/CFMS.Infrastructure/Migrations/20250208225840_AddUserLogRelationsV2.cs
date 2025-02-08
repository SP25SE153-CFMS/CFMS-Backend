using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CFMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddUserLogRelationsV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HealthlogUser");

            migrationBuilder.DropTable(
                name: "QuantitylogUser");

            migrationBuilder.DropTable(
                name: "userhealthlog");

            migrationBuilder.DropTable(
                name: "userquantitylog");

            migrationBuilder.DropTable(
                name: "uservaccinationlog");

            migrationBuilder.DropTable(
                name: "UserVaccinationlog");

            migrationBuilder.CreateTable(
                name: "user_health_log",
                columns: table => new
                {
                    hLogId = table.Column<Guid>(type: "uuid", nullable: false),
                    userId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_health_log", x => new { x.hLogId, x.userId });
                    table.ForeignKey(
                        name: "FK_user_health_log_healthlog_hLogId",
                        column: x => x.hLogId,
                        principalTable: "healthlog",
                        principalColumn: "hlogid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_health_log_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_quantity_log",
                columns: table => new
                {
                    qLogId = table.Column<Guid>(type: "uuid", nullable: false),
                    userId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_quantity_log", x => new { x.qLogId, x.userId });
                    table.ForeignKey(
                        name: "FK_user_quantity_log_quantitylog_qLogId",
                        column: x => x.qLogId,
                        principalTable: "quantitylog",
                        principalColumn: "qlogid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_quantity_log_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_vaccine_log",
                columns: table => new
                {
                    userId = table.Column<Guid>(type: "uuid", nullable: false),
                    vLogId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_vaccine_log", x => new { x.userId, x.vLogId });
                    table.ForeignKey(
                        name: "FK_user_vaccine_log_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_vaccine_log_vaccinationlog_vLogId",
                        column: x => x.vLogId,
                        principalTable: "vaccinationlog",
                        principalColumn: "vlogid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_health_log_userId",
                table: "user_health_log",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_user_quantity_log_userId",
                table: "user_quantity_log",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_user_vaccine_log_vLogId",
                table: "user_vaccine_log",
                column: "vLogId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_health_log");

            migrationBuilder.DropTable(
                name: "user_quantity_log");

            migrationBuilder.DropTable(
                name: "user_vaccine_log");

            migrationBuilder.CreateTable(
                name: "HealthlogUser",
                columns: table => new
                {
                    HealthLogsHlogid = table.Column<Guid>(type: "uuid", nullable: false),
                    UsersUserid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthlogUser", x => new { x.HealthLogsHlogid, x.UsersUserid });
                    table.ForeignKey(
                        name: "FK_HealthlogUser_healthlog_HealthLogsHlogid",
                        column: x => x.HealthLogsHlogid,
                        principalTable: "healthlog",
                        principalColumn: "hlogid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HealthlogUser_users_UsersUserid",
                        column: x => x.UsersUserid,
                        principalTable: "users",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuantitylogUser",
                columns: table => new
                {
                    QuantityLogsQlogid = table.Column<Guid>(type: "uuid", nullable: false),
                    UsersUserid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuantitylogUser", x => new { x.QuantityLogsQlogid, x.UsersUserid });
                    table.ForeignKey(
                        name: "FK_QuantitylogUser_quantitylog_QuantityLogsQlogid",
                        column: x => x.QuantityLogsQlogid,
                        principalTable: "quantitylog",
                        principalColumn: "qlogid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuantitylogUser_users_UsersUserid",
                        column: x => x.UsersUserid,
                        principalTable: "users",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "userhealthlog",
                columns: table => new
                {
                    hlogid = table.Column<Guid>(type: "uuid", nullable: false),
                    userid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "userhealthlog_hlogid_fkey",
                        column: x => x.hlogid,
                        principalTable: "healthlog",
                        principalColumn: "hlogid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "userhealthlog_userid_fkey",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "userquantitylog",
                columns: table => new
                {
                    qlogid = table.Column<Guid>(type: "uuid", nullable: false),
                    userid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "userquantitylog_qlogid_fkey",
                        column: x => x.qlogid,
                        principalTable: "quantitylog",
                        principalColumn: "qlogid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "userquantitylog_userid_fkey",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "uservaccinationlog",
                columns: table => new
                {
                    userid = table.Column<Guid>(type: "uuid", nullable: false),
                    vlogid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "uservaccinationlog_userid_fkey",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "uservaccinationlog_vlogid_fkey",
                        column: x => x.vlogid,
                        principalTable: "vaccinationlog",
                        principalColumn: "vlogid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserVaccinationlog",
                columns: table => new
                {
                    UsersUserid = table.Column<Guid>(type: "uuid", nullable: false),
                    VaccinationLogsVlogid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserVaccinationlog", x => new { x.UsersUserid, x.VaccinationLogsVlogid });
                    table.ForeignKey(
                        name: "FK_UserVaccinationlog_users_UsersUserid",
                        column: x => x.UsersUserid,
                        principalTable: "users",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserVaccinationlog_vaccinationlog_VaccinationLogsVlogid",
                        column: x => x.VaccinationLogsVlogid,
                        principalTable: "vaccinationlog",
                        principalColumn: "vlogid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HealthlogUser_UsersUserid",
                table: "HealthlogUser",
                column: "UsersUserid");

            migrationBuilder.CreateIndex(
                name: "IX_QuantitylogUser_UsersUserid",
                table: "QuantitylogUser",
                column: "UsersUserid");

            migrationBuilder.CreateIndex(
                name: "IX_userhealthlog_hlogid",
                table: "userhealthlog",
                column: "hlogid");

            migrationBuilder.CreateIndex(
                name: "IX_userhealthlog_userid",
                table: "userhealthlog",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_userquantitylog_qlogid",
                table: "userquantitylog",
                column: "qlogid");

            migrationBuilder.CreateIndex(
                name: "IX_userquantitylog_userid",
                table: "userquantitylog",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_uservaccinationlog_userid",
                table: "uservaccinationlog",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_uservaccinationlog_vlogid",
                table: "uservaccinationlog",
                column: "vlogid");

            migrationBuilder.CreateIndex(
                name: "IX_UserVaccinationlog_VaccinationLogsVlogid",
                table: "UserVaccinationlog",
                column: "VaccinationLogsVlogid");
        }
    }
}
