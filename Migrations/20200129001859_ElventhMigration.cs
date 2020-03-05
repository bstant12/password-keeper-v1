using Microsoft.EntityFrameworkCore.Migrations;

namespace PasswordKeeper.Migrations
{
    public partial class ElventhMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PasswordNotes",
                table: "Passwords",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Passwords",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordNotes",
                table: "Passwords");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Passwords");
        }
    }
}
