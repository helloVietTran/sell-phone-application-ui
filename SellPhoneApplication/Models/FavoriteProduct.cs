namespace SellPhoneApplication.Models
{
    public class FavoriteProduct
    {
        public int FavoriteId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public string ProductDescription { get; set; }
    }
}
