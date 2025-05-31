using SellPhoneApplication.constant;
using SellPhoneApplication.DTOs;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace SellPhoneApplication.Services
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(string email, string password);
        Task<bool> RegisterAsync(string email, string password, string fullName);
    }
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService()
        {
            _httpClient = new HttpClient();
        }
        public async Task<bool> LoginAsync(string email, string password)
        {
            try
            {
                var loginData = new
                {
                    email,
                    password
                };

                var content = new StringContent(
                    JsonSerializer.Serialize(loginData),
                    Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PostAsync($"{AppConstants.BaseApiUrl}{AppConstants.LoginEndpoint}", content);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var apiResponse = JsonSerializer.Deserialize<ApiResponse<AuthenticationResponse>>(json, options);

                    if (apiResponse != null && apiResponse.Result != null && !string.IsNullOrEmpty(apiResponse.Result.AccessToken))
                    {
                        var token = apiResponse.Result.AccessToken;
                        await SecureStorage.SetAsync("auth_token", token);

                        // Điều hướng sang HomePage
                        await Shell.Current.GoToAsync("//HomePage");
                        return true;
                    }
                }
                else
                {

                    Debug.WriteLine("Đăng nhập thất bại!");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Lỗi đăng nhập: " + ex.Message);
            }

            return false;
        }
        public async Task<bool> RegisterAsync(string email, string password, string fullName)
        {
            try
            {
                var registerData = new
                {
                    email,
                    password,
                    fullName
                };

                var content = new StringContent(
                    JsonSerializer.Serialize(registerData),
                    Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PostAsync($"{AppConstants.BaseApiUrl}{AppConstants.RegisterEndpoint}", content);

                if (response.IsSuccessStatusCode)
                {
                    // chuyển hướng tới trang đăng ký
                    await Shell.Current.GoToAsync("//LoginPage");
                    return true;
                }
                else
                {

                    Debug.WriteLine("Đăng ký thất bại: ");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Lỗi đăng ký: " + ex.Message);
            }

            return false;
        }
    }
}
