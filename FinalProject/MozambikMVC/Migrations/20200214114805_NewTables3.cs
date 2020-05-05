using Microsoft.EntityFrameworkCore.Migrations;

namespace MozambikMVC.Migrations
{
    public partial class NewTables3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_SubMenus_SubMenuId",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Markas_Category_CategoryId",
                table: "Markas");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Models_ModelId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_SubMenus_Menus_MenuID",
                table: "SubMenus");

            migrationBuilder.RenameColumn(
                name: "MenuID",
                table: "SubMenus",
                newName: "MenuId");

            migrationBuilder.RenameIndex(
                name: "IX_SubMenus_MenuID",
                table: "SubMenus",
                newName: "IX_SubMenus_MenuId");

            migrationBuilder.RenameColumn(
                name: "ModelId",
                table: "Products",
                newName: "ModelID");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ModelId",
                table: "Products",
                newName: "IX_Products_ModelID");

            migrationBuilder.RenameColumn(
                name: "SubMenuId",
                table: "Category",
                newName: "SubMenuID");

            migrationBuilder.RenameIndex(
                name: "IX_Category_SubMenuId",
                table: "Category",
                newName: "IX_Category_SubMenuID");

            migrationBuilder.AlterColumn<int>(
                name: "MenuId",
                table: "SubMenus",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ModelID",
                table: "Products",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Markas",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SubMenuID",
                table: "Category",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Category_SubMenus_SubMenuID",
                table: "Category",
                column: "SubMenuID",
                principalTable: "SubMenus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Markas_Category_CategoryId",
                table: "Markas",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Models_ModelID",
                table: "Products",
                column: "ModelID",
                principalTable: "Models",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubMenus_Menus_MenuId",
                table: "SubMenus",
                column: "MenuId",
                principalTable: "Menus",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_SubMenus_SubMenuID",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Markas_Category_CategoryId",
                table: "Markas");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Models_ModelID",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_SubMenus_Menus_MenuId",
                table: "SubMenus");

            migrationBuilder.RenameColumn(
                name: "MenuId",
                table: "SubMenus",
                newName: "MenuID");

            migrationBuilder.RenameIndex(
                name: "IX_SubMenus_MenuId",
                table: "SubMenus",
                newName: "IX_SubMenus_MenuID");

            migrationBuilder.RenameColumn(
                name: "ModelID",
                table: "Products",
                newName: "ModelId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ModelID",
                table: "Products",
                newName: "IX_Products_ModelId");

            migrationBuilder.RenameColumn(
                name: "SubMenuID",
                table: "Category",
                newName: "SubMenuId");

            migrationBuilder.RenameIndex(
                name: "IX_Category_SubMenuID",
                table: "Category",
                newName: "IX_Category_SubMenuId");

            migrationBuilder.AlterColumn<int>(
                name: "MenuID",
                table: "SubMenus",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ModelId",
                table: "Products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Markas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "SubMenuId",
                table: "Category",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Category_SubMenus_SubMenuId",
                table: "Category",
                column: "SubMenuId",
                principalTable: "SubMenus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Markas_Category_CategoryId",
                table: "Markas",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Models_ModelId",
                table: "Products",
                column: "ModelId",
                principalTable: "Models",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubMenus_Menus_MenuID",
                table: "SubMenus",
                column: "MenuID",
                principalTable: "Menus",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
