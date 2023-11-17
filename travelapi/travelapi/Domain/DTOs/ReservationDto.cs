using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace travelapi.Domain.Dto
{
    public class ReservationDto
    {
        [Key]
        public int? IdReservation { get; set; }
        public int UserId { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public HotelDto? ReservedHotel { get; set; }


        
        //public List<Activity>? ReservedActivities { get; set; } = new List<Activity>();
    }
}
