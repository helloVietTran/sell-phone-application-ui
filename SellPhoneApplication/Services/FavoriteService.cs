using SellPhoneApplication.Models;
using SellPhoneApplication.DTOs;
using SellPhoneApplication.constant;
using System.Text.Json;

namespace SellPhoneApplication.Services
{
    public interface IFavoriteService
    {
        Task AddToFavoritesAsync(int productId);
        Task<List<FavoriteProduct>> GetFavoritesAsync();
        Task RemoveFavoriteAsync(int favoriteId);
    }
    public class FavoriteService : IFavoriteService
    {
        private readonly HttpClient _httpClient;

        public FavoriteService(HttpClient httpClient)
        {
            var handler = new AuthHttpClientHandler(new HttpClientHandler());
            _httpClient = new HttpClient(handler);
        }

        public async Task AddToFavoritesAsync(int productId)
        {
            var response = await _httpClient.PostAsync($"{AppConstants.BaseApiUrl}/favorites/{productId}", null);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to add product to favorites");
            }
        }

        public async Task<List<FavoriteProduct>> GetFavoritesAsync()
        {
            var response = await _httpClient.GetAsync($"{AppConstants.BaseApiUrl}/favorites");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var apiResponse = JsonSerializer.Deserialize<ApiResponse<List<FavoriteProduct>>>(json, options);

            return apiResponse?.Result ?? new List<FavoriteProduct>();
        }

        public async Task RemoveFavoriteAsync(int favoriteId)
        {
            var response = await _httpClient.DeleteAsync($"{AppConstants.BaseApiUrl}/favorites/{favoriteId}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to remove favorite");
            }
        }
    }
}
