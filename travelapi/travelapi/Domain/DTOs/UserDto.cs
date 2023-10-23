using System.ComponentModel.DataAnnotations;

namespace travelapi.Domain.Dto
{
    public class UserDto
    {
        [Key]
        public int? IdUser { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; }
        [Required]
        public List<ReservationDto> Reservations { get; set; } = new List<ReservationDto>();
        public string Image { get; set; }
        public string TypePermission { get; set; }
    }
}
