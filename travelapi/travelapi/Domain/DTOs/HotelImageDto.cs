using System.ComponentModel.DataAnnotations;

namespace travelapi.Domain.Dto
{
    public class HotelImageDto
    {
        [Key]
        public int Id { get; set; }
        public int HotelId { get; set; }
        public string ImageUrl { get; set; }

    }
}
