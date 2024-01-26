using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartInventory.Migrations
{
    public partial class init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StockIn",
                table: "Inventories");

            migrationBuilder.RenameColumn(
                name: "StockOut",
                table: "Inventories",
                newName: "TransactionDate");

            migrationBuilder.AddColumn<bool>(
                name: "isBlockStock",
                table: "Inventories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isInflow",
                table: "Inventories",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isBlockStock",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "isInflow",
                table: "Inventories");

            migrationBuilder.RenameColumn(
                name: "TransactionDate",
                table: "Inventories",
                newName: "StockOut");

            migrationBuilder.AddColumn<DateTime>(
                name: "StockIn",
                table: "Inventories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
