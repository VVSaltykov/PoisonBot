using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PoisonBot.Migrations
{
    /// <inheritdoc />
    public partial class addtypeordertodelivery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TypeOrder",
                table: "Deliveries",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeOrder",
                table: "Deliveries");
        }
    }
}
