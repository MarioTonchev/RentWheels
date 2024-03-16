using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentWheels.Infrastructure.Migrations
{
    public partial class AddedCategoryAndRemovedLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Locations_LocationId",
                table: "Rentals");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_LocationId",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Rentals");

            migrationBuilder.AddColumn<string>(
                name: "DropOffLocation",
                table: "Rentals",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                comment: "Where the car will be dropped off");

            migrationBuilder.AddColumn<string>(
                name: "PickUpLocation",
                table: "Rentals",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                comment: "Where the car will be picked up");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Shape of the car");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Identifier of category")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Category's name"),
                    DoorCount = table.Column<int>(type: "int", nullable: false, comment: "The number of doors this category has")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                },
                comment: "Shape of the car's coupe");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "DoorCount", "Name" },
                values: new object[,]
                {
                    { 1, 4, "Sedan" },
                    { 2, 4, "Hatchback" },
                    { 3, 4, "SUV" },
                    { 4, 4, "Estate" },
                    { 5, 2, "Coupe" },
                    { 6, 2, "Supercar" }
                });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1,
                column: "CategoryId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 2,
                column: "CategoryId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 3,
                column: "CategoryId",
                value: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CategoryId",
                table: "Cars",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Categories_CategoryId",
                table: "Cars",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Categories_CategoryId",
                table: "Cars");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Cars_CategoryId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "DropOffLocation",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "PickUpLocation",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Cars");

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Rentals",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Identifier of the location (place of pick up and drop off)");

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Identifier of the location")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DropOff = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Where the car will be dropped off by the renter"),
                    PickUp = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Where the car will be picked up by the renter")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                },
                comment: "Information about where the car will be picked up and dropped off");

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
    }
}
