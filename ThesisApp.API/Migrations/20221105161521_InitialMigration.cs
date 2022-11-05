using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThesisApp.API.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeviceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DeviceUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceUsers_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeviceUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Name", "UserId" },
                values: new object[] { 1, "Room01", null });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Name", "UserId" },
                values: new object[] { 2, "Room02", null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DeviceId", "Email", "ImageUrl", "Name", "Password", "PhoneNumber" },
                values: new object[] { 1, null, "mjtcatanaoan@test.com", null, "Mel-john Catanaoan", null, "+44 (452) 886 09 12" });

            migrationBuilder.InsertData(
                table: "Devices",
                columns: new[] { "Id", "IsActive", "Name", "RoomId" },
                values: new object[,]
                {
                    { 1, false, "Device01", 1 },
                    { 2, false, "Device02", 1 },
                    { 3, false, "Device03", 2 },
                    { 4, false, "Device04", 2 }
                });

            migrationBuilder.InsertData(
                table: "DeviceUsers",
                columns: new[] { "Id", "DeviceId", "UserId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Devices_RoomId",
                table: "Devices",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceUsers_DeviceId",
                table: "DeviceUsers",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceUsers_UserId",
                table: "DeviceUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_UserId",
                table: "Rooms",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_DeviceId",
                table: "Users",
                column: "DeviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Rooms_RoomId",
                table: "Devices",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Rooms_RoomId",
                table: "Devices");

            migrationBuilder.DropTable(
                name: "DeviceUsers");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Devices");
        }
    }
}
