using System.ComponentModel.DataAnnotations;

namespace travelapi.Domain.Models
{
    public class CarRental
    {
        [Key]
        public int? IdCarRental { get; set; }
        public string? Company { get; set; }
        public string? Model { get; set; }
        public decimal? PricePerDay { get; set; }
        public Local? PickupLocation { get; set; }
    }
}
