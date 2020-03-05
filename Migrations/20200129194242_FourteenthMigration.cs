using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PasswordKeeper.Migrations
{
    public partial class FourteenthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Passwords_Passwords_PasswordMakerPasswordId",
                table: "Passwords");

            migrationBuilder.DropIndex(
                name: "IX_Passwords_PasswordMakerPasswordId",
                table: "Passwords");

            migrationBuilder.DropColumn(
                name: "PasswordMakerPasswordId",
                table: "Passwords");

            migrationBuilder.CreateTable(
                name: "PersonalInformations",
                columns: table => new
                {
                    PersonalInformationId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    InfoName = table.Column<string>(nullable: true),
                    InfoContent = table.Column<string>(nullable: true),
                    InfoNote = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalInformations", x => x.PersonalInformationId);
                    table.ForeignKey(
                        name: "FK_PersonalInformations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonalInformations_UserId",
                table: "PersonalInformations",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonalInformations");

            migrationBuilder.AddColumn<int>(
                name: "PasswordMakerPasswordId",
                table: "Passwords",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Passwords_PasswordMakerPasswordId",
                table: "Passwords",
                column: "PasswordMakerPasswordId");

            migrationBuilder.AddForeignKey(
                name: "FK_Passwords_Passwords_PasswordMakerPasswordId",
                table: "Passwords",
                column: "PasswordMakerPasswordId",
                principalTable: "Passwords",
                principalColumn: "PasswordId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
