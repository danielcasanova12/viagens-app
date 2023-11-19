using System.ComponentModel.DataAnnotations;
using travelapi.Domain.Models;

namespace travelapi.Domain.DTOs
{
    public class FlightDto
    {
        [Key]
        public int? IdFlight { get; set; }
        public string? Airline { get; set; }
        public Local? DepartureLocation { get; set; }
        public Local? ArrivalLocation { get; set; }
        public DateTime? DepartureTime { get; set; }
        public DateTime? ArrivalTime { get; set; }
        public string? Image { get; set; }
        public decimal? Price { get; set; }
    }
}
