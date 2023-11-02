using System.ComponentModel.DataAnnotations;
using travelapi.Domain.Dto;

namespace travelapi.Domain.Models
{
    public class HotelImage
    {
        [Key]
        public int Id { get; set; }
        public int HotelId { get; set; } 
        public string ImageUrl { get; set; }
    }
}
