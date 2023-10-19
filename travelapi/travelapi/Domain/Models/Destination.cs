using System.ComponentModel.DataAnnotations;

namespace travelapi.Domain.Models
{    public class Destination
    {
        [Key]
        public int? IdDestination { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<Activity>? Activities { get; set; } = new List<Activity>();
        public Local Location { get; set; }
        public List<TouristAttraction> Attractions { get; set; }
        public List<Hotel> Hotels { get; set; }
        public List<Restaurant> Restaurants { get; set; }


    }
}
