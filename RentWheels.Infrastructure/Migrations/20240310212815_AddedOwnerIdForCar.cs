using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentWheels.Infrastructure.Migrations
{
    public partial class AddedOwnerIdForCar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "Rentals",
                comment: "Contains details about the renting of a car by a user",
                oldComment: "Containts details about the renting of a car by a user");

            migrationBuilder.AlterColumn<string>(
                name: "Available",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: false,
                comment: "Checks whether the car is rented or not",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Checks wheter the car is rented or not");

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Cars",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                comment: "Identifier of the owner of the car");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_OwnerId",
                table: "Cars",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_AspNetUsers_OwnerId",
                table: "Cars",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_AspNetUsers_OwnerId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_OwnerId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Cars");

            migrationBuilder.AlterTable(
                name: "Rentals",
                comment: "Containts details about the renting of a car by a user",
                oldComment: "Contains details about the renting of a car by a user");

            migrationBuilder.AlterColumn<string>(
                name: "Available",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: false,
                comment: "Checks wheter the car is rented or not",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Checks whether the car is rented or not");
        }
    }
}
