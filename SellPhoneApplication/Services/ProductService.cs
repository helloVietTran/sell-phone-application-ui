using SellPhoneApplication.constant;
using SellPhoneApplication.DTOs;
using SellPhoneApplication.Models;
using System.Diagnostics;
using System.Text.Json;
using System.Web;

namespace SellPhoneApplication.Services
{
    public interface IProductService
    {
        Task<List<Phone>> FilterPhonesAsync(double maxPrice, IEnumerable<string> brands, IEnumerable<string> memories, string color, string sortByPrice);
        Task<List<Phone>> SearchPhonesAsync(string searchValue);

        Task<bool> DeleteProductAsync(int productId);

    }
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService()
        {
            var handler = new AuthHttpClientHandler(new HttpClientHandler());
            _httpClient = new HttpClient(handler);
        }

        public async Task<List<Phone>> FilterPhonesAsync(double maxPrice, IEnumerable<string> brands, IEnumerable<string> memories, string color, string sortByPrice)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);

            if (maxPrice > 0)
                query["maxPrice"] = maxPrice.ToString();

            if (brands != null)
            {
                foreach (var brand in brands)
                    query.Add("brands", brand);
            }

            if (memories != null)
            {
                foreach (var mem in memories)
                    query.Add("memories", mem);
            }

            if (!string.IsNullOrEmpty(color))
                query["color"] = color;

            if (!string.IsNullOrEmpty(sortByPrice))
                query["sortByPrice"] = sortByPrice;

            try
            {
                var response = await _httpClient.GetAsync($"{AppConstants.BaseApiUrl}{AppConstants.FilterEndpoint}?{query}");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var res = JsonSerializer.Deserialize<ApiResponse<List<Phone>>>(json, options);
                    Debug.WriteLine("Gọi API lấy điện thoại thành công: " + res);
                    return res?.Result ?? new List<Phone>();
                }
                else
                {
                    Debug.WriteLine("Lỗi khi gọi API lọc sản phẩm");
                    return new List<Phone>();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Lỗi API: " + ex.Message);
                return new List<Phone>();
            }
        }
        public async Task<List<Phone>> SearchPhonesAsync(string searchValue)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);

            if (!string.IsNullOrEmpty(searchValue))
            {
                query["searchValue"] = searchValue;
            }

            try
            {
                var response = await _httpClient.GetAsync($"{AppConstants.BaseApiUrl}/products/search?{query}");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var res = JsonSerializer.Deserialize<ApiResponse<List<Phone>>>(json, options);
                    Debug.WriteLine("Gọi API tìm kiếm thành công: " + res);
                    return res?.Result ?? new List<Phone>();
                }
                else
                {
                    Debug.WriteLine("Lỗi khi gọi API tìm kiếm");
                    return new List<Phone>();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Lỗi API tìm kiếm: " + ex.Message);
                return new List<Phone>();
            }
        }

        public async Task<bool> DeleteProductAsync(int productId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{AppConstants.BaseApiUrl}/products/{productId}");

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine($"Xóa sản phẩm {productId} thành công.");
                    return true;
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine($"Lỗi khi xóa sản phẩm: {error}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Lỗi khi gọi API xóa sản phẩm: {ex.Message}");
                return false;
            }
        }
    }
}
