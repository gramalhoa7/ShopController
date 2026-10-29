using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopControllerAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixOrderedItems2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderedItems_Item_ItemOrdId",
                table: "OrderedItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderedItems_ItemOrdId",
                table: "OrderedItems");

            migrationBuilder.DropColumn(
                name: "ItemOrdId",
                table: "OrderedItems");

            migrationBuilder.CreateIndex(
                name: "IX_OrderedItems_ItemId",
                table: "OrderedItems",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderedItems_Item_ItemId",
                table: "OrderedItems",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderedItems_Item_ItemId",
                table: "OrderedItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderedItems_ItemId",
                table: "OrderedItems");

            migrationBuilder.AddColumn<Guid>(
                name: "ItemOrdId",
                table: "OrderedItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_OrderedItems_ItemOrdId",
                table: "OrderedItems",
                column: "ItemOrdId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderedItems_Item_ItemOrdId",
                table: "OrderedItems",
                column: "ItemOrdId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
