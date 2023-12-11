using Microsoft.Extensions.WebEncoders.Testing;
using travelapi.Domain.Models;

namespace travelapi.Infrastructure
{
    public class DatabaseInitializer
    {
        private readonly TravelContext _context;

        public DatabaseInitializer(TravelContext context)
        {
            _context = context;
        }

        public void AddDataIfNotExists()
        {
            // Add your data here. For example:
            var user = new User
            {
                Username = "Claudio",
                Email = "Claudio@gmail.com",
                Password = "Claudio@gmail.com",
                Reservations = new List<Reservation>(),
                Image = "https://imgs.search.brave.com/bPH3tSEsO7v8UEecanswP3SVkVf1YS-BXEyKnwEz6og/rs:fit:860:0:0/g:ce/aHR0cHM6Ly9pbWcz/LnN0b2NrZnJlc2gu/Y29tL2ZpbGVzL2Qv/ZGlzY292b2QvbS83/Mi8zMjM3Mzk0X3N0/b2NrLXBob3RvLWhh/cHB5LWZhdC1tYW4t/aW4tYS1ibHVlLXNo/aXJ0LmpwZw",
                TypePermission = "string"
            };
            _context.Users.Add(user);
            var hotel = new Hotel
            {
                Name = "Hotel horizonte",
                Location = new Local
                {
                    Name = "Matelandia",
                    Adress = "Centro",
                    City = "Matelandia",
                    State = "Parana",
                    Country = "Brasil",
                    Image = "string"
                },
                StarRating = 4,
                PricePerNight = 250,
                Images = new List<HotelImage>()
            };

            // Add the hotel to the database
            _context.Hotels.Add(hotel);
            
            var flight = new Flight
            {
                Airline = "Avião Kh-45",
                DepartureLocation = new Local
                {
                    Name = "Parana",
                    Adress = "Centro do parana",
                    City = "Cascavel",
                    State = "Parana",
                    Country = "Brasil",
                    Image = "Departure Image URL"
                },
                ArrivalLocation = new Local
                {
                    Name = "Macapa",
                    Adress = "Centro do macapa",
                    City = "Aravai",
                    State = "Macapa",
                    Country = "Brasil",
                    Image = "Arrival Image URL"
                },
                DepartureTime = DateTime.Parse("2023-12-11T23:35:44.266Z"),
                ArrivalTime = DateTime.Parse("2023-12-12T23:35:44.266Z"),
                Image = "https://imgs.search.brave.com/DUkwY_1pr-tRhtaciZqdWs7X7c1toZ7wRe3HZZyvSlA/rs:fit:860:0:0/g:ce/aHR0cHM6Ly92ZWph/LmFicmlsLmNvbS5i/ci93cC1jb250ZW50/L3VwbG9hZHMvMjAy/My8xMC81MzI0ODQ4/Mzc3MF9hNGQ2YjQz/MmE0X2stMS5qcGc_/cXVhbGl0eT05MCZz/dHJpcD1pbmZvJnc9/OTAw",
                Price = 1000
            };

            _context.Flights.Add(flight);
            var car = new CarRental
            {
                Company = "Gol",
                Model = "Gol",
                PricePerDay = 100,
                Image = "https://imgs.search.brave.com/c7Mlijytzp6nTp3xs-XcAcLHAIN2-NrJpULamluDxbo/rs:fit:860:0:0/g:ce/aHR0cHM6Ly9zMi1h/dXRvZXNwb3J0ZS5n/bGJpbWcuY29tL0kt/OHk5RWdyNjVQcUxj/YnNETjRrMjBTampu/OD0vMHgwOjE0MDB4/ODU2Lzk4NHgwL3Nt/YXJ0L2ZpbHRlcnM6/c3RyaXBfaWNjKCkv/aS5zMy5nbGJpbWcu/Y29tL3YxL0FVVEhf/Y2Y5ZDAzNWJmMjZi/NDY0NmIxMDViZDk1/OGYzMjA4OWQvaW50/ZXJuYWxfcGhvdG9z/L2JzLzIwMjIvVy9a/L0JrN1JTdFNBT2pL/WjZUY0ZVa3JBL2Rz/YzA2MTg2LmpwZw",
                PickupLocation = new Local
                {
                    Name = "Pickup Location Name",
                    Adress = "Pickup Location Address",
                    City = "Pickup City",
                    State = "Pickup State",
                    Country = "Pickup Country",
                    Image = "Pickup Location Image URL"
                }
            };

            _context.CarRentals.Add(car);

            _context.SaveChanges();
            Teste();
        }

