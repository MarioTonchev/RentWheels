using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentWheels.Infrastructure.Migrations
{
    public partial class AddedDefaultValueForAvailableCar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Available",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "true",
                comment: "Checks whether the car is rented or not",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Checks whether the car is rented or not");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Available",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: false,
                comment: "Checks whether the car is rented or not",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "true",
                oldComment: "Checks whether the car is rented or not");
        }
    }
}
