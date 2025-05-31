
namespace SellPhoneApplication.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string Email { get; set; }
        public string UserFullName { get; set; }
        public double TotalPrice { get; set; }
        public string Status { get; set; }
        public string CreatedAt { get; set; }
    }
}
