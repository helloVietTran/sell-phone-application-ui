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

    private void OnFavouriteTapped(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//FavouritePage");
    }

    private void OnManagementTapped(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//ProductManagementPage");
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