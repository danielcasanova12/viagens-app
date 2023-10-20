using System.ComponentModel.DataAnnotations;

namespace travelapi.Domain.Models
{
    public class Costs
    {
        [Key]
        public int IdCosts { get; set; }
        public decimal Transport { get; set; }
        public decimal accommodation {  get; set; }
        public decimal Attractions { get; set; }
        public decimal Restaurants { get; set; }
        public decimal Extras { get; set; }
    }
}
