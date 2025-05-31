using System.Diagnostics;

namespace SellPhoneApplication.Shared;

public partial class HeaderView : ContentView
{
    public HeaderView() : this(new HeaderViewModel())
    {
    }
    public HeaderView(HeaderViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    private void OnHomeTapped(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//HomePage");
    }

    private void OnPhonesTapped(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//PhonesPage");
    }

    private async void OnFavouriteTapped(object sender, EventArgs e)
    {
        Debug.WriteLine("Navigating to FavouritePage...");
        try
        {
            await Shell.Current.GoToAsync("//FavouritePage");
            Debug.WriteLine("Navigation succeeded.");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Navigation failed: {ex.Message}");
        }
    }


    private void OnProductManagementTapped(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//ProductManagementPage");
    }

    private void OnOrderManagementTapped(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//OrderManagementPage");
    }

    private void OnUserTapped(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//ProfilePage");
    }

    private void OnLogoutTapped(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//LoginPage");
    }

    private void OnCartTapped(object sender, EventArgs e)
    {

        Shell.Current.GoToAsync("//CartPage");
    }
}