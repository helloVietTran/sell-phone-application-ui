using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SellPhoneApplication.Services;
using System.Text.RegularExpressions;

public partial class RegisterViewModel : ObservableObject
{
    // Các trường nhập dữ liệu
    [ObservableProperty]
    private string fullName;

    [ObservableProperty]
    private string email;

    [ObservableProperty]
    private string password;

    [ObservableProperty]
    private string confirmPassword;

    [ObservableProperty]
    private string generalError;


    private readonly IAuthService _authService;

    public RegisterViewModel(IAuthService authService)
    {
        _authService = authService;
    }


    [RelayCommand]
    async Task RegisterAsync()
    {

        GeneralError = "";


        if (string.IsNullOrWhiteSpace(FullName))
        {
            GeneralError = "Vui lòng nhập họ và tên!";
            return;
        }


        if (string.IsNullOrWhiteSpace(Email))
        {
            GeneralError = "Vui lòng nhập email!";
            return; // Dừng lại và chỉ hiển thị lỗi này
        }
        else if (!Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
        {
            GeneralError = "Email không hợp lệ!";
            return;
        }

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

        // Validate Confirm Password: phải có và trùng với Password.
        if (string.IsNullOrWhiteSpace(ConfirmPassword))
        {
            GeneralError = "Vui lòng xác nhận mật khẩu!";
            return;
        }
        else if (ConfirmPassword != Password)
        {
            GeneralError = "Mật khẩu xác nhận không khớp!";
            return;
        }

        try
        {
            var success = await _authService.RegisterAsync(Email, Password, FullName);
            if (!success)
            {
                GeneralError = "Đăng ký thất bại. Vui lòng kiểm tra lại thông tin!";
            }
        }
        catch (Exception ex)
        {
            GeneralError = "Đã xảy ra lỗi: " + ex.Message;
        }

    }
}