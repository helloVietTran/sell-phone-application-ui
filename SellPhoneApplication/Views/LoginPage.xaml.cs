namespace SellPhoneApplication.Views;

public partial class LoginPage : ContentPage
{
    // tiêm loginviewmodel
    public LoginPage(LoginViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        Shell.SetFlyoutBehavior(this, FlyoutBehavior.Disabled);
    }

    private async void OnRegisterTapped(object sender, EventArgs e)
    {

        await Shell.Current.GoToAsync("//RegisterPage");

    }
}