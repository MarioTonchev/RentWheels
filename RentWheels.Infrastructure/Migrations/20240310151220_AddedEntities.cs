using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentWheels.Infrastructure.Migrations
{
    public partial class AddedEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Engines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Identifier of the engine")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Horsepower = table.Column<int>(type: "int", nullable: false, comment: "Horsepower of the engine"),
                    Cubage = table.Column<int>(type: "int", nullable: false, comment: "Cubage of the engine"),
                    FuelType = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Fuel type of the engine")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Engines", x => x.Id);
                },
                comment: "Details about the engine of the car");

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Identifier of the location")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PickUp = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Where the car will be picked up by the renter"),
                    DropOff = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Where the car will be dropped off by the renter")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                },
                comment: "Information about where the car will be picked up and dropped off");

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Identifier for car")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "Brand of the car"),
                    Model = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "Model of the car"),
                    Year = table.Column<int>(type: "int", nullable: false, comment: "Year in which the car was produced"),
                    Color = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false, comment: "Color of the car"),
                    PricePerDay = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "Price per day for renting the car"),
                    Available = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Checks wheter the car is rented or not"),
                    EngineId = table.Column<int>(type: "int", nullable: false, comment: "Id of the engine of the car")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_Engines_EngineId",
                        column: x => x.EngineId,
                        principalTable: "Engines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Information about the car that can be rented");

            migrationBuilder.CreateTable(
                name: "Rentals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Identifier of the rental")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<int>(type: "int", nullable: false, comment: "Identifier of the rented car"),
                    RenterId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Identifier of the renter"),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "The date that the car was rented"),
                    End = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "The date that the car rent will end"),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "Total price of the rent")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rentals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rentals_AspNetUsers_RenterId",
                        column: x => x.RenterId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rentals_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Containts details about the renting of a car by a user");

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Identifier of the review")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardId = table.Column<int>(type: "int", nullable: false, comment: "Identifier of the reviewed car"),
                    ReviewerId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Identifier of the reviewer of the car"),
                    Rating = table.Column<int>(type: "int", nullable: false, comment: "Rating given by the reviewer for the car"),
                    Comment = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false, comment: "Comment given by the reviewer for the car")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_AspNetUsers_ReviewerId",
                        column: x => x.ReviewerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Cars_CardId",
                        column: x => x.CardId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Reviews posted by the user about the experience with the car");

            migrationBuilder.CreateTable(
                name: "RentalsLocations",
                columns: table => new
                {
                    RentalId = table.Column<int>(type: "int", nullable: false, comment: "Identifier of the rental, part of composite key"),
                    LocationId = table.Column<int>(type: "int", nullable: false, comment: "Identifier of the location, part of composite key")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalsLocations", x => new { x.RentalId, x.LocationId });
                    table.ForeignKey(
                        name: "FK_RentalsLocations_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RentalsLocations_Rentals_RentalId",
                        column: x => x.RentalId,
                        principalTable: "Rentals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_EngineId",
                table: "Cars",
                column: "EngineId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_CarId",
                table: "Rentals",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_RenterId",
                table: "Rentals",
                column: "RenterId");

            migrationBuilder.CreateIndex(
                name: "IX_RentalsLocations_LocationId",
                table: "RentalsLocations",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CardId",
                table: "Reviews",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ReviewerId",
                table: "Reviews",
                column: "ReviewerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RentalsLocations");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Rentals");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Engines");
        }
    }
}
