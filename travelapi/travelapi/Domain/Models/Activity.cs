﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace travelapi.Domain.Models
{
    public class Activity
    {
        [Key]
        public int? IdActivity { get; set; }
        public string? Name { get; set; }
        public int ReservationId { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
    }
}
