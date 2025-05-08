using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CFMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseV34 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockReceiptDetail_ResourceSupplier_ResourceSupplierId",
                table: "StockReceiptDetail");

            migrationBuilder.DropTable(
                name: "EvaluationResultDetail");

            migrationBuilder.DropTable(
                name: "TemplateCriteria");

            migrationBuilder.DropTable(
                name: "EvaluationResult");

            migrationBuilder.DropTable(
                name: "EvaluatedTarget");

            migrationBuilder.DropTable(
                name: "EvaluationTemplate");

            migrationBuilder.RenameColumn(
                name: "ResourceSupplierId",
                table: "StockReceiptDetail",
                newName: "SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_StockReceiptDetail_ResourceSupplierId",
                table: "StockReceiptDetail",
                newName: "IX_StockReceiptDetail_SupplierId");

            migrationBuilder.AddColumn<Guid>(
                name: "SupplierId",
                table: "WareStock",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WareStock_SupplierId",
                table: "WareStock",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockReceiptDetail_Supplier_SupplierId",
                table: "StockReceiptDetail",
                column: "SupplierId",
                principalTable: "Supplier",
                principalColumn: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "WareStock_SupplierId_fkey",
                table: "WareStock",
                column: "SupplierId",
                principalTable: "Supplier",
                principalColumn: "SupplierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockReceiptDetail_Supplier_SupplierId",
                table: "StockReceiptDetail");

            migrationBuilder.DropForeignKey(
                name: "WareStock_SupplierId_fkey",
                table: "WareStock");

            migrationBuilder.DropIndex(
                name: "IX_WareStock_SupplierId",
                table: "WareStock");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "WareStock");

            migrationBuilder.RenameColumn(
                name: "SupplierId",
                table: "StockReceiptDetail",
                newName: "ResourceSupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_StockReceiptDetail_SupplierId",
                table: "StockReceiptDetail",
                newName: "IX_StockReceiptDetail_ResourceSupplierId");

            migrationBuilder.CreateTable(
                name: "EvaluatedTarget",
                columns: table => new
                {
                    EvaluatedTargetId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TargetId = table.Column<Guid>(type: "uuid", nullable: true),
                    TargetTypeId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("EvaluatedTarget_pkey", x => x.EvaluatedTargetId);
                    table.ForeignKey(
                        name: "EvaluatedTarget_TargetId_fkey",
                        column: x => x.TargetId,
                        principalTable: "Task",
                        principalColumn: "TaskId");
                    table.ForeignKey(
                        name: "EvaluatedTarget_TargetId_fkey1",
                        column: x => x.TargetId,
                        principalTable: "Chicken",
                        principalColumn: "ChickenId");
                    table.ForeignKey(
                        name: "EvaluatedTarget_TargetTypeId_fkey",
                        column: x => x.TargetTypeId,
                        principalTable: "SubCategory",
                        principalColumn: "SubCategoryId");
                    table.ForeignKey(
                        name: "FK_EvaluatedTarget_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EvaluatedTarget_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EvaluationTemplate",
                columns: table => new
                {
                    EvaluationTemplateId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TemplateTypeId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TemplateName = table.Column<string>(type: "character varying", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("EvaluationTemplate_pkey", x => x.EvaluationTemplateId);
                    table.ForeignKey(
                        name: "EvaluationTemplate_TemplateTypeId_fkey",
                        column: x => x.TemplateTypeId,
                        principalTable: "SubCategory",
                        principalColumn: "SubCategoryId");
                    table.ForeignKey(
                        name: "FK_EvaluationTemplate_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EvaluationTemplate_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EvaluationResult",
                columns: table => new
                {
                    EvaluationResultId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    EvaluatedTargetId = table.Column<Guid>(type: "uuid", nullable: true),
                    EvaluationTemplateId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EvaluatedDate = table.Column<DateTime>(name: "EvaluatedDate ", type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("EvaluationResult_pkey", x => x.EvaluationResultId);
                    table.ForeignKey(
                        name: "EvaluationResult_EvaluatedTargetId_fkey",
                        column: x => x.EvaluatedTargetId,
                        principalTable: "EvaluatedTarget",
                        principalColumn: "EvaluatedTargetId");
                    table.ForeignKey(
                        name: "EvaluationResult_EvaluationTemplateId_fkey",
                        column: x => x.EvaluationTemplateId,
                        principalTable: "EvaluationTemplate",
                        principalColumn: "EvaluationTemplateId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_EvaluationResult_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EvaluationResult_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TemplateCriteria",
                columns: table => new
                {
                    TemplateCriteriaId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CriteriaId = table.Column<Guid>(type: "uuid", nullable: true),
                    EvaluationTemplateId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ExpectedCondition = table.Column<string>(type: "character varying", nullable: true),
                    ExpectedUnit = table.Column<string>(type: "character varying", nullable: true),
                    ExpectedValue = table.Column<string>(type: "character varying", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TemplateName = table.Column<string>(type: "character varying", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("TemplateCriteria_pkey", x => x.TemplateCriteriaId);
                    table.ForeignKey(
                        name: "FK_TemplateCriteria_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TemplateCriteria_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "TemplateCriteria_CriteriaId_fkey",
                        column: x => x.CriteriaId,
                        principalTable: "SubCategory",
                        principalColumn: "SubCategoryId");
                    table.ForeignKey(
                        name: "TemplateCriteria_EvaluationTemplateId_fkey",
                        column: x => x.EvaluationTemplateId,
                        principalTable: "EvaluationTemplate",
                        principalColumn: "EvaluationTemplateId");
                });

            migrationBuilder.CreateTable(
                name: "EvaluationResultDetail",
                columns: table => new
                {
                    EvaluationResultDetailId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    EvaluationResultId = table.Column<Guid>(type: "uuid", nullable: true),
                    ActualValue = table.Column<string>(type: "character varying", nullable: true),
                    IsPass = table.Column<int>(type: "integer", nullable: true, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("EvaluationResultDetail_pkey", x => x.EvaluationResultDetailId);
                    table.ForeignKey(
                        name: "EvaluationResultDetail_EvaluationResultId_fkey",
                        column: x => x.EvaluationResultId,
                        principalTable: "EvaluationResult",
                        principalColumn: "EvaluationResultId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EvaluatedTarget_CreatedByUserId",
                table: "EvaluatedTarget",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluatedTarget_LastEditedByUserId",
                table: "EvaluatedTarget",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluatedTarget_TargetId",
                table: "EvaluatedTarget",
                column: "TargetId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluatedTarget_TargetTypeId",
                table: "EvaluatedTarget",
                column: "TargetTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationResult_CreatedByUserId",
                table: "EvaluationResult",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationResult_EvaluatedTargetId",
                table: "EvaluationResult",
                column: "EvaluatedTargetId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationResult_EvaluationTemplateId",
                table: "EvaluationResult",
                column: "EvaluationTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationResult_LastEditedByUserId",
                table: "EvaluationResult",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationResultDetail_EvaluationResultId",
                table: "EvaluationResultDetail",
                column: "EvaluationResultId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationTemplate_CreatedByUserId",
                table: "EvaluationTemplate",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationTemplate_LastEditedByUserId",
                table: "EvaluationTemplate",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationTemplate_TemplateTypeId",
                table: "EvaluationTemplate",
                column: "TemplateTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateCriteria_CreatedByUserId",
                table: "TemplateCriteria",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateCriteria_CriteriaId",
                table: "TemplateCriteria",
                column: "CriteriaId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateCriteria_EvaluationTemplateId",
                table: "TemplateCriteria",
                column: "EvaluationTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateCriteria_LastEditedByUserId",
                table: "TemplateCriteria",
                column: "LastEditedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockReceiptDetail_ResourceSupplier_ResourceSupplierId",
                table: "StockReceiptDetail",
                column: "ResourceSupplierId",
                principalTable: "ResourceSupplier",
                principalColumn: "ResourceSupplierId");
        }
    }
}
