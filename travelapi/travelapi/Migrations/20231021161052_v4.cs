using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace travelapi.Migrations
{
    /// <inheritdoc />
    public partial class v4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomTypes_Hotels_HotelIdHotel",
                table: "RoomTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomTypes",
                table: "RoomTypes");

            migrationBuilder.RenameTable(
                name: "RoomTypes",
                newName: "TypeRooms");

            migrationBuilder.RenameIndex(
                name: "IX_RoomTypes_HotelIdHotel",
                table: "TypeRooms",
                newName: "IX_TypeRooms_HotelIdHotel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TypeRooms",
                table: "TypeRooms",
                column: "IdTypeRoom");

            migrationBuilder.AddForeignKey(
                name: "FK_TypeRooms_Hotels_HotelIdHotel",
                table: "TypeRooms",
                column: "HotelIdHotel",
                principalTable: "Hotels",
                principalColumn: "IdHotel");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TypeRooms_Hotels_HotelIdHotel",
                table: "TypeRooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TypeRooms",
                table: "TypeRooms");

            migrationBuilder.RenameTable(
                name: "TypeRooms",
                newName: "RoomTypes");

            migrationBuilder.RenameIndex(
                name: "IX_TypeRooms_HotelIdHotel",
                table: "RoomTypes",
                newName: "IX_RoomTypes_HotelIdHotel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomTypes",
                table: "RoomTypes",
                column: "IdTypeRoom");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomTypes_Hotels_HotelIdHotel",
                table: "RoomTypes",
                column: "HotelIdHotel",
                principalTable: "Hotels",
                principalColumn: "IdHotel");
        }
    }
}
