using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentWheels.Infrastructure.Migrations
{
    public partial class RemovedRentalsLocations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RentalsLocations");

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Rentals",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Identifier of the location (place of pick up and drop off)");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_LocationId",
                table: "Rentals",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Locations_LocationId",
                table: "Rentals",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Locations_LocationId",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_LocationId",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Rentals");

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
                name: "IX_RentalsLocations_LocationId",
                table: "RentalsLocations",
                column: "LocationId");
        }
    }
}
