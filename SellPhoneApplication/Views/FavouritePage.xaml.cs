namespace SellPhoneApplication.Views;

public partial class FavouritePage : ContentPage
{
    public FavouritePage()
    {
        InitializeComponent();
        BindingContext = new FavouriteViewModel();
    }
}