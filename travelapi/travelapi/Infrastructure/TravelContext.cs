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

        public bool HasData()
        {
            return Users.Any() || Reservations.Any() || Hotels.Any() || CarRentals.Any() || Flights.Any();
        }

        public void AddDataIfNotExists()
        {
            if (!HasData())
            {
                // Adicione seus dados aqui. Por exemplo:
                var user = new User
                {
                    Username = "string@gmail.com",
                    Email = "string@gmail.com",
                    Password = "string@gmail.com",
                    Reservations = new List<Reservation>(),
                    Image = "string",
                    TypePermission = "string"
                };
                Users.Add(user);
                var hotel = new Hotel
                {
                    Name = "string",
                    Location = new Local
                    {
                        Name = "string",
                        Adress = "string",
                        City = "string",
                        State = "string",
                        Country = "string",
                        Image = "string"
                    },
                    StarRating = 0,
                    PricePerNight = 250,
                    Images = new List<HotelImage>()
                };

                // Adicione o hotel ao banco de dados
                Hotels.Add(hotel);

                // Crie uma nova imagem do hotel
                //var hotelImage = new HotelImage
                //{
                //    HotelId = 1,
                //    ImageUrl = "https://imgs.search.brave.com/dE6dKPeR2-4c6JEZSoKa4s7TCBE79YDWaCZ348oOtfA/rs:fit:860:0:0/g:ce/aHR0cHM6Ly9tZWRp/YS5pc3RvY2twaG90/by5jb20vaWQvMTA0/NzMxNzE3L3Bob3Rv/L2x1eHVyeS1yZXNv/cnQuanBnP3M9NjEy/eDYxMiZ3PTAmaz0y/MCZjPWNPRE1TUGJZ/eXJuMUZIYWtlMXhZ/ejlNOHIxNWlPZkd6/OUFvc3k5RGI3bUk9"
                //};

                //// Adicione a imagem do hotel ao banco de dados
                //HotelImages.Add(hotelImage);

                // Crie um novo voo
                var flight = new Flight
                {
                    Airline = "string",
                    DepartureLocation = new Local
                    {
                        IdLocal = 0,
                        Name = "string",
                        Adress = "string",
                        City = "string",
                        State = "string",
                        Country = "string",
                        Image = "string"
                    },
                    ArrivalLocation = new Local
                    {
                        IdLocal = 0,
                        Name = "string",
                        Adress = "string",
                        City = "string",
                        State = "string",
                        Country = "string",
                        Image = "string"
                    },
                    DepartureTime = DateTime.Parse("2023-11-26T13:05:27.239Z"),
                    ArrivalTime = DateTime.Parse("2023-11-26T13:05:27.239Z"),
                    Image = "https://imgs.search.brave.com/Kuwz2ZtMxJUfCbLDmB1BhSjYSzL9zTapvew_k9pFi9U/rs:fit:860:0:0/g:ce/aHR0cHM6Ly9zdGF0/aWMucG9kZXIzNjAu/Y29tLmJyLzIwMjEv/MTEvZ29sLWF2aWFv/LTg2OHg2NDQtMS04/NDh4NDc3LnBuZw",
                    Price = 1500
                };

                // Adicione o voo ao banco de dados
                Flights.Add(flight);

                // Crie um novo aluguel de carro
                var carRental = new CarRental
                {
                    Company = "string",
                    Model = "string",
                    PricePerDay = 159,
                    Image = "https://imgs.search.brave.com/wqXxsTI8gGUbxXwz3Xcsdmw6lIW9GsMLJhjlbtv_T14/rs:fit:860:0:0/g:ce/aHR0cHM6Ly9zMi1h/dXRvZXNwb3J0ZS5n/bGJpbWcuY29tL2Uw/aEw1N2ZSSmlUTE52/Ykx4U3d5eUtLZWpR/bz0vNTQweDMwNC90/b3Avc21hcnQvaHR0/cHM6Ly9pLnMzLmds/YmltZy5jb20vdjEv/QVVUSF9jZjlkMDM1/YmYyNmI0NjQ2YjEw/NWJkOTU4ZjMyMDg5/ZC9pbnRlcm5hbF9w/aG90b3MvYnMvMjAy/Mi9SL00vSzdrb3M1/UUdhZnh6U0dFa0ZQ/N1EvZHNjMDYxODEu/anBn",
                    PickupLocation = new Local
                    {
                        IdLocal = 0,
                        Name = "string",
                        Adress = "string",
                        City = "string",
                        State = "string",
                        Country = "string",
                        Image = "asd"
                    }
                };

                // Adicione o aluguel de carro ao banco de dados
                CarRentals.Add(carRental);

                SaveChanges();
            }

        }


    }

}
