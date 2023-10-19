using System.ComponentModel.DataAnnotations;

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
        public ICollection<TypeRoom> TypesRoom { get; set; }
        public string Image { get; set; }


    }
}
