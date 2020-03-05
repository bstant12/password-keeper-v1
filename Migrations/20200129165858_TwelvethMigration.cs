using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PasswordKeeper.Migrations
{
    public partial class TwelvethMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpMonth",
                table: "CreditCards");

            migrationBuilder.DropColumn(
                name: "ExpYear",
                table: "CreditCards");

            migrationBuilder.AddColumn<DateTime>(
                name: "Expiration",
                table: "CreditCards",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Expiration",
                table: "CreditCards");

            migrationBuilder.AddColumn<string>(
                name: "ExpMonth",
                table: "CreditCards",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ExpYear",
                table: "CreditCards",
                nullable: false,
                defaultValue: "");
        }
    }
}
