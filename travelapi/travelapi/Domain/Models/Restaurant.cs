﻿namespace travelapi.Domain.Models
{
    public class Restaurant
    {
        public int IdRestaurant { get; set; }
        public string Name { get; set; }
        public Local Localition { get; set; }

        public string ImageBase64 { get; set; }

        public float Averageprice { get; set; }
    }
}