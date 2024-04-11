using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentWheels.Infrastructure.Migrations
{
    public partial class AddedIsActiveForRental : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IsActive",
                table: "Rentals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "true",
                comment: "Is the rent still active or not");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Rentals");
        }
    }
}
