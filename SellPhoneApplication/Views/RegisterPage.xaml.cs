namespace SellPhoneApplication.Views;

public partial class RegisterPage : ContentPage
{
    public RegisterPage(RegisterViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }


    protected override void OnAppearing()
    {
        base.OnAppearing();
        Shell.SetFlyoutBehavior(this, FlyoutBehavior.Disabled);
    }
    private async void OnLoginTapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//LoginPage");
    }
}