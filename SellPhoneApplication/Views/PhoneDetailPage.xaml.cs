using CommunityToolkit.Maui.Views;
using SellPhoneApplication.Shared;
namespace SellPhoneApplication.Views;

public partial class PhoneDetailPage : ContentPage
{
    public PhoneDetailPage(PhoneDetailViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        Shell.SetFlyoutBehavior(this, FlyoutBehavior.Disabled);
    }
    private async void OnWriteReviewClicked(object sender, EventArgs e)
    {
        var popup = new ReviewPopup();
        var result = await this.ShowPopupAsync(popup);

        if (popup.Rating > 0 && !string.IsNullOrWhiteSpace(popup.Comment))
        {

        }
    }

}