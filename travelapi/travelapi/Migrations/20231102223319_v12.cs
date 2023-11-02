using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace travelapi.Migrations
{
    /// <inheritdoc />
    public partial class v12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelImage_Hotels_HotelId",
                table: "HotelImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HotelImage",
                table: "HotelImage");

            migrationBuilder.RenameTable(
                name: "HotelImage",
                newName: "HotelImages");

            migrationBuilder.RenameIndex(
                name: "IX_HotelImage_HotelId",
                table: "HotelImages",
                newName: "IX_HotelImages_HotelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HotelImages",
                table: "HotelImages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelImages_Hotels_HotelId",
                table: "HotelImages",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "IdHotel");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelImages_Hotels_HotelId",
                table: "HotelImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HotelImages",
                table: "HotelImages");

            migrationBuilder.RenameTable(
                name: "HotelImages",
                newName: "HotelImage");

            migrationBuilder.RenameIndex(
                name: "IX_HotelImages_HotelId",
                table: "HotelImage",
                newName: "IX_HotelImage_HotelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HotelImage",
                table: "HotelImage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelImage_Hotels_HotelId",
                table: "HotelImage",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "IdHotel");
        }
    }
}
