using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDongTrung.Migrations
{
    public partial class updateFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductTypes_ProductTypeId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Warehouses_Products_ProductsId",
                table: "Warehouses");

            migrationBuilder.DropIndex(
                name: "IX_Warehouses_ProductsId",
                table: "Warehouses");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductTypeId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductsId",
                table: "Warehouses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductsId",
                table: "Warehouses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_ProductsId",
                table: "Warehouses",
                column: "ProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductTypeId",
                table: "Products",
                column: "ProductTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductTypes_ProductTypeId",
                table: "Products",
                column: "ProductTypeId",
                principalTable: "ProductTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouses_Products_ProductsId",
                table: "Warehouses",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
