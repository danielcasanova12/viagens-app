using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace travelapi.Migrations
{
    /// <inheritdoc />
    public partial class v5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TouristAttraction_Destinations_DestinationIdDestination",
                table: "TouristAttraction");

            migrationBuilder.DropForeignKey(
                name: "FK_TouristAttraction_Locations_LocationIdLocal",
                table: "TouristAttraction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TouristAttraction",
                table: "TouristAttraction");

            migrationBuilder.RenameTable(
                name: "TouristAttraction",
                newName: "TouristAttractions");

            migrationBuilder.RenameColumn(
                name: "ImageBase64",
                table: "Restaurants",
                newName: "Image");

            migrationBuilder.RenameIndex(
                name: "IX_TouristAttraction_LocationIdLocal",
                table: "TouristAttractions",
                newName: "IX_TouristAttractions_LocationIdLocal");

            migrationBuilder.RenameIndex(
                name: "IX_TouristAttraction_DestinationIdDestination",
                table: "TouristAttractions",
                newName: "IX_TouristAttractions_DestinationIdDestination");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TouristAttractions",
                table: "TouristAttractions",
                column: "IdAttraction");

            migrationBuilder.AddForeignKey(
                name: "FK_TouristAttractions_Destinations_DestinationIdDestination",
                table: "TouristAttractions",
                column: "DestinationIdDestination",
                principalTable: "Destinations",
                principalColumn: "IdDestination");

            migrationBuilder.AddForeignKey(
                name: "FK_TouristAttractions_Locations_LocationIdLocal",
                table: "TouristAttractions",
                column: "LocationIdLocal",
                principalTable: "Locations",
                principalColumn: "IdLocal");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TouristAttractions_Destinations_DestinationIdDestination",
                table: "TouristAttractions");

            migrationBuilder.DropForeignKey(
                name: "FK_TouristAttractions_Locations_LocationIdLocal",
                table: "TouristAttractions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TouristAttractions",
                table: "TouristAttractions");

            migrationBuilder.RenameTable(
                name: "TouristAttractions",
                newName: "TouristAttraction");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Restaurants",
                newName: "ImageBase64");

            migrationBuilder.RenameIndex(
                name: "IX_TouristAttractions_LocationIdLocal",
                table: "TouristAttraction",
                newName: "IX_TouristAttraction_LocationIdLocal");

            migrationBuilder.RenameIndex(
                name: "IX_TouristAttractions_DestinationIdDestination",
                table: "TouristAttraction",
                newName: "IX_TouristAttraction_DestinationIdDestination");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TouristAttraction",
                table: "TouristAttraction",
                column: "IdAttraction");

            migrationBuilder.AddForeignKey(
                name: "FK_TouristAttraction_Destinations_DestinationIdDestination",
                table: "TouristAttraction",
                column: "DestinationIdDestination",
                principalTable: "Destinations",
                principalColumn: "IdDestination");

            migrationBuilder.AddForeignKey(
                name: "FK_TouristAttraction_Locations_LocationIdLocal",
                table: "TouristAttraction",
                column: "LocationIdLocal",
                principalTable: "Locations",
                principalColumn: "IdLocal");
        }
    }
}
