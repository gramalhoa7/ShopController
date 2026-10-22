using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopControllerAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddExistToItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "Item",
                newName: "Id");

            migrationBuilder.AddColumn<bool>(
                name: "Exist",
                table: "Item",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Exist",
                table: "Item");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Item",
                newName: "id");
        }
    }
}
