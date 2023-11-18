using System.ComponentModel.DataAnnotations;
using travelapi.Domain.Dto;

namespace travelapi.Domain.Models
{
    public class Hotel
    {
        [Key]
        public int? IdHotel { get; set; }
        public string? Name { get; set; }
        public Local? Location { get; set; }
        public int? StarRating { get; set; }
        public decimal? PricePerNight { get; set; }
        public List<HotelImage> Images { get; set; }
    }
}
