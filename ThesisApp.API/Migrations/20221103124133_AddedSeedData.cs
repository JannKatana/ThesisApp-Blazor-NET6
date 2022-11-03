using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThesisApp.API.Migrations
{
    public partial class AddedSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Room01" });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Room02" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "ImageUrl", "Name", "Password", "PhoneNumber" },
                values: new object[] { 1, "mjtcatanaoan@test.com", null, "Mel-john Catanaoan", null, "+44 (452) 886 09 12" });

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Devices",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Devices",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Devices",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Devices",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
