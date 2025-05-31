using CommunityToolkit.Maui.Views;
using SellPhoneApplication.Shared;
using SellPhoneApplication.DTOs;
namespace SellPhoneApplication.Views;

public partial class PhoneDetailPage : ContentPage
{
    public PhoneDetailPage(PhoneDetailViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;

        Shell.SetFlyoutBehavior(this, FlyoutBehavior.Disabled);
    }
    private void OnWriteReviewClicked(object sender, EventArgs e)
    {
        var popup = new ReviewPopup();

        // 👇 Inject callback 
        if (BindingContext is PhoneDetailViewModel vm)
        {
            popup.OnSubmitReview = async (rating, comment) =>
            {
                var request = new ReviewRequest
                {
                    Rating = rating,
                    Content = comment,
                   
                };

                await vm.SubmitReviewAsync(request);
            };
        }

        this.ShowPopup(popup);
    }

}