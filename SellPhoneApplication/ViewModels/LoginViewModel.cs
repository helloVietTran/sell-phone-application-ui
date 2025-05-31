using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SellPhoneApplication.Services;
using System.Text.RegularExpressions;
public partial class LoginViewModel : ObservableObject
{
    [ObservableProperty]
    private string email;

    [ObservableProperty]
    private string password;

    // Thuộc tính để hiển thị lỗi tổng quát
    [ObservableProperty]
    private string generalError;

    private readonly IAuthService _authService;

    public LoginViewModel(IAuthService authService)
    {
        _authService = authService;
    }

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
            var success = await _authService.LoginAsync(Email, Password);
            if (!success)
            {
                GeneralError = "Đăng nhập thất bại. Vui lòng kiểm tra lại thông tin!";
            }
          
        }
        catch (Exception ex)
        {
            GeneralError = "Đã xảy ra lỗi: " + ex.Message;
        }
    }
}