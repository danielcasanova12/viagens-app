using System.ComponentModel.DataAnnotations;

namespace travelapi.Domain.Dto
{
    public class RestaurantDto
    {
        [Key]
        public int IdRestaurant { get; set; }
        public string Name { get; set; }
        public LocalDto Localition { get; set; }

        public string Image { get; set; }

        public float Averageprice { get; set; }
    }
}
