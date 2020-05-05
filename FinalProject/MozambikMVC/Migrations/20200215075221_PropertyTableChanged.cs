using Microsoft.EntityFrameworkCore.Migrations;

namespace MozambikMVC.Migrations
{
    public partial class PropertyTableChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Items_ItemID",
                table: "Properties");

            migrationBuilder.RenameColumn(
                name: "ItemID",
                table: "Properties",
                newName: "ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_Properties_ItemID",
                table: "Properties",
                newName: "IX_Properties_ItemId");

            migrationBuilder.AlterColumn<int>(
                name: "ItemId",
                table: "Properties",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Items_ItemId",
                table: "Properties",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Items_ItemId",
                table: "Properties");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "Properties",
                newName: "ItemID");

            migrationBuilder.RenameIndex(
                name: "IX_Properties_ItemId",
                table: "Properties",
                newName: "IX_Properties_ItemID");

            migrationBuilder.AlterColumn<int>(
                name: "ItemID",
                table: "Properties",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Items_ItemID",
                table: "Properties",
                column: "ItemID",
                principalTable: "Items",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
