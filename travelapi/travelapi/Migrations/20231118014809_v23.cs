using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace travelapi.Migrations
{
    /// <inheritdoc />
    public partial class v23 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarRental_Locations_PickupLocationIdLocal",
                table: "CarRental");

            migrationBuilder.DropForeignKey(
                name: "FK_Flight_Locations_ArrivalLocationIdLocal",
                table: "Flight");

            migrationBuilder.DropForeignKey(
                name: "FK_Flight_Locations_DepartureLocationIdLocal",
                table: "Flight");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_CarRental_CarRentalsIdCarRental",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Flight_FlightsIdFlight",
                table: "Reservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Flight",
                table: "Flight");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarRental",
                table: "CarRental");

            migrationBuilder.RenameTable(
                name: "Flight",
                newName: "Flights");

            migrationBuilder.RenameTable(
                name: "CarRental",
                newName: "CarRentals");

            migrationBuilder.RenameIndex(
                name: "IX_Flight_DepartureLocationIdLocal",
                table: "Flights",
                newName: "IX_Flights_DepartureLocationIdLocal");

            migrationBuilder.RenameIndex(
                name: "IX_Flight_ArrivalLocationIdLocal",
                table: "Flights",
                newName: "IX_Flights_ArrivalLocationIdLocal");

            migrationBuilder.RenameIndex(
                name: "IX_CarRental_PickupLocationIdLocal",
                table: "CarRentals",
                newName: "IX_CarRentals_PickupLocationIdLocal");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Flights",
                table: "Flights",
                column: "IdFlight");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarRentals",
                table: "CarRentals",
                column: "IdCarRental");

            migrationBuilder.AddForeignKey(
                name: "FK_CarRentals_Locations_PickupLocationIdLocal",
                table: "CarRentals",
                column: "PickupLocationIdLocal",
                principalTable: "Locations",
                principalColumn: "IdLocal");

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Locations_ArrivalLocationIdLocal",
                table: "Flights",
                column: "ArrivalLocationIdLocal",
                principalTable: "Locations",
                principalColumn: "IdLocal");

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Locations_DepartureLocationIdLocal",
                table: "Flights",
                column: "DepartureLocationIdLocal",
                principalTable: "Locations",
                principalColumn: "IdLocal");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_CarRentals_CarRentalsIdCarRental",
                table: "Reservations",
                column: "CarRentalsIdCarRental",
                principalTable: "CarRentals",
                principalColumn: "IdCarRental");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Flights_FlightsIdFlight",
                table: "Reservations",
                column: "FlightsIdFlight",
                principalTable: "Flights",
                principalColumn: "IdFlight");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarRentals_Locations_PickupLocationIdLocal",
                table: "CarRentals");

            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Locations_ArrivalLocationIdLocal",
                table: "Flights");

            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Locations_DepartureLocationIdLocal",
                table: "Flights");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_CarRentals_CarRentalsIdCarRental",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Flights_FlightsIdFlight",
                table: "Reservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Flights",
                table: "Flights");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarRentals",
                table: "CarRentals");

            migrationBuilder.RenameTable(
                name: "Flights",
                newName: "Flight");

            migrationBuilder.RenameTable(
                name: "CarRentals",
                newName: "CarRental");

            migrationBuilder.RenameIndex(
                name: "IX_Flights_DepartureLocationIdLocal",
                table: "Flight",
                newName: "IX_Flight_DepartureLocationIdLocal");

            migrationBuilder.RenameIndex(
                name: "IX_Flights_ArrivalLocationIdLocal",
                table: "Flight",
                newName: "IX_Flight_ArrivalLocationIdLocal");

            migrationBuilder.RenameIndex(
                name: "IX_CarRentals_PickupLocationIdLocal",
                table: "CarRental",
                newName: "IX_CarRental_PickupLocationIdLocal");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Flight",
                table: "Flight",
                column: "IdFlight");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarRental",
                table: "CarRental",
                column: "IdCarRental");

            migrationBuilder.AddForeignKey(
                name: "FK_CarRental_Locations_PickupLocationIdLocal",
                table: "CarRental",
                column: "PickupLocationIdLocal",
                principalTable: "Locations",
                principalColumn: "IdLocal");

            migrationBuilder.AddForeignKey(
                name: "FK_Flight_Locations_ArrivalLocationIdLocal",
                table: "Flight",
                column: "ArrivalLocationIdLocal",
                principalTable: "Locations",
                principalColumn: "IdLocal");

            migrationBuilder.AddForeignKey(
                name: "FK_Flight_Locations_DepartureLocationIdLocal",
                table: "Flight",
                column: "DepartureLocationIdLocal",
                principalTable: "Locations",
                principalColumn: "IdLocal");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_CarRental_CarRentalsIdCarRental",
                table: "Reservations",
                column: "CarRentalsIdCarRental",
                principalTable: "CarRental",
                principalColumn: "IdCarRental");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Flight_FlightsIdFlight",
                table: "Reservations",
                column: "FlightsIdFlight",
                principalTable: "Flight",
                principalColumn: "IdFlight");
        }
    }
}
