namespace SellPhoneApplication.Views;

public partial class ProductManagementPage : ContentPage
{
    public ProductManagementPage()
    {
        InitializeComponent();
        BindingContext = new ProductManagementViewModel();

        Shell.SetFlyoutBehavior(this, FlyoutBehavior.Disabled);
    }

    private async void OnOrderManagementTapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//OrderManagementPage");
    }

}