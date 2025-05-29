
namespace SellPhoneApplication.Models
{
    public class Phone
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Memory { get; set; }
        public int? Ram { get; set; }
        public string Color { get; set; }
        public double? Price { get; set; }
        public int? Stock { get; set; }
        public string ImageUrl { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }
    }
}
