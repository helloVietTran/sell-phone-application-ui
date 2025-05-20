namespace SellPhoneApplication.View;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        Shell.SetFlyoutBehavior(this, FlyoutBehavior.Disabled);
    }

    private async void OnHomeTapped(object sender, EventArgs e)
    {
        //await Shell.Current.GoToAsync("//home");
    }

    private async void OnSenifoneTapped(object sender, EventArgs e)
    {
        //await Shell.Current.GoToAsync("//senifoneS1");
    }

    private async void OnAccessoriesTapped(object sender, EventArgs e)
    {
       // await Shell.Current.GoToAsync("//accessories");
    }

    private async void OnCustomerServiceTapped(object sender, EventArgs e)
    {
        //await Shell.Current.GoToAsync("//customerService");
    }

    private async void OnPointsOfSaleTapped(object sender, EventArgs e)
    {
        // await Shell.Current.GoToAsync("//pointsOfSale");
    }

    private async void OnUserTapped(object sender, EventArgs e)
    {
        //await Shell.Current.GoToAsync("//profile");
    }

    private async void OnSearchTapped(object sender, EventArgs e)
    {
     //   await Shell.Current.GoToAsync("//search");
    }

    private async void OnCartTapped(object sender, EventArgs e)
    {
      //  await Shell.Current.GoToAsync("//cart");
    }
}