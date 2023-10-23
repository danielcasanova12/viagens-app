using System.ComponentModel.DataAnnotations;

namespace travelapi.Domain.Dto
{    public class DestinationDto
    {
        [Key]
        public int? IdDestination { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<ActivityDto>? Activities { get; set; } = new List<ActivityDto>();
        public LocalDto Location { get; set; }
        public List<TouristAttractionDto> Attractions { get; set; }
        public List<HotelDto> Hotels { get; set; }
        public List<RestaurantDto> Restaurants { get; set; }


    }
}
