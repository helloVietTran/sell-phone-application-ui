namespace SellPhoneApplication.View;

public partial class RegisterPage : ContentPage
{
    public RegisterPage()
    {
        InitializeComponent();
        BindingContext = new RegisterViewModel();
    }
    

    private async void OnLoginTapped(object sender, EventArgs e)
    {
        var label = sender as Label;
        if (label != null) { 

            label.TextDecorations = TextDecorations.Underline;

            await Task.Delay(100);
            label.TextDecorations = TextDecorations.None;

            await Shell.Current.GoToAsync("//LoginPage");
        }

    }
}