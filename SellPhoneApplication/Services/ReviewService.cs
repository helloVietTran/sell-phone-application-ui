
using SellPhoneApplication.constant;
using SellPhoneApplication.DTOs;
using SellPhoneApplication.Models;
using System.Diagnostics;
using System.Text.Json;

namespace SellPhoneApplication.Services
{
    public interface IReviewService
    {
        Task PostReviewAsync(ReviewRequest request);
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

        public async Task PostReviewAsync(ReviewRequest request)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var json = JsonSerializer.Serialize(request, options);

            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{AppConstants.BaseApiUrl}{AppConstants.ReviewEndpoint}", content);

            if (!response.IsSuccessStatusCode)
            {
                Debug.WriteLine(json);
                throw new Exception("Gửi đánh giá thất bại");
            }
     
            Debug.WriteLine("Gửi đánh giá thành công");
        }

        public async Task<List<Review>> GetReviewAsync(string productId)
        {
            var response = await _httpClient.GetAsync($"{AppConstants.BaseApiUrl}{AppConstants.ReviewEndpoint}/{productId}");

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
