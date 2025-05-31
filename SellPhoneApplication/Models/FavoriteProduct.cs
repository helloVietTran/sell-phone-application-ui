namespace SellPhoneApplication.Models
{
    public class FavoriteProduct
    {
        public int FavouriteId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public string ProductDescription { get; set; }

        public double Price { get; set; }
    }
}
