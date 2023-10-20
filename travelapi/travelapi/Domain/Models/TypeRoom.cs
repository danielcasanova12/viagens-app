using System.ComponentModel.DataAnnotations;

namespace travelapi.Domain.Models
{
    public class TypeRoom
    {
        [Key]
        public int IdTypeRoom { get; set; }
        public string Name { get; set; }
        public decimal PriceDaily { get; set; }
    }
}
