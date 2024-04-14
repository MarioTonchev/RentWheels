using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentWheels.Infrastructure.Migrations
{
    public partial class AddedMessageEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IsActive",
                table: "Rentals",
                type: "nvarchar(max)",
                nullable: false,
                comment: "Is the rent still active or not",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "true",
                oldComment: "Is the rent still active or not");

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Identifier of the message")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false, comment: "Title of the message"),
                    Content = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false, comment: "Main content of the message"),
                    SenderId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Identifier of the sender"),
                    ReceiverId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Identifier of the receiver")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_AspNetUsers_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Messages_AspNetUsers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                },
                comment: "Message sent by a user to the owner of the car they have rented");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ReceiverId",
                table: "Messages",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderId",
                table: "Messages",
                column: "SenderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.AlterColumn<string>(
                name: "IsActive",
                table: "Rentals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "true",
                comment: "Is the rent still active or not",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Is the rent still active or not");
        }
    }
}
