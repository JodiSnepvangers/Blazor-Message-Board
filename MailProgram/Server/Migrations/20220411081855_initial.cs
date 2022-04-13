using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MailProgram.Server.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    MessageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.MessageId);
                    table.ForeignKey(
                        name: "FK_Messages_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Name" },
                values: new object[] { 1, "Lucy" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Name" },
                values: new object[] { 2, "Sarah" });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "MessageId", "Created", "Text", "Title", "UserId" },
                values: new object[] { 1, new DateTime(2022, 4, 11, 10, 18, 55, 427, DateTimeKind.Local).AddTicks(8525), "World", "Hello", 1 });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "MessageId", "Created", "Text", "Title", "UserId" },
                values: new object[] { 2, new DateTime(2022, 4, 11, 10, 18, 55, 427, DateTimeKind.Local).AddTicks(8559), "Test", "test message", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_UserId",
                table: "Messages",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
