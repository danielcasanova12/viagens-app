using System.ComponentModel.DataAnnotations;

namespace travelapi.Domain.Dto
{
    public class CostDto
    {
        [Key]
        public int IdCosts { get; set; }
        public int UserId { get; set; }
        public decimal Transport { get; set; }
        public decimal accommodation {  get; set; }
        public decimal Attractions { get; set; }
        public decimal Restaurants { get; set; }
        public decimal Extras { get; set; }
    }
}
