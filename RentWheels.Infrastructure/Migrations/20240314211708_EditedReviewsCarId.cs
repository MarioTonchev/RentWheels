using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentWheels.Infrastructure.Migrations
{
    public partial class EditedReviewsCarId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Cars_CardId",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "CardId",
                table: "Reviews",
                newName: "CarId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_CardId",
                table: "Reviews",
                newName: "IX_Reviews_CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Cars_CarId",
                table: "Reviews",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Cars_CarId",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "CarId",
                table: "Reviews",
                newName: "CardId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_CarId",
                table: "Reviews",
                newName: "IX_Reviews_CardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Cars_CardId",
                table: "Reviews",
                column: "CardId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
