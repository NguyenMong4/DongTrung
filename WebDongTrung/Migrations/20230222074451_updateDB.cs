using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDongTrung.Migrations
{
    public partial class updateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_CartDetails_CartDetailIdCart_CartDetailIdProduct",
                table: "Carts");

            migrationBuilder.DropTable(
                name: "CartDetailProduct");

            migrationBuilder.DropIndex(
                name: "IX_Carts_CartDetailIdCart_CartDetailIdProduct",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "ProductForeignKey",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CartDetailIdCart",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "CartDetailIdProduct",
                table: "Carts");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Employees",
                type: "varchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldMaxLength: 10)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "PassWord",
                table: "Employees",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Employees",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Emp_kbn",
                table: "Employees",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Employees",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Employees",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductForeignKey",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Phone",
                keyValue: null,
                column: "Phone",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Employees",
                type: "varchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldMaxLength: 10,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "PassWord",
                keyValue: null,
                column: "PassWord",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "PassWord",
                table: "Employees",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Name",
                keyValue: null,
                column: "Name",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Employees",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Emp_kbn",
                keyValue: null,
                column: "Emp_kbn",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Emp_kbn",
                table: "Employees",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Email",
                keyValue: null,
                column: "Email",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Employees",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Address",
                keyValue: null,
                column: "Address",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Employees",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "CartDetailIdCart",
                table: "Carts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CartDetailIdProduct",
                table: "Carts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CartDetailProduct",
                columns: table => new
                {
                    ProductsId = table.Column<int>(type: "int", nullable: false),
                    CartDetailsIdCart = table.Column<int>(type: "int", nullable: false),
                    CartDetailsIdProduct = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartDetailProduct", x => new { x.ProductsId, x.CartDetailsIdCart, x.CartDetailsIdProduct });
                    table.ForeignKey(
                        name: "FK_CartDetailProduct_CartDetails_CartDetailsIdCart_CartDetailsI~",
                        columns: x => new { x.CartDetailsIdCart, x.CartDetailsIdProduct },
                        principalTable: "CartDetails",
                        principalColumns: new[] { "IdCart", "IdProduct" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartDetailProduct_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_CartDetailIdCart_CartDetailIdProduct",
                table: "Carts",
                columns: new[] { "CartDetailIdCart", "CartDetailIdProduct" });

            migrationBuilder.CreateIndex(
                name: "IX_CartDetailProduct_CartDetailsIdCart_CartDetailsIdProduct",
                table: "CartDetailProduct",
                columns: new[] { "CartDetailsIdCart", "CartDetailsIdProduct" });

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_CartDetails_CartDetailIdCart_CartDetailIdProduct",
                table: "Carts",
                columns: new[] { "CartDetailIdCart", "CartDetailIdProduct" },
                principalTable: "CartDetails",
                principalColumns: new[] { "IdCart", "IdProduct" });
        }
    }
}
