using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDongTrung.Migrations
{
    public partial class addProductQuantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RealityQuantity",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SystemQuantity",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "CartDetails",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreateId",
                table: "CartDetails",
                type: "varchar(12)",
                maxLength: 12,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "CartDetails",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdateId",
                table: "CartDetails",
                type: "varchar(12)",
                maxLength: 12,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RealityQuantity",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SystemQuantity",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "CartDetails");

            migrationBuilder.DropColumn(
                name: "CreateId",
                table: "CartDetails");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "CartDetails");

            migrationBuilder.DropColumn(
                name: "UpdateId",
                table: "CartDetails");
        }
    }
}
