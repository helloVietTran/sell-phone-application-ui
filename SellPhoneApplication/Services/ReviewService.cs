
using SellPhoneApplication.constant;
using SellPhoneApplication.DTOs;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using SellPhoneApplication.Models;

namespace SellPhoneApplication.Services
{
    public interface IReviewService
    {
        Task PostReviewAsync(string productId, string reviewContent, int rating);
        Task<List<Review>> GetReviewAsync(string productId);
    }
    public class ReviewService : IReviewService
    {
        private readonly HttpClient _httpClient;

        public ReviewService()
        {
            var handler = new AuthHttpClientHandler(new HttpClientHandler());
            _httpClient = new HttpClient(handler);
        }
        public async Task PostReviewAsync(string productId, string reviewContent, int rating)
        {
            var request = new
            {
                productId = productId,
                content = reviewContent,
                rating = rating
            };

            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{AppConstants.BaseApiUrl}/reviews", content);

            if (!response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Gửi đánh giá thất bại");
                throw new Exception("Gửi đánh giá thất bại");
            }

            Debug.WriteLine("Gửi đánh giá thành công");
        }

        public async Task<List<Review>> GetReviewAsync(string productId)
        {
            var response = await _httpClient.GetAsync($"{AppConstants.BaseApiUrl}/reviews/{productId}");

            if (!response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Không thể lấy đánh giá");
                return new List<Review>();
            }

            var json = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonSerializer.Deserialize<ApiResponse<List<Review>>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return apiResponse?.Result ?? new List<Review>();

        }
    }
}
