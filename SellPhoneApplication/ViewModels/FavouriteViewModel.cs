using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SellPhoneApplication.Models;
using SellPhoneApplication.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

public partial class FavouriteViewModel : ObservableObject
{
    private readonly IFavouriteService _favouriteService;

    public FavouriteViewModel(IFavouriteService favouriteService)
    {
        _favouriteService = favouriteService;
        
    }

    [ObservableProperty]
    private ObservableCollection<FavoriteProduct> favouritePhones = new();

    [ObservableProperty]
    private string errorMessage;

    // Command để load lại danh sách
    [RelayCommand]
    public async Task LoadFavoritesAsync()
    {
        try
        {
            var list = await _favouriteService.GetFavoritesAsync();
            FavouritePhones = new ObservableCollection<FavoriteProduct>(list);
        }
        catch (System.Exception ex)
        {
            ErrorMessage = "Không thể tải danh sách yêu thích.";
        }
    }

    // Command remove (unfavorite)
    [RelayCommand]
    public async Task UnfavouriteAsync(FavoriteProduct item)
    {
        if (item == null) return;

        try
        {
            await _favouriteService.RemoveFavouriteAsync(item.FavouriteId);
            // Reload lại danh sách
            await LoadFavoritesAsync();
        }
        catch (System.Exception ex)
        {
            ErrorMessage = "Xóa yêu thích thất bại.";
        }
    }
}
