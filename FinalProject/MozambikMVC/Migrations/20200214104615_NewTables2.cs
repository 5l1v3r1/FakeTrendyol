using Microsoft.EntityFrameworkCore.Migrations;

namespace MozambikMVC.Migrations
{
    public partial class NewTables2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_SubCategories_SubMenuId",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_SubCategories_Categories_MenuID",
                table: "SubCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubCategories",
                table: "SubCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "SubCategories",
                newName: "SubMenus");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Menus");

            migrationBuilder.RenameIndex(
                name: "IX_SubCategories_MenuID",
                table: "SubMenus",
                newName: "IX_SubMenus_MenuID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubMenus",
                table: "SubMenus",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Menus",
                table: "Menus",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_SubMenus_SubMenuId",
                table: "Category",
                column: "SubMenuId",
                principalTable: "SubMenus",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_SubMenus_SubMenuId",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_SubMenus_Menus_MenuID",
                table: "SubMenus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubMenus",
                table: "SubMenus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Menus",
                table: "Menus");

            migrationBuilder.RenameTable(
                name: "SubMenus",
                newName: "SubCategories");

            migrationBuilder.RenameTable(
                name: "Menus",
                newName: "Categories");

            migrationBuilder.RenameIndex(
                name: "IX_SubMenus_MenuID",
                table: "SubCategories",
                newName: "IX_SubCategories_MenuID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubCategories",
                table: "SubCategories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_SubCategories_SubMenuId",
                table: "Category",
                column: "SubMenuId",
                principalTable: "SubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategories_Categories_MenuID",
                table: "SubCategories",
                column: "MenuID",
                principalTable: "Categories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
