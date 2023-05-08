using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace oishii_pizza.Infrastructure.Migrations
{
    public partial class Init_Table_Order : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameCustomer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumberCustomer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressCustomer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalPrice = table.Column<float>(type: "real", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
