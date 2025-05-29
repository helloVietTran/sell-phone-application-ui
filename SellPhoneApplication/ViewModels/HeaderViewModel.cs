using CommunityToolkit.Mvvm.ComponentModel;
using SellPhoneApplication.constant;
using SellPhoneApplication.DTOs;
using SellPhoneApplication.Models;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;


public partial class HeaderViewModel : ObservableObject
{
    [ObservableProperty]
    private bool isAdmin;

    public HeaderViewModel()
    {
        _ = LoadUserInfoAsync();
    }

    private async Task LoadUserInfoAsync()
    {
        try
        {
            var token = await SecureStorage.GetAsync("auth_token");

            if (string.IsNullOrEmpty(token))
                return;

            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.GetAsync($"{AppConstants.BaseApiUrl}{AppConstants.MyInfoEndpoint}");
            if (!response.IsSuccessStatusCode)
                return;

            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var apiResponse = JsonSerializer.Deserialize<ApiResponse<User>>(json, options);

            if (apiResponse?.Result?.Role == "ADMIN")
            {
                IsAdmin = true;
            }
        }
        catch
        {
            Debug.WriteLine("Lỗi khi tải người dùng");
        }
    }
}
