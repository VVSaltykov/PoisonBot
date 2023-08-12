using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PoisonBot.Migrations
{
    /// <inheritdoc />
    public partial class addsneakersindeliverytodb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeliveryId",
                table: "Sneakers",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sneakers_DeliveryId",
                table: "Sneakers",
                column: "DeliveryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sneakers_Deliveries_DeliveryId",
                table: "Sneakers",
                column: "DeliveryId",
                principalTable: "Deliveries",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sneakers_Deliveries_DeliveryId",
                table: "Sneakers");

            migrationBuilder.DropIndex(
                name: "IX_Sneakers_DeliveryId",
                table: "Sneakers");

            migrationBuilder.DropColumn(
                name: "DeliveryId",
                table: "Sneakers");
        }
    }
}
