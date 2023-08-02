using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PoisonBot.Migrations
{
    /// <inheritdoc />
    public partial class anotherupdatesneakersmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sneakers_Users_UserId",
                table: "Sneakers");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Sneakers",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Sneakers_Users_UserId",
                table: "Sneakers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sneakers_Users_UserId",
                table: "Sneakers");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Sneakers",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Sneakers_Users_UserId",
                table: "Sneakers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
