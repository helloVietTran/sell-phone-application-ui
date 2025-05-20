namespace SellPhoneApplication.View;

public partial class PhoneDetailPage : ContentPage
{
	public PhoneDetailPage()
	{
		InitializeComponent();
		BindingContext = new PhoneDetailViewModel();
	}
}