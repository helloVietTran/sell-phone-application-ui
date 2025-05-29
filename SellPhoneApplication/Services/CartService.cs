using SellPhoneApplication.constant;
using SellPhoneApplication.DTOs;
using SellPhoneApplication.Models;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json; 
using System.Text.Json;

namespace SellPhoneApplication.Services
{
    public interface ICartService
    {
        Task AddToCartAsync(int productId, int quantity);
        Task<List<CartItem>> GetCartItemsAsync();
        Task RemoveFromCartAsync(int productId);

    }

    public class CartService : ICartService
    {
        private readonly HttpClient _httpClient;

        public CartService()
        {
            var handler = new AuthHttpClientHandler(new HttpClientHandler());
            _httpClient = new HttpClient(handler);
        }

        public async Task AddToCartAsync(int productId, int quantity)
        {
            var request = new
            {
                productId = productId,
                quantity = quantity
            };

            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(
                $"{AppConstants.BaseApiUrl}{AppConstants.CartEndpoint}", content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Thêm vào giỏ hàng thất bại");
            }
        }


        public async Task<List<CartItem>> GetCartItemsAsync()
        {
            var response = await _httpClient.GetAsync($"{AppConstants.BaseApiUrl}{AppConstants.CartEndpoint}");

            if (!response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Không thể lấy danh sách giỏ hàng");

            }

            var json = await response.Content.ReadAsStringAsync();
            var res = JsonSerializer.Deserialize<ApiResponse<List<CartItem>>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });


            return res.Result;
        }



        public async Task RemoveFromCartAsync(int productId)
        {
            var response = await _httpClient.DeleteAsync($"{AppConstants.BaseApiUrl}{AppConstants.CartEndpoint}/{productId}");

            if (!response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Xóa sản phẩm khỏi giỏ hàng thất bại");
                throw new Exception("Xóa sản phẩm khỏi giỏ hàng thất bại");
            }

            Debug.WriteLine($"Đã xóa sản phẩm có ID: {productId}");
        }
    }

}