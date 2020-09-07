﻿namespace KFC.Models {
    public class ProductByCategory {
        public int id { get; set; }
        public string name { get; set; }
        public string detail { get; set; }
        public string imageUrl { get; set; }
        public double price { get; set; }
        public bool isPopularProduct { get; set; }
        public int categoryId { get; set; }
        public object imageArray { get; set; }
    }
}