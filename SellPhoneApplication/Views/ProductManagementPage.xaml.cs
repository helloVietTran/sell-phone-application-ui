using CommunityToolkit.Maui.Views;
using SellPhoneApplication.Shared;
namespace SellPhoneApplication.Views;

public partial class ProductManagementPage : ContentPage
{
    public ProductManagementPage(ProductManagementViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;

        Shell.SetFlyoutBehavior(this, FlyoutBehavior.Disabled);
    }

    private async void OnAddNewClicked(object sender, EventArgs e)
    {
        var popup = new PhonePopup();
        await Application.Current.MainPage.ShowPopupAsync(popup);
    }

}