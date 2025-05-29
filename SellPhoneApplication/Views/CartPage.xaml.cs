namespace SellPhoneApplication.Views;

public partial class CartPage : ContentPage
{
    public CartPage(CartViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        Shell.SetFlyoutBehavior(this, FlyoutBehavior.Disabled);

    }
}