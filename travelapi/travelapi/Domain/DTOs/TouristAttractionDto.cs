using System.ComponentModel.DataAnnotations;

namespace travelapi.Domain.Dto
{
    public class TouristAttractionDto
    {
        [Key]
        public int? IdAttraction { get; set; }
        public string? Name { get; set; }
        public string Description { get; set; }
        public LocalDto? Location { get; set; }
        public float TicketPrice { get; set; }
        public string Image { get; set; }
    }
}
