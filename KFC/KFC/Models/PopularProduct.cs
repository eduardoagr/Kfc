﻿namespace KFC.Models {
    public class PopularProduct {
        public int id { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public string imageUrl { get; set; }

        public string FullImageUrl => $"{AppSettings.APIURL}{imageUrl}";
    }
}
