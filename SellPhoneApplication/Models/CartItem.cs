﻿
namespace SellPhoneApplication.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string ImageUrl { get; set; }
        public int Ram { get; set; }
        public int Memory { get; set; }
    }
}
