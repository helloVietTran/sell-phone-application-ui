

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
using System.Web;

public partial class PhonesViewModel : ObservableObject
{
    private readonly ICartService _cartService;

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

    public PhonesViewModel(ICartService cartService)
    {
        _cartService = cartService;
        SelectedMemories = new ObservableCollection<string>();

        SelectedBrands = new ObservableCollection<string>();

    }

    [RelayCommand]
    public async Task ApplyFilterAsync()
    {
        var query = HttpUtility.ParseQueryString(string.Empty);

        if (MaxPrice > 0)
            query["maxPrice"] = MaxPrice.ToString();

        if (SelectedBrands.Any())
            foreach (var brand in SelectedBrands)
                query.Add("brands", brand);

        if (SelectedMemories.Any())
            foreach (var mem in SelectedMemories)
                query.Add("memories", mem);

        if (!string.IsNullOrEmpty(Color))
            query["color"] = Color;


        try
        {
            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync($"{AppConstants.BaseApiUrl}{AppConstants.FilterEndpoint}?{query.ToString()}");

            if (response.IsSuccessStatusCode)
            {
                
                var json = await response.Content.ReadAsStringAsync();
                

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var res = JsonSerializer.Deserialize<ApiResponse<List<Phone>>>(json, options);

                Debug.WriteLine("Gọi Api thành công: " + res);

                if (res?.Result != null)
                    Phones = new ObservableCollection<Phone>(res.Result);
            }
            else
            {
                
                ErrorMessage = "Lỗi khi tải dữ liệu sản phẩm";
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
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
}
