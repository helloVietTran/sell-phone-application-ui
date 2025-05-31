using SellPhoneApplication.constant;
using SellPhoneApplication.DTOs;
using SellPhoneApplication.Models;
using System.Diagnostics;
using System.Text.Json;

namespace SellPhoneApplication.Services
{
    public interface IFavouriteService
    {
        Task AddToFavoritesAsync(int productId);
        Task<List<FavoriteProduct>> GetFavoritesAsync();
        Task RemoveFavouriteAsync(int favouriteId);
    }
    public class FavouriteService : IFavouriteService
    {
        private readonly HttpClient _httpClient;

        public FavouriteService()
        {
            var handler = new AuthHttpClientHandler(new HttpClientHandler());
            _httpClient = new HttpClient(handler);
        }

        public async Task AddToFavoritesAsync(int productId)
        {
            var response = await _httpClient.PostAsync($"{AppConstants.BaseApiUrl}/favourite/{productId}", null);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to add product to favourite page");
            }
        }

        public async Task<List<FavoriteProduct>> GetFavoritesAsync()
        {


            var response = await _httpClient.GetAsync($"{AppConstants.BaseApiUrl}/favourite");

            if (!response.IsSuccessStatusCode)
            {
                Debug.WriteLine("[GetFavoritesAsync] Không thể lấy danh sách yêu thích. Status: " + response.StatusCode);
                return new List<FavoriteProduct>();
            }

            var json = await response.Content.ReadAsStringAsync();
            Debug.WriteLine($"[GetFavoritesAsync] Response JSON: {json}");

            var res = JsonSerializer.Deserialize<ApiResponse<List<FavoriteProduct>>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });


            return res?.Result ?? new List<FavoriteProduct>();
        }


        public async Task RemoveFavouriteAsync(int favouriteId)
        {

            try
            {
                var response = await _httpClient.DeleteAsync($"{AppConstants.BaseApiUrl}/favourite/{favouriteId}");

                if (!response.IsSuccessStatusCode)
                {
                    Debug.WriteLine($"[RemoveFavoriteAsync] Failed to remove favorite");
                    throw new Exception("Failed to remove favorite");
                }

                Debug.WriteLine("[RemoveFavoriteAsync] Successfully removed favorite item.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[RemoveFavoriteAsync] Exception occurred: {ex.Message}");
                throw;
            }
        }

    }
}
