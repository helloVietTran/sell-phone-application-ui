namespace SellPhoneApplication.View;

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
        var label = sender as Label;
        if (label != null)
        {
            // Hiện dấu gạch dưới ngay khi nhấn
            label.TextDecorations = TextDecorations.Underline;

            await Task.Delay(100);
            label.TextDecorations = TextDecorations.None;

            await Shell.Current.GoToAsync("//RegisterPage");
        }

    }
}