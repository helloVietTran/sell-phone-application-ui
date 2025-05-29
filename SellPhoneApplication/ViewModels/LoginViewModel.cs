using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SellPhoneApplication.constant;
using SellPhoneApplication.DTOs;
using System.Text;
using System.Text.RegularExpressions;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;


public partial class LoginViewModel : ObservableObject
{
    [ObservableProperty]
    private string email;

    [ObservableProperty]
    private string password;

    // Thuộc tính để hiển thị lỗi tổng quát
    [ObservableProperty]
    private string generalError;



    [RelayCommand]
    async Task LoginAsync()
    {
        // Reset lỗi tổng quát khi bắt đầu kiểm tra
        GeneralError = "";

        if (string.IsNullOrWhiteSpace(Email))
        {
            GeneralError = "Vui lòng nhập email!";
            return;
        }
        else if (!Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
        {
            GeneralError = "Email không hợp lệ!";
            return;
        }

        // Validate mật khẩu: không rỗng, tối thiểu 6 ký tự và không chứa ký tự đặc biệt.
        if (string.IsNullOrWhiteSpace(Password))
        {
            GeneralError = "Vui lòng nhập mật khẩu!";
            return;
        }
        else if (Password.Length < 6)
        {
            GeneralError = "Mật khẩu tối thiểu 6 ký tự!";
            return;
        }
        else if (!Regex.IsMatch(Password, @"^[A-Za-z0-9]+$"))
        {
            GeneralError = "Mật khẩu không được chứa ký tự đặc biệt!";
            return;
        }


        try
        {
            var httpClient = new HttpClient();
            var loginData = new
            {
                email = Email,
                password = Password
            };

            var content = new StringContent(
                JsonSerializer.Serialize(loginData),
                Encoding.UTF8,
                "application/json");

            var response = await httpClient.PostAsync($"{AppConstants.BaseApiUrl}{AppConstants.LoginEndpoint}", content);
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

                    await Shell.Current.GoToAsync("//HomePage");
                }
                else
                {
                    GeneralError = "Không nhận được token hợp lệ từ server.";
                }
            }
            else
            {
                var errorBody = await response.Content.ReadAsStringAsync();
                GeneralError = "Đăng nhập thất bại: " + errorBody;
            }
        }
        catch (Exception ex)
        {
            GeneralError = "Đã xảy ra lỗi: " + ex.Message;
        }
    }
}