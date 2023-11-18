using System.ComponentModel.DataAnnotations;

namespace travelapi.Domain.Models
{
    public class Flight
    {
        [Key]
        public int? IdFlight { get; set; }
        public string? Airline { get; set; }
        public Local? DepartureLocation { get; set; }
        public Local? ArrivalLocation { get; set; }
        public DateTime? DepartureTime { get; set; }
        public DateTime? ArrivalTime { get; set; }
        public decimal? Price { get; set; }
    }
}
