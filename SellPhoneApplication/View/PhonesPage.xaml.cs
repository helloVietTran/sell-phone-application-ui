namespace SellPhoneApplication.View;

public partial class PhonesPage : ContentPage
{
	public PhonesPage(PhonesViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        Shell.SetFlyoutBehavior(this, FlyoutBehavior.Disabled);
    }
}