using System.ComponentModel.DataAnnotations;

namespace travelapi.Domain.Dto
{
    public class TypeRoomDto
    {
        [Key]
        public int IdTypeRoom { get; set; }
        public string Name { get; set; }
        public decimal PriceDaily { get; set; }
    }
}
