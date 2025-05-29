namespace SellPhoneApplication.Views;

public partial class RegisterPage : ContentPage
{
    public RegisterPage()
    {
        InitializeComponent();
        BindingContext = new RegisterViewModel();
    }


    private async void OnLoginTapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//LoginPage");
    }
}