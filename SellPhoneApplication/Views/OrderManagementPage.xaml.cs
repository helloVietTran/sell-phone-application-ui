namespace SellPhoneApplication.Views;

public partial class OrderManagementPage : ContentPage
{
    public OrderManagementPage(OrderManagementViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }


}