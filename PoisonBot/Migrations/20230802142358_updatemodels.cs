using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PoisonBot.Migrations
{
    /// <inheritdoc />
    public partial class updatemodels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sneakers_Users_UserId",
                table: "Sneakers");

            migrationBuilder.DropIndex(
                name: "IX_Sneakers_UserId",
                table: "Sneakers");

            migrationBuilder.CreateTable(
                name: "SneakersUser",
                columns: table => new
                {
                    SneakersId = table.Column<int>(type: "integer", nullable: false),
                    UsersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SneakersUser", x => new { x.SneakersId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_SneakersUser_Sneakers_SneakersId",
                        column: x => x.SneakersId,
                        principalTable: "Sneakers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SneakersUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SneakersUser_UsersId",
                table: "SneakersUser",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SneakersUser");

            migrationBuilder.CreateIndex(
                name: "IX_Sneakers_UserId",
                table: "Sneakers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sneakers_Users_UserId",
                table: "Sneakers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
