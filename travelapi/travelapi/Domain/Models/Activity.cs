using System.ComponentModel.DataAnnotations;

namespace travelapi.Domain.Models
{
    public class Activity
    {
        [Key]
        public int? IdActivity { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
    }
}
