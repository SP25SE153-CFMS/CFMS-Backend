using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CFMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseV28 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StockReceipt",
                columns: table => new
                {
                    StockReceiptId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    StockReceiptCode = table.Column<string>(type: "text", nullable: true),
                    ReceiptTypeId = table.Column<Guid>(type: "uuid", nullable: true),
                    FarmId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastEditedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedByUserUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("StockReceipt_pkey", x => x.StockReceiptId);
                    table.ForeignKey(
                        name: "FK_StockReceipt_SubCategory_ReceiptTypeId",
                        column: x => x.ReceiptTypeId,
                        principalTable: "SubCategory",
                        principalColumn: "SubCategoryId");
                    table.ForeignKey(
                        name: "FK_StockReceipt_User_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockReceipt_User_LastEditedByUserUserId",
                        column: x => x.LastEditedByUserUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockReceiptDetail",
                columns: table => new
                {
                    StockReceiptDetailId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    StockReceiptId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<decimal>(type: "numeric", nullable: false),
                    UnitId = table.Column<Guid>(type: "uuid", nullable: true),
                    ToWareId = table.Column<Guid>(type: "uuid", nullable: false),
                    ResourceId = table.Column<Guid>(type: "uuid", nullable: true),
                    ResourceSupplierId = table.Column<Guid>(type: "uuid", nullable: true),
                    WarehouseWareId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("StockReceiptDetail_pkey", x => x.StockReceiptDetailId);
                    table.ForeignKey(
                        name: "FK_StockReceiptDetail_ResourceSupplier_ResourceSupplierId",
                        column: x => x.ResourceSupplierId,
                        principalTable: "ResourceSupplier",
                        principalColumn: "ResourceSupplierId");
                    table.ForeignKey(
                        name: "FK_StockReceiptDetail_Resource_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resource",
                        principalColumn: "ResourceId");
                    table.ForeignKey(
                        name: "FK_StockReceiptDetail_SubCategory_UnitId",
                        column: x => x.UnitId,
                        principalTable: "SubCategory",
                        principalColumn: "SubCategoryId");
                    table.ForeignKey(
                        name: "FK_StockReceiptDetail_Warehouse_WarehouseWareId",
                        column: x => x.WarehouseWareId,
                        principalTable: "Warehouse",
                        principalColumn: "WareId");
                    table.ForeignKey(
                        name: "StockReceiptDetail_StockReceiptId_fkey",
                        column: x => x.StockReceiptId,
                        principalTable: "StockReceipt",
                        principalColumn: "StockReceiptId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockReceipt_LastEditedByUserId",
                table: "StockReceipt",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StockReceipt_LastEditedByUserUserId",
                table: "StockReceipt",
                column: "LastEditedByUserUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StockReceipt_ReceiptTypeId",
                table: "StockReceipt",
                column: "ReceiptTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_StockReceiptDetail_ResourceId",
                table: "StockReceiptDetail",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_StockReceiptDetail_ResourceSupplierId",
                table: "StockReceiptDetail",
                column: "ResourceSupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_StockReceiptDetail_StockReceiptId",
                table: "StockReceiptDetail",
                column: "StockReceiptId");

            migrationBuilder.CreateIndex(
                name: "IX_StockReceiptDetail_UnitId",
                table: "StockReceiptDetail",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_StockReceiptDetail_WarehouseWareId",
                table: "StockReceiptDetail",
                column: "WarehouseWareId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockReceiptDetail");

            migrationBuilder.DropTable(
                name: "StockReceipt");
        }
    }
}
