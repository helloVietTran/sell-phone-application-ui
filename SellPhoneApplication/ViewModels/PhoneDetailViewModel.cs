using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SellPhoneApplication.constant;
using SellPhoneApplication.DTOs;
using SellPhoneApplication.Models;
using SellPhoneApplication.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;


[QueryProperty(nameof(ProductId), "productId")]
public partial class PhoneDetailViewModel : ObservableObject

{
    private readonly ICartService _cartService;

    private readonly IReviewService _reviewService;

    [ObservableProperty]
    string productId;

    [ObservableProperty]
    private Phone phone;

    [ObservableProperty]
    public ObservableCollection<Review> reviews;

    [ObservableProperty]
    public String errorMessage;

    public string MemoryDisplay => phone?.Memory.HasValue == true ? $"{phone.Memory.Value} GB" : "N/A";
    public string RamDisplay => phone?.Ram.HasValue == true ? $"{phone.Ram.Value} GB" : "N/A";

    public PhoneDetailViewModel(ICartService cartService, IReviewService reviewService)
    {
        _cartService = cartService;
        _reviewService = reviewService;
    }

    partial void OnProductIdChanged(string value)
    {
        // Khi productId được gán từ Shell, gọi API
        _ = LoadPhoneDetailAsync(value);

        LoadReviewsCommand.Execute(null);
    }


    [RelayCommand]
    private async Task LoadReviews()
    {
        try
        {
            var items = await _reviewService.GetReviewAsync(productId);
            Reviews = new ObservableCollection<Review>(items);

        }
        catch (Exception ex)
        {
            Debug.WriteLine("Lỗi khi tả reviews");
        }
    }

    private async Task LoadPhoneDetailAsync(string id)
    {

        try
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"{AppConstants.BaseApiUrl}/products/{id}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var res = JsonSerializer.Deserialize<ApiResponse<Phone>>(json, options);
                Phone = res?.Result;
            }
            else
            {
                ErrorMessage = "Không thể tải chi tiết sản phẩm.";
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
    }



    [RelayCommand]
    private async Task AddToCart()
    {
        try
        {
            await _cartService.AddToCartAsync(Phone.Id, 1);
        }
        catch (Exception ex)
        {
            // handle error
        }
    }

    partial void OnPhoneChanged(Phone value)
    {
        OnPropertyChanged(nameof(PhoneColor));
    }
    public Microsoft.Maui.Graphics.Color PhoneColor
    {
        get
        {
            if (string.IsNullOrWhiteSpace(phone?.Color))
                return Microsoft.Maui.Graphics.Colors.Transparent;

            return MapColorNameToColor(phone.Color);
        }
    }

    private Microsoft.Maui.Graphics.Color MapColorNameToColor(string colorName)
    {
        return colorName.ToUpper() switch
        {
            "BLACK" => Microsoft.Maui.Graphics.Colors.Black,
            "WHITE" => Microsoft.Maui.Graphics.Colors.White,
            "RED" => Microsoft.Maui.Graphics.Colors.Red,
            "GREEN" => Microsoft.Maui.Graphics.Colors.Green,
            "BLUE" => Microsoft.Maui.Graphics.Colors.Blue,
            "YELLOW" => Microsoft.Maui.Graphics.Colors.Yellow,
            "GRAY" => Microsoft.Maui.Graphics.Colors.Gray,
            "PINK" => Microsoft.Maui.Graphics.Color.FromArgb("#FFC0CB"),
            _ => Microsoft.Maui.Graphics.Colors.Transparent
        };
    }

}