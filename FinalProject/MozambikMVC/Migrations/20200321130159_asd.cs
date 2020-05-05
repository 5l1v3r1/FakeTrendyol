using Microsoft.EntityFrameworkCore.Migrations;

namespace MozambikMVC.Migrations
{
    public partial class asd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_DeliveryInformations_DeliveryInformationID",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "DeliveryInformations");

            migrationBuilder.DropIndex(
                name: "IX_Orders_DeliveryInformationID",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeliveryInformationID",
                table: "Orders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeliveryInformationID",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DeliveryInformations",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressDetail = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AddressPosX = table.Column<int>(type: "int", nullable: false),
                    AddressPosY = table.Column<int>(type: "int", nullable: false),
                    BaseAddress = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    OrderID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryInformations", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DeliveryInformationID",
                table: "Orders",
                column: "DeliveryInformationID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_DeliveryInformations_DeliveryInformationID",
                table: "Orders",
                column: "DeliveryInformationID",
                principalTable: "DeliveryInformations",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
