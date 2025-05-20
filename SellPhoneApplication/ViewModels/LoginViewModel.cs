
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text.RegularExpressions;
public partial class LoginViewModel : ObservableObject
{
    // tự động tạo NotifyPropertyChanged
    [ObservableProperty]
    private string email;

    [ObservableProperty]
    private string password;

    // chữa lỗi
    [ObservableProperty]
    private string emailError;

    [ObservableProperty]
    private string passwordError;

    // Tạo command xử lý đăng nhập
    [RelayCommand]
    async Task LoginAsync()
    {
        bool isValid = true;

        EmailError = "";
        PasswordError = "";

        if (string.IsNullOrWhiteSpace(Email))
        {
            EmailError = "Vui lòng nhập email!";
            isValid = false;
        }
        else
        {
            if (!Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                EmailError = "Email không hợp lệ!";
                isValid = false;
            }
        }

        // Validate mật khẩu: không rỗng, tối thiểu 6 ký tự và không chứa ký tự đặc biệt.
        if (string.IsNullOrWhiteSpace(Password))
        {
            PasswordError = "Vui lòng nhập mật khẩu!";
            isValid = false;
        }
        else
        {
            if (Password.Length < 6)
            {
                PasswordError = "Mật khẩu tối thiểu 6 ký tự!";
                isValid = false;
            }
            // Kiểm tra mật khẩu chỉ chứa chữ và số.
            else if (!Regex.IsMatch(Password, @"^[A-Za-z0-9]+$"))
            {
                PasswordError = "Mật khẩu không được chứa ký tự đặc biệt!";
                isValid = false;
            }
        }

        if (!isValid)
            return;

        await Task.Delay(1000);
        await Shell.Current.GoToAsync("//HomePage");
    }
}