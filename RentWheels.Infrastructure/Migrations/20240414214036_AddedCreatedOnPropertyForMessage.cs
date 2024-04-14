using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentWheels.Infrastructure.Migrations
{
    public partial class AddedCreatedOnPropertyForMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Messages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "When the message was sent");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Messages");
        }
    }
}
