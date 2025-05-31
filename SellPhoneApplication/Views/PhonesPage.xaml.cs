using SellPhoneApplication.Models;

namespace SellPhoneApplication.Views;

public partial class PhonesPage : ContentPage
{
    public PhonesPage(PhonesViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        Shell.SetFlyoutBehavior(this, FlyoutBehavior.Disabled);

        if (BindingContext is PhonesViewModel vm)
            await vm.ApplyFilterAsync();
    }

    private void MemoryCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (BindingContext is PhonesViewModel vm && sender is CheckBox cb && cb.ClassId is string memory)
        {
            vm.ToggleMemoryCommand.Execute(memory);
        }
    }

    private void BrandCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (BindingContext is PhonesViewModel vm && sender is CheckBox cb && cb.ClassId is string brand)
        {
            vm.ToggleBrandCommand.Execute(brand);
        }
    }

    private async void OnPhoneTapped(object sender, EventArgs e)
    {
        if (sender is VisualElement element && element.BindingContext is Phone tappedPhone)
        {
            await Shell.Current.GoToAsync($"///PhoneDetailPage?productId={tappedPhone.Id}");
        }
    }

    private void OnSortPickerChanged(object sender, EventArgs e)
    {
        var picker = sender as Picker;
        if (picker.SelectedItem is string selected)
        {
            if (BindingContext is PhonesViewModel vm)
            {
                switch (selected)
                {
                    case "Giá: Thấp → Cao":
                        vm.SortByPrice = "asc";
                        break;
                    case "Giá: Cao → Thấp":
                        vm.SortByPrice = "dsc";
                        break;
                    default:
                        vm.SortByPrice = null;
                        break;
                }

                vm.ApplyFilterCommand.Execute(null); 
            }
        }
    }

}