namespace SellPhoneApplication.Views;

public partial class OrderManagementPage : ContentPage
{
    public OrderManagementPage()
    {
        InitializeComponent();
        BindingContext = new OrderManagementViewModel();
    }

    private async void OnProductManagementTapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//ProductManagementPage");
    }


}