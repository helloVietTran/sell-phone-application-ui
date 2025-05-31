namespace SellPhoneApplication.Views;

public partial class FavouritePage : ContentPage
{
    public FavouritePage(FavouriteViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}