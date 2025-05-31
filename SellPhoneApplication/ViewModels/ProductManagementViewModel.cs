
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SellPhoneApplication.Models;
using SellPhoneApplication.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

public partial class ProductManagementViewModel : ObservableObject
{
    private readonly IProductService _productService;

    [ObservableProperty]
    private ObservableCollection<Phone> phones;

    [ObservableProperty]
    private string searchValue;

    public ProductManagementViewModel(IProductService productService)
    {
        _productService = productService;

        _ = LoadProductsAsync();
    }

    [RelayCommand]
    public async Task LoadProductsAsync()
    {
        try
        {
            var result = await _productService.SearchPhonesAsync(SearchValue);

            Phones = new ObservableCollection<Phone>(result);
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Lỗi khi load danh sách sản phẩm: " + ex.Message);
        }
    }


    [RelayCommand]
    private async Task DeletePhoneAsync(Phone phone)
    {
        var confirm = await Application.Current.MainPage.DisplayAlert("Xác nhận", $"Bạn có chắc muốn xóa {phone.Name}?", "Xóa", "Hủy");
        if (!confirm) return;

        var result = await _productService.DeleteProductAsync(phone.Id);
        if (result)
        {
            Phones.Remove(phone);
            await Application.Current.MainPage.DisplayAlert("Thành công", "Sản phẩm đã bị xóa", "OK");
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Lỗi", "Không thể xóa sản phẩm", "OK");
        }
    }
}


