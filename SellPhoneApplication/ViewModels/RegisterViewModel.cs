using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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

    // Các thông báo lỗi tương ứng
    [ObservableProperty]
    private string fullNameError;

    [ObservableProperty]
    private string emailError;

    [ObservableProperty]
    private string passwordError;

    [ObservableProperty]
    private string confirmPasswordError;

    // Command xử lý đăng ký
    [RelayCommand]
    async Task RegisterAsync()
    {
        bool isValid = true;

        // Reset các thông báo lỗi
        FullNameError = "";
        EmailError = "";
        PasswordError = "";
        ConfirmPasswordError = "";

        // Validate Full Name: không được rỗng.
        if (string.IsNullOrWhiteSpace(FullName))
        {
            FullNameError = "Vui lòng nhập họ và tên!";
            isValid = false;
        }

        // Validate Email: phải có và đúng định dạng email.
        if (string.IsNullOrWhiteSpace(Email))
        {
            EmailError = "Vui lòng nhập email!";
            isValid = false;
        }
        else if (!Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
        {
            EmailError = "Email không hợp lệ!";
            isValid = false;
        }

        // Validate Password: tối thiểu 6 ký tự và chỉ chứa chữ và số.
        if (string.IsNullOrWhiteSpace(Password))
        {
            PasswordError = "Vui lòng nhập mật khẩu!";
            isValid = false;
        }
        else if (Password.Length < 6)
        {
            PasswordError = "Mật khẩu tối thiểu 6 ký tự!";
            isValid = false;
        }
        else if (!Regex.IsMatch(Password, @"^[A-Za-z0-9]+$"))
        {
            PasswordError = "Mật khẩu không được chứa ký tự đặc biệt!";
            isValid = false;
        }

        // Validate Confirm Password: phải có và trùng với Password.
        if (string.IsNullOrWhiteSpace(ConfirmPassword))
        {
            ConfirmPasswordError = "Vui lòng xác nhận mật khẩu!";
            isValid = false;
        }
        else if (ConfirmPassword != Password)
        {
            ConfirmPasswordError = "Mật khẩu xác nhận không khớp!";
            isValid = false;
        }

        if (!isValid)
            return;

        // Nếu mọi thứ hợp lệ, xử lý đăng ký (ví dụ gọi API, chuyển trang,...)
        await Task.Delay(1000);
        await Shell.Current.GoToAsync("//HomePage");
    }
}
