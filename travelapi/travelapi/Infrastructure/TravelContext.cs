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
        public DbSet<TypeRoom> RoomTypes { get; set; }
        public DbSet<Local> Locations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;database=traveldb;user=root;password=root",
                new MySqlServerVersion(new Version(10, 4, 28))); // versão do servidor MySQL
        }
    }
}
