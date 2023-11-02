using System.ComponentModel.DataAnnotations;
using travelapi.Domain.Dto;

namespace travelapi.Domain.Dto
{
    public class HotelDto
    {
        [Key]
        public int? IdHotel { get; set; }
        public string? Name { get; set; }
        public LocalDto? Location { get; set; }
        public int? StarRating { get; set; }
        public decimal? PricePerNight { get; set; }
        public List<HotelImageDto> Images { get; set; }

    }
}
