using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace travelapi.Migrations
{
    /// <inheritdoc />
    public partial class v21 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CarRentalsIdCarRental",
                table: "Reservations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FlightsIdFlight",
                table: "Reservations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CarRental",
                columns: table => new
                {
                    IdCarRental = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Company = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Model = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PricePerDay = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    PickupLocationIdLocal = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarRental", x => x.IdCarRental);
                    table.ForeignKey(
                        name: "FK_CarRental_Locations_PickupLocationIdLocal",
                        column: x => x.PickupLocationIdLocal,
                        principalTable: "Locations",
                        principalColumn: "IdLocal");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Flight",
                columns: table => new
                {
                    IdFlight = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Airline = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DepartureLocationIdLocal = table.Column<int>(type: "int", nullable: true),
                    ArrivalLocationIdLocal = table.Column<int>(type: "int", nullable: true),
                    DepartureTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ArrivalTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(65,30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flight", x => x.IdFlight);
                    table.ForeignKey(
                        name: "FK_Flight_Locations_ArrivalLocationIdLocal",
                        column: x => x.ArrivalLocationIdLocal,
                        principalTable: "Locations",
                        principalColumn: "IdLocal");
                    table.ForeignKey(
                        name: "FK_Flight_Locations_DepartureLocationIdLocal",
                        column: x => x.DepartureLocationIdLocal,
                        principalTable: "Locations",
                        principalColumn: "IdLocal");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_CarRentalsIdCarRental",
                table: "Reservations",
                column: "CarRentalsIdCarRental");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_FlightsIdFlight",
                table: "Reservations",
                column: "FlightsIdFlight");

            migrationBuilder.CreateIndex(
                name: "IX_CarRental_PickupLocationIdLocal",
                table: "CarRental",
                column: "PickupLocationIdLocal");

            migrationBuilder.CreateIndex(
                name: "IX_Flight_ArrivalLocationIdLocal",
                table: "Flight",
                column: "ArrivalLocationIdLocal");

            migrationBuilder.CreateIndex(
                name: "IX_Flight_DepartureLocationIdLocal",
                table: "Flight",
                column: "DepartureLocationIdLocal");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_CarRental_CarRentalsIdCarRental",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Flight_FlightsIdFlight",
                table: "Reservations");

            migrationBuilder.DropTable(
                name: "CarRental");

            migrationBuilder.DropTable(
                name: "Flight");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_CarRentalsIdCarRental",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_FlightsIdFlight",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "CarRentalsIdCarRental",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "FlightsIdFlight",
                table: "Reservations");
        }
    }
}
