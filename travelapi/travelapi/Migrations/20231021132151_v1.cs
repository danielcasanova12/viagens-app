using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace travelapi.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    IdLocal = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Adress = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    City = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    State = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Country = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Image = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.IdLocal);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Image = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TypePermission = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.IdUser);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Destinations",
                columns: table => new
                {
                    IdDestination = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LocationIdLocal = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destinations", x => x.IdDestination);
                    table.ForeignKey(
                        name: "FK_Destinations_Locations_LocationIdLocal",
                        column: x => x.LocationIdLocal,
                        principalTable: "Locations",
                        principalColumn: "IdLocal",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Hotels",
                columns: table => new
                {
                    IdHotel = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LocationIdLocal = table.Column<int>(type: "int", nullable: true),
                    StarRating = table.Column<int>(type: "int", nullable: true),
                    PricePerNight = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Image = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DestinationIdDestination = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.IdHotel);
                    table.ForeignKey(
                        name: "FK_Hotels_Destinations_DestinationIdDestination",
                        column: x => x.DestinationIdDestination,
                        principalTable: "Destinations",
                        principalColumn: "IdDestination");
                    table.ForeignKey(
                        name: "FK_Hotels_Locations_LocationIdLocal",
                        column: x => x.LocationIdLocal,
                        principalTable: "Locations",
                        principalColumn: "IdLocal");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Restaurants",
                columns: table => new
                {
                    IdRestaurant = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LocalitionIdLocal = table.Column<int>(type: "int", nullable: false),
                    ImageBase64 = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Averageprice = table.Column<float>(type: "float", nullable: false),
                    DestinationIdDestination = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurants", x => x.IdRestaurant);
                    table.ForeignKey(
                        name: "FK_Restaurants_Destinations_DestinationIdDestination",
                        column: x => x.DestinationIdDestination,
                        principalTable: "Destinations",
                        principalColumn: "IdDestination");
                    table.ForeignKey(
                        name: "FK_Restaurants_Locations_LocalitionIdLocal",
                        column: x => x.LocalitionIdLocal,
                        principalTable: "Locations",
                        principalColumn: "IdLocal",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TouristAttraction",
                columns: table => new
                {
                    IdAttraction = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LocationIdLocal = table.Column<int>(type: "int", nullable: true),
                    TicketPrice = table.Column<float>(type: "float", nullable: false),
                    Image = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DestinationIdDestination = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TouristAttraction", x => x.IdAttraction);
                    table.ForeignKey(
                        name: "FK_TouristAttraction_Destinations_DestinationIdDestination",
                        column: x => x.DestinationIdDestination,
                        principalTable: "Destinations",
                        principalColumn: "IdDestination");
                    table.ForeignKey(
                        name: "FK_TouristAttraction_Locations_LocationIdLocal",
                        column: x => x.LocationIdLocal,
                        principalTable: "Locations",
                        principalColumn: "IdLocal");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    IdReservation = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CheckInDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CheckOutDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ReservedHotelIdHotel = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.IdReservation);
                    table.ForeignKey(
                        name: "FK_Reservations_Hotels_ReservedHotelIdHotel",
                        column: x => x.ReservedHotelIdHotel,
                        principalTable: "Hotels",
                        principalColumn: "IdHotel");
                    table.ForeignKey(
                        name: "FK_Reservations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RoomTypes",
                columns: table => new
                {
                    IdTypeRoom = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PriceDaily = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    HotelIdHotel = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomTypes", x => x.IdTypeRoom);
                    table.ForeignKey(
                        name: "FK_RoomTypes_Hotels_HotelIdHotel",
                        column: x => x.HotelIdHotel,
                        principalTable: "Hotels",
                        principalColumn: "IdHotel");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    IdActivity = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ReservationId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    DestinationIdDestination = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.IdActivity);
                    table.ForeignKey(
                        name: "FK_Activities_Destinations_DestinationIdDestination",
                        column: x => x.DestinationIdDestination,
                        principalTable: "Destinations",
                        principalColumn: "IdDestination");
                    table.ForeignKey(
                        name: "FK_Activities_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservations",
                        principalColumn: "IdReservation",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_DestinationIdDestination",
                table: "Activities",
                column: "DestinationIdDestination");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_ReservationId",
                table: "Activities",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_Destinations_LocationIdLocal",
                table: "Destinations",
                column: "LocationIdLocal");

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_DestinationIdDestination",
                table: "Hotels",
                column: "DestinationIdDestination");

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_LocationIdLocal",
                table: "Hotels",
                column: "LocationIdLocal");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ReservedHotelIdHotel",
                table: "Reservations",
                column: "ReservedHotelIdHotel");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_UserId",
                table: "Reservations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_DestinationIdDestination",
                table: "Restaurants",
                column: "DestinationIdDestination");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_LocalitionIdLocal",
                table: "Restaurants",
                column: "LocalitionIdLocal");

            migrationBuilder.CreateIndex(
                name: "IX_RoomTypes_HotelIdHotel",
                table: "RoomTypes",
                column: "HotelIdHotel");

            migrationBuilder.CreateIndex(
                name: "IX_TouristAttraction_DestinationIdDestination",
                table: "TouristAttraction",
                column: "DestinationIdDestination");

            migrationBuilder.CreateIndex(
                name: "IX_TouristAttraction_LocationIdLocal",
                table: "TouristAttraction",
                column: "LocationIdLocal");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "Restaurants");

            migrationBuilder.DropTable(
                name: "RoomTypes");

            migrationBuilder.DropTable(
                name: "TouristAttraction");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Hotels");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Destinations");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
