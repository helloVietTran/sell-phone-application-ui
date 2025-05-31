using SellPhoneApplication.constant;
using System.Diagnostics;

namespace SellPhoneApplication.Services
{
     public interface IOrderService
    {
        Task PlaceOrderAsync();
    }

    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService()
        {
            var handler = new AuthHttpClientHandler(new HttpClientHandler());
            _httpClient = new HttpClient(handler);
        }

        public async Task PlaceOrderAsync()
        {
            try
            {
                var response = await _httpClient.PostAsync(
                    $"{AppConstants.BaseApiUrl}{AppConstants.OrderEndpoint}", null); // Không cần body

                if (!response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Đặt hàng thất bại");
                    throw new Exception("Đặt hàng thất bại");
                }

                Debug.WriteLine("Đặt hàng thành công");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Lỗi khi đặt hàng: {ex.Message}");
                throw;
            }
        }
    }
}
