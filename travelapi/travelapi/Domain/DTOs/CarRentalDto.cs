using System.ComponentModel.DataAnnotations;
using travelapi.Domain.Models;

namespace travelapi.Domain.DTOs
{
    public class CarRentalDto
    {
        [Key]
        public int? IdCarRental { get; set; }
        public string? Company { get; set; }
        public string? Model { get; set; }
        public decimal? PricePerDay { get; set; }
        public string? Image { get; set; }
        public Local? PickupLocation { get; set; }
    }
}
