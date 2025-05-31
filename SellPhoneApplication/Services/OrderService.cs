using SellPhoneApplication.constant;
using SellPhoneApplication.DTOs;
using SellPhoneApplication.Models;
using System.Diagnostics;
using System.Text.Json;

namespace SellPhoneApplication.Services
{
    public interface IOrderService
    {
        Task PlaceOrderAsync();
        Task<List<Order>> GetOrdersAsync();

        Task ConfirmOrderAsync(int orderId);
        Task CancelOrderAsync(int orderId);
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

        public async Task<List<Order>> GetOrdersAsync()
        {
            var response = await _httpClient.GetAsync($"{AppConstants.BaseApiUrl}{AppConstants.OrderEndpoint}");

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"Lấy danh sách đơn hàng thất bại: {error}");
                throw new Exception("Không thể lấy danh sách đơn hàng");
            }

            var json = await response.Content.ReadAsStringAsync();

            var res = JsonSerializer.Deserialize<ApiResponse<List<Order>>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return res.Result;
        }

        public async Task ConfirmOrderAsync(int orderId)
        {
            var response = await _httpClient.PostAsync($"{AppConstants.BaseApiUrl}/orders/{orderId}/status?accept=true", null);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Xác nhận đơn hàng thất bại");
        }

        public async Task CancelOrderAsync(int orderId)
        {
            var response = await _httpClient.PostAsync($"{AppConstants.BaseApiUrl}/orders/{orderId}/status?accept=false", null);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Hủy đơn hàng thất bại");
        }
    }
}
