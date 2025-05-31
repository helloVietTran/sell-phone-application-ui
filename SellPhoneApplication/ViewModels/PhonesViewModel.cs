

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SellPhoneApplication.Models;
using SellPhoneApplication.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

public partial class PhonesViewModel : ObservableObject
{
    private readonly ICartService _cartService;
    private readonly IProductService _productService;
    private readonly IFavouriteService _favouriteService;

    [ObservableProperty]
    double maxPrice = 25000000;

    [ObservableProperty]
    String color;

    [ObservableProperty]
    ObservableCollection<string> selectedMemories = new();

    [ObservableProperty]
    ObservableCollection<string> selectedBrands = new();

    [ObservableProperty]
    public ObservableCollection<Phone> phones = new();

    [ObservableProperty]
    string errorMessage;

    [ObservableProperty]
    string sortByPrice;

    public PhonesViewModel(ICartService cartService, IProductService productService, IFavouriteService favouriteService)
    {
        _cartService = cartService;
        _productService = productService;
        _favouriteService = favouriteService;

        SelectedMemories = new ObservableCollection<string>();
        SelectedBrands = new ObservableCollection<string>();

    }

    [RelayCommand]
    public async Task ApplyFilterAsync()
    {
        try
        {
            var result = await _productService.FilterPhonesAsync(MaxPrice, SelectedBrands, SelectedMemories, Color, SortByPrice);

            if (result != null)
            {
                Phones = new ObservableCollection<Phone>(result);
            }
            else
            {
                ErrorMessage = "Không có sản phẩm phù hợp.";
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = "Lỗi khi tải dữ liệu sản phẩm: " + ex.Message;
            Debug.WriteLine(ex.Message);
        }
    }


    [RelayCommand]
    private void SetColor(string selectedColor)
    {
        Color = selectedColor;
    }

    [RelayCommand]
    private void ToggleMemory(string memory)
    {
        if (string.IsNullOrEmpty(memory))
            return;

        if (SelectedMemories.Contains(memory))
        {
            SelectedMemories.Remove(memory);
        }
        else
        {
            SelectedMemories.Add(memory);
        }
    }

    [RelayCommand]
    private void ToggleBrand(string memory)
    {
        if (string.IsNullOrEmpty(memory))
            return;

        if (SelectedBrands.Contains(memory))
        {
            SelectedBrands.Remove(memory);
        }
        else
        {
            SelectedBrands.Add(memory);
        }
    }

    [RelayCommand]
    public async Task AddToCart(Phone phone)
    {
        if (phone == null) return;

        try
        {
            await _cartService.AddToCartAsync(phone.Id, 1);

        }
        catch (Exception ex)
        {
            Debug.WriteLine("Thêm sản phẩm vào giỏ hàng thất bại!");
            Debug.WriteLine(ex.Message);
        }
    }

    [RelayCommand]
    public async Task ToggleFavoriteAsync(Phone phone)
    {
        if (phone == null)
            return;

        try
        {
            await _favouriteService.AddToFavoritesAsync(phone.Id);
            Debug.WriteLine($"Đã thêm sản phẩm {phone.Id} vào danh sách yêu thích");
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Lỗi khi thêm yêu thích: " + ex.Message);
        }
    }
}
