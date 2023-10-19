using System.ComponentModel.DataAnnotations;

namespace travelapi.Domain.Models
{
    public class TouristAttraction
    {
        [Key]
        public int? IdAttraction { get; set; }
        public string? Name { get; set; }
        public string Description { get; set; }
        public Local? Location { get; set; }
        public float TicketPrice { get; set; }
        public string Image { get; set; }
    }
}
