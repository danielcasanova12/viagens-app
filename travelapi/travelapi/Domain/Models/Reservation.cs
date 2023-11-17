using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace travelapi.Domain.Models
{
    public class Reservation
    {
        [Key]
        public int? IdReservation { get; set; }
        public int UserId { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public Hotel? ReservedHotel { get; set; }
        //public List<Activity>? ReservedActivities { get; set; } = new List<Activity>();
    }
}
