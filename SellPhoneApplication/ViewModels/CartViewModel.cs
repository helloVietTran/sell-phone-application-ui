using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SellPhoneApplication.Models;
using SellPhoneApplication.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
public partial class CartViewModel : ObservableObject
{
    private readonly ICartService _cartService;
    private readonly IOrderService _orderService;

    [ObservableProperty]
    private ObservableCollection<CartItem> cartItems;

    public double Subtotal => CartItems?.Sum(x => x.Price * x.Quantity) ?? 0;

    public double Discount => Subtotal * 0.2;

    public bool HasItems => CartItems != null && CartItems.Any();
    public double DeliveryFee => HasItems ? 15000 : 0;

    public double Total => Subtotal - Discount + DeliveryFee;

    public CartViewModel(ICartService cartService, IOrderService orderService)
    {
        _cartService = cartService;
        _orderService = orderService;

        LoadCartItemsCommand.Execute(null);       
    }


    [RelayCommand]
    public async Task LoadCartItems()
    {
        try
        {
            var items = await _cartService.GetCartItemsAsync();
            CartItems = new ObservableCollection<CartItem>(items);

            OnPropertyChanged(nameof(CartItems));
            OnPropertyChanged(nameof(Subtotal));
            OnPropertyChanged(nameof(Discount));
            OnPropertyChanged(nameof(DeliveryFee));
            OnPropertyChanged(nameof(Total));
            OnPropertyChanged(nameof(HasItems));
        }
        catch (Exception ex)
        {
            // xử lý lỗi nếu cần
        }
    }

    [RelayCommand]
    private async Task Remove(CartItem item)
    {
        if (item == null)
            return;

        try
        {
            await _cartService.RemoveFromCartAsync(item.ProductId);
            await LoadCartItems(); // Tải lại danh sách sau khi xóa
        }
        catch (Exception ex)
        {

            Debug.WriteLine("Lỗi khi xóa sản phẩm: " + ex.Message);
        }
    }

    [RelayCommand]
    private async Task PlaceOrder()
    {
       await _orderService.PlaceOrderAsync();
    }
}
