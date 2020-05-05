using Microsoft.EntityFrameworkCore.Migrations;

namespace MozambikMVC.Migrations
{
    public partial class ItemTableChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_ItemID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Items");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ItemID",
                table: "Products",
                column: "ItemID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_ItemID",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ItemID",
                table: "Products",
                column: "ItemID",
                unique: true);
        }
    }
}
