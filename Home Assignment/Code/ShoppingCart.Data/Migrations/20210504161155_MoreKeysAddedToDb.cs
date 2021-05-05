using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingCart.Data.Migrations
{
    public partial class MoreKeysAddedToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PubicKey",
                table: "StudentAssignments");

            migrationBuilder.AddColumn<string>(
                name: "Iv",
                table: "StudentAssignments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "StudentAssignments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrivateKey",
                table: "StudentAssignments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PublicKey",
                table: "StudentAssignments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Iv",
                table: "StudentAssignments");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "StudentAssignments");

            migrationBuilder.DropColumn(
                name: "PrivateKey",
                table: "StudentAssignments");

            migrationBuilder.DropColumn(
                name: "PublicKey",
                table: "StudentAssignments");

            migrationBuilder.AddColumn<string>(
                name: "PubicKey",
                table: "StudentAssignments",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
