using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopControllerAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixBug : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CashierId",
                table: "Order",
                newName: "CashierSessionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CashierSessionId",
                table: "Order",
                newName: "CashierId");
        }
    }
}
