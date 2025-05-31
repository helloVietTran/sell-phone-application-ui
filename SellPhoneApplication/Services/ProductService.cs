using SellPhoneApplication.constant;
using SellPhoneApplication.DTOs;
using SellPhoneApplication.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Web;

namespace SellPhoneApplication.Services
{
    public interface IProductService
    {
        Task<List<Phone>> FilterPhonesAsync(double maxPrice, IEnumerable<string> brands, IEnumerable<string> memories, string color, string sortByPrice);
      
    }
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService()
        {
            _httpClient = new HttpClient();
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
       
    }
}
