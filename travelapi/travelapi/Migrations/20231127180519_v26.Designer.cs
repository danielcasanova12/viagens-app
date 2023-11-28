﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using travelapi.Infrastructure;

#nullable disable

namespace travelapi.Migrations
{
    [DbContext(typeof(TravelContext))]
    [Migration("20231127180519_v26")]
    partial class v26
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("travelapi.Domain.Models.Activity", b =>
                {
                    b.Property<int?>("IdActivity")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<int?>("DestinationIdDestination")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("ReservationId")
                        .HasColumnType("int");

                    b.HasKey("IdActivity");

                    b.HasIndex("DestinationIdDestination");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("travelapi.Domain.Models.CarRental", b =>
                {
                    b.Property<int?>("IdCarRental")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Company")
                        .HasColumnType("longtext");

                    b.Property<string>("Image")
                        .HasColumnType("longtext");

                    b.Property<string>("Model")
                        .HasColumnType("longtext");

                    b.Property<int?>("PickupLocationIdLocal")
                        .HasColumnType("int");

                    b.Property<decimal?>("PricePerDay")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("IdCarRental");

                    b.HasIndex("PickupLocationIdLocal");

                    b.ToTable("CarRentals");
                });

            modelBuilder.Entity("travelapi.Domain.Models.Cost", b =>
                {
                    b.Property<int>("IdCosts")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<decimal>("Attractions")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("Extras")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("Restaurants")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("Transport")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<decimal>("accommodation")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("IdCosts");

                    b.ToTable("Costs");
                });

            modelBuilder.Entity("travelapi.Domain.Models.Destination", b =>
                {
                    b.Property<int?>("IdDestination")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<int>("LocationIdLocal")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("IdDestination");

                    b.HasIndex("LocationIdLocal");

                    b.ToTable("Destinations");
                });

            modelBuilder.Entity("travelapi.Domain.Models.Flight", b =>
                {
                    b.Property<int?>("IdFlight")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Airline")
                        .HasColumnType("longtext");

                    b.Property<int?>("ArrivalLocationIdLocal")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ArrivalTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("DepartureLocationIdLocal")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DepartureTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Image")
                        .HasColumnType("longtext");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("IdFlight");

                    b.HasIndex("ArrivalLocationIdLocal");

                    b.HasIndex("DepartureLocationIdLocal");

                    b.ToTable("Flights");
                });

            modelBuilder.Entity("travelapi.Domain.Models.Hotel", b =>
                {
                    b.Property<int?>("IdHotel")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("DestinationIdDestination")
                        .HasColumnType("int");

                    b.Property<int?>("LocationIdLocal")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<decimal?>("PricePerNight")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int?>("StarRating")
                        .HasColumnType("int");

                    b.HasKey("IdHotel");

                    b.HasIndex("DestinationIdDestination");

                    b.HasIndex("LocationIdLocal");

                    b.ToTable("Hotels");
                });

            modelBuilder.Entity("travelapi.Domain.Models.HotelImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("HotelId");

                    b.ToTable("HotelImages");
                });

            modelBuilder.Entity("travelapi.Domain.Models.Local", b =>
                {
                    b.Property<int>("IdLocal")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Adress")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("IdLocal");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("travelapi.Domain.Models.Reservation", b =>
                {
                    b.Property<int?>("IdReservation")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("CarRentalsIdCarRental")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CheckInDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("CheckOutDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool?>("Confirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("FlightsIdFlight")
                        .HasColumnType("int");

                    b.Property<int?>("ReservedHotelIdHotel")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("IdReservation");

                    b.HasIndex("CarRentalsIdCarRental");

                    b.HasIndex("FlightsIdFlight");

                    b.HasIndex("ReservedHotelIdHotel");

                    b.HasIndex("UserId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("travelapi.Domain.Models.Restaurant", b =>
                {
                    b.Property<int>("IdRestaurant")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<float>("Averageprice")
                        .HasColumnType("float");

                    b.Property<int?>("DestinationIdDestination")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("LocalitionIdLocal")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("IdRestaurant");

                    b.HasIndex("DestinationIdDestination");

                    b.HasIndex("LocalitionIdLocal");

                    b.ToTable("Restaurants");
                });

            modelBuilder.Entity("travelapi.Domain.Models.TouristAttraction", b =>
                {
                    b.Property<int?>("IdAttraction")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("DestinationIdDestination")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("LocationIdLocal")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<float>("TicketPrice")
                        .HasColumnType("float");

                    b.HasKey("IdAttraction");

                    b.HasIndex("DestinationIdDestination");

                    b.HasIndex("LocationIdLocal");

                    b.ToTable("TouristAttractions");
                });

            modelBuilder.Entity("travelapi.Domain.Models.User", b =>
                {
                    b.Property<int?>("IdUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("Image")
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("TypePermission")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Username")
                        .HasColumnType("longtext");

                    b.HasKey("IdUser");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("travelapi.Domain.Models.Activity", b =>
                {
                    b.HasOne("travelapi.Domain.Models.Destination", null)
                        .WithMany("Activities")
                        .HasForeignKey("DestinationIdDestination");
                });

            modelBuilder.Entity("travelapi.Domain.Models.CarRental", b =>
                {
                    b.HasOne("travelapi.Domain.Models.Local", "PickupLocation")
                        .WithMany()
                        .HasForeignKey("PickupLocationIdLocal");

                    b.Navigation("PickupLocation");
                });

            modelBuilder.Entity("travelapi.Domain.Models.Destination", b =>
                {
                    b.HasOne("travelapi.Domain.Models.Local", "Location")
                        .WithMany()
                        .HasForeignKey("LocationIdLocal")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");
                });

            modelBuilder.Entity("travelapi.Domain.Models.Flight", b =>
                {
                    b.HasOne("travelapi.Domain.Models.Local", "ArrivalLocation")
                        .WithMany()
                        .HasForeignKey("ArrivalLocationIdLocal");

                    b.HasOne("travelapi.Domain.Models.Local", "DepartureLocation")
                        .WithMany()
                        .HasForeignKey("DepartureLocationIdLocal");

                    b.Navigation("ArrivalLocation");

                    b.Navigation("DepartureLocation");
                });

            modelBuilder.Entity("travelapi.Domain.Models.Hotel", b =>
                {
                    b.HasOne("travelapi.Domain.Models.Destination", null)
                        .WithMany("Hotels")
                        .HasForeignKey("DestinationIdDestination");

                    b.HasOne("travelapi.Domain.Models.Local", "Location")
                        .WithMany()
                        .HasForeignKey("LocationIdLocal");

                    b.Navigation("Location");
                });

            modelBuilder.Entity("travelapi.Domain.Models.HotelImage", b =>
                {
                    b.HasOne("travelapi.Domain.Models.Hotel", null)
                        .WithMany("Images")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("travelapi.Domain.Models.Reservation", b =>
                {
                    b.HasOne("travelapi.Domain.Models.CarRental", "CarRentals")
                        .WithMany()
                        .HasForeignKey("CarRentalsIdCarRental");

                    b.HasOne("travelapi.Domain.Models.Flight", "Flights")
                        .WithMany()
                        .HasForeignKey("FlightsIdFlight");

                    b.HasOne("travelapi.Domain.Models.Hotel", "ReservedHotel")
                        .WithMany()
                        .HasForeignKey("ReservedHotelIdHotel");

                    b.HasOne("travelapi.Domain.Models.User", null)
                        .WithMany("Reservations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CarRentals");

                    b.Navigation("Flights");

                    b.Navigation("ReservedHotel");
                });

            modelBuilder.Entity("travelapi.Domain.Models.Restaurant", b =>
                {
                    b.HasOne("travelapi.Domain.Models.Destination", null)
                        .WithMany("Restaurants")
                        .HasForeignKey("DestinationIdDestination");

                    b.HasOne("travelapi.Domain.Models.Local", "Localition")
                        .WithMany()
                        .HasForeignKey("LocalitionIdLocal")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Localition");
                });

            modelBuilder.Entity("travelapi.Domain.Models.TouristAttraction", b =>
                {
                    b.HasOne("travelapi.Domain.Models.Destination", null)
                        .WithMany("Attractions")
                        .HasForeignKey("DestinationIdDestination");

                    b.HasOne("travelapi.Domain.Models.Local", "Location")
                        .WithMany()
                        .HasForeignKey("LocationIdLocal");

                    b.Navigation("Location");
                });

            modelBuilder.Entity("travelapi.Domain.Models.Destination", b =>
                {
                    b.Navigation("Activities");

                    b.Navigation("Attractions");

                    b.Navigation("Hotels");

                    b.Navigation("Restaurants");
                });

            modelBuilder.Entity("travelapi.Domain.Models.Hotel", b =>
                {
                    b.Navigation("Images");
                });

            modelBuilder.Entity("travelapi.Domain.Models.User", b =>
                {
                    b.Navigation("Reservations");
                });
#pragma warning restore 612, 618
        }
    }
}
