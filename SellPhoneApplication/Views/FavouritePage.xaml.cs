namespace SellPhoneApplication.Views;

public partial class FavouritePage : ContentPage
{
    public FavouritePage(FavouriteViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Gọi lại hàm LoadFavoritesAsync mỗi lần trang xuất hiện
        if (BindingContext is FavouriteViewModel vm)
        {
            vm.LoadFavoritesCommand.Execute(null);
        }
    }
}