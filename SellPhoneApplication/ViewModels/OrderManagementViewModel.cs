using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SellPhoneApplication.Models;
using SellPhoneApplication.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

public partial class OrderManagementViewModel : ObservableObject
{
    private readonly IOrderService _orderService;

    [ObservableProperty]
    ObservableCollection<Order> orders = new();

    public OrderManagementViewModel(IOrderService orderService)
    {
        _orderService = orderService;
        _ = LoadOrdersAsync();
    }

    [RelayCommand]
    private async Task LoadOrdersAsync()
    {
        try
        {
            var result = await _orderService.GetOrdersAsync();
            Orders = new ObservableCollection<Order>(result);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Lỗi khi lấy đơn hàng: {ex.Message}");
        }
    }

    [RelayCommand]
    private async Task ConfirmOrderAsync(int orderId)
    {
        try
        {
            await _orderService.ConfirmOrderAsync(orderId);
            await LoadOrdersAsync();
        }
        catch (Exception ex)
        {
            // Handle error
        }
    }

    [RelayCommand]
    private async Task CancelOrderAsync(int orderId)
    {
        try
        {
            await _orderService.CancelOrderAsync(orderId);
            await LoadOrdersAsync();
        }
        catch (Exception ex)
        {
            // Handle error
        }
    }
}
