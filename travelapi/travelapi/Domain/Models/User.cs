using System.ComponentModel.DataAnnotations;

namespace travelapi.Domain.Models
{
    public class User
    {
        [Key]
        public int? IdUser { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; }
        public List<Reservation>? Reservations { get; set; } = new List<Reservation>();
        public string Image { get; set; }
        public string TypePermission { get; set; }
    }
}