        public void Teste()
        {
            var hotelimage1 = new HotelImage
            {
                HotelId = 1,
                ImageUrl = "https://imgs.search.brave.com/A-2N8wSspTuKUK3nITU1VT6716alUax2FbsWoZvjCUc/rs:fit:860:0:0/g:ce/aHR0cHM6Ly9tZWRp/YS5pc3RvY2twaG90/by5jb20vaWQvNDg3/MDQyMjc2L3Bob3Rv/L2hvdGVsLXNpZ24u/anBnP3M9NjEyeDYx/MiZ3PTAmaz0yMCZj/PURqRVZBb0ZuakIy/Y1d3WDI4Y3hTS1dr/eHNiemU3bzlqZ2tZ/cmh5Zm1xOUU9"
            };

            _context.HotelImages.Add(hotelimage1);
            var hotelimage2 = new HotelImage
            {
                HotelId = 1,
                ImageUrl = "https://imgs.search.brave.com/HJI0Qn0QvCBADgUbEhpU2pt2UIrWa-c0wI2igB17yPc/rs:fit:860:0:0/g:ce/aHR0cHM6Ly9tZWRp/YS5pc3RvY2twaG90/by5jb20vaWQvMTE5/OTI2MzM5L3Bob3Rv/L3Jlc29ydC1zd2lt/bWluZy1wb29sLmpw/Zz9zPTYxMng2MTIm/dz0wJms9MjAmYz05/UXR3SkMyYm9xM0dG/SGFlRHNLeXRGNC1D/YXZZS1F1eTFqQkQy/SVJmWUtjPQ"
            };

            _context.HotelImages.Add(hotelimage2);
            var hotelimage3 = new HotelImage
            {
                HotelId = 1,
                ImageUrl = "https://imgs.search.brave.com/dE6dKPeR2-4c6JEZSoKa4s7TCBE79YDWaCZ348oOtfA/rs:fit:860:0:0/g:ce/aHR0cHM6Ly9tZWRp/YS5pc3RvY2twaG90/by5jb20vaWQvMTA0/NzMxNzE3L3Bob3Rv/L2x1eHVyeS1yZXNv/cnQuanBnP3M9NjEy/eDYxMiZ3PTAmaz0y/MCZjPWNPRE1TUGJZ/eXJuMUZIYWtlMXhZ/ejlNOHIxNWlPZkd6/OUFvc3k5RGI3bUk9"
            };

            _context.HotelImages.Add(hotelimage3);
            var hotelimage4 = new HotelImage
            {
                HotelId = 1,
                ImageUrl = "https://imgs.search.brave.com/tvuYS1ojA168YBGw_syCk7K-35VJqfrIibBdFS7gZc8/rs:fit:860:0:0/g:ce/aHR0cHM6Ly9tZWRp/YS5pc3RvY2twaG90/by5jb20vaWQvODQz/ODIzNjU2L3Bob3Rv/L2hvdGVsLXJvb20u/anBnP3M9NjEyeDYx/MiZ3PTAmaz0yMCZj/PTgtWk5BNTJlNUds/UHV1UVBYcVpSZ3NU/TzlXUlp3WmdGdERv/dHlDNkNHSFk9"
            };

            _context.HotelImages.Add(hotelimage4);

            _context.SaveChanges();
        }
    }
}
