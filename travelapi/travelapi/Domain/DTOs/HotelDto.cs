using System.ComponentModel.DataAnnotations;

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
        public ICollection<TypeRoomDto> TypesRoom { get; set; }
        public string Image { get; set; }


    }
}
