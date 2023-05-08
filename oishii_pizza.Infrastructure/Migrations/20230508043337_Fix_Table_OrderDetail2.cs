using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace oishii_pizza.Infrastructure.Migrations
{
    public partial class Fix_Table_OrderDetail2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "DeleteAt",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "OrderDetails");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "OrderDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteAt",
                table: "OrderDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "OrderDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "OrderDetails",
                type: "datetime2",
                nullable: true);
        }
    }
}
