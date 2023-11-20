using Microsoft.EntityFrameworkCore;
using travelapi.Domain.Models;

namespace travelapi.Infrastructure
{
    public class TravelContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Destination> Destinations { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        
        public DbSet<HotelImage> HotelImages { get; set; }
        public DbSet<Local> Locations { get; set; }
        public DbSet<TouristAttraction> TouristAttractions { get; set; }
         public DbSet<CarRental> CarRentals { get; set; }
        public DbSet<Cost> Costs { get; set; }
        public DbSet<Flight> Flights { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                "server=172.18.0.3;port=3306;database=traveldb;user=root;password=root",
                new MySqlServerVersion(new Version(8, 2, 0)),
                mySqlOptions =>
                {
                    mySqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 10,
                        maxRetryDelay: TimeSpan.FromSeconds(10),
                        errorNumbersToAdd: null
                    );
                })
                .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));

        }

    }
}
