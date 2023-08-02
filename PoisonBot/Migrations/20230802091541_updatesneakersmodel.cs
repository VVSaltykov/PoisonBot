using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PoisonBot.Migrations
{
    /// <inheritdoc />
    public partial class updatesneakersmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "Sneakers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Size",
                table: "Sneakers");
        }
    }
}
