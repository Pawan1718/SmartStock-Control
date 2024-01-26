using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartInventory.Migrations
{
    public partial class init4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isBlockStock",
                table: "Inventories");

            migrationBuilder.RenameColumn(
                name: "isInflow",
                table: "Inventories",
                newName: "isRejected");

            migrationBuilder.AddColumn<string>(
                name: "ReasonForRejection",
                table: "Inventories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TransactionType",
                table: "Inventories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReasonForRejection",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "TransactionType",
                table: "Inventories");

            migrationBuilder.RenameColumn(
                name: "isRejected",
                table: "Inventories",
                newName: "isInflow");

            migrationBuilder.AddColumn<bool>(
                name: "isBlockStock",
                table: "Inventories",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
