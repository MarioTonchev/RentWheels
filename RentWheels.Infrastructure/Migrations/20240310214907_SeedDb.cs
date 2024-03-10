using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentWheels.Infrastructure.Migrations
{
    public partial class SeedDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Engines",
                columns: new[] { "Id", "Cubage", "FuelType", "Horsepower" },
                values: new object[,]
                {
                    { 1, 1400, "diesel", 100 },
                    { 2, 2000, "diesel", 180 },
                    { 3, 2200, "gasoline", 240 },
                    { 4, 4000, "gasoline", 500 }
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "Available", "Brand", "Color", "EngineId", "ImageUrl", "Model", "OwnerId", "PricePerDay", "Year" },
                values: new object[] { 1, "true", "Audi", "black metallic", 2, "https://as1.ftcdn.net/v2/jpg/03/63/44/86/1000_F_363448659_uZxsIp3cObzOiDx6oDi20fb3QFoYVAJF.jpg", "A4", "572f859b-0afa-4112-aa5b-23a6d9560fca", 100m, 2017 });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "Available", "Brand", "Color", "EngineId", "ImageUrl", "Model", "OwnerId", "PricePerDay", "Year" },
                values: new object[] { 2, "true", "BMW", "royal blue", 4, "https://as1.ftcdn.net/v2/jpg/04/35/92/40/1000_F_435924070_A2n5ZyQUF7nCRsYZj6SX1SAYOn5sggFh.jpg", "M5", "572f859b-0afa-4112-aa5b-23a6d9560fca", 350m, 2018 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Engines",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Engines",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Engines",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Engines",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
