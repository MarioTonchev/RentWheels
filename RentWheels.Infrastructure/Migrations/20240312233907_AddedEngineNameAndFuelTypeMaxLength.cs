using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentWheels.Infrastructure.Migrations
{
    public partial class AddedEngineNameAndFuelTypeMaxLength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FuelType",
                table: "Engines",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "Fuel type of the engine",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Fuel type of the engine");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Engines",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "Name of the engine");

            migrationBuilder.UpdateData(
                table: "Engines",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Small");

            migrationBuilder.UpdateData(
                table: "Engines",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Medium");

            migrationBuilder.UpdateData(
                table: "Engines",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Big");

            migrationBuilder.UpdateData(
                table: "Engines",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Sport");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Engines");

            migrationBuilder.AlterColumn<string>(
                name: "FuelType",
                table: "Engines",
                type: "nvarchar(max)",
                nullable: false,
                comment: "Fuel type of the engine",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "Fuel type of the engine");
        }
    }
}
