
namespace SellPhoneApplication.DTOs
{
    public class ReviewRequest
    {
        public string Content { get; set; }
        public int Rating { get; set; }
        public int ProductId { get; set; }
    }
}
