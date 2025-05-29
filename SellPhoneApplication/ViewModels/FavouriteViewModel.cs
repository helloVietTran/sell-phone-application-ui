using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SellPhoneApplication.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;


public partial class FavouriteViewModel : ObservableObject
{
    public ObservableCollection<Phone> FavouritePhones { get; } = new();

    public ICommand UnfavoriteCommand { get; }
    public ICommand PhoneSelectedCommand { get; }
    public ICommand AddToCartCommand { get; }

    public FavouriteViewModel()
    {
        // Giả sử bạn đã load list phones yêu thích ở đây
        FavouritePhones = LoadFavouritePhones();

        UnfavoriteCommand = new RelayCommand<Phone>(OnUnfavorite);
        PhoneSelectedCommand = new RelayCommand<Phone>(OnPhoneSelected);
        AddToCartCommand = new RelayCommand<Phone>(OnAddToCart);
    }

    private void OnUnfavorite(Phone phone)
    {
        if (FavouritePhones.Contains(phone))
        {
            FavouritePhones.Remove(phone);
            // TODO: Đồng bộ lại backend/local storage
        }
    }

    private void OnPhoneSelected(Phone phone)
    {
        // TODO: Navigation đến trang detail
        Shell.Current.GoToAsync($"//PhoneDetailPage?phoneId={phone.Id}");
    }

    private void OnAddToCart(Phone phone)
    {
        // TODO: Thêm vào giỏ hàng
    }

    private ObservableCollection<Phone> LoadFavouritePhones()
    {
        // TODO: load từ DB / API
        return new ObservableCollection<Phone>
            {
                // ví dụ mẫu
                new Phone { Id = 1, Name="iPhone 13", Price=25000000, ImageUrl="iphone13.png" },
                new Phone { Id = 2, Name="Galaxy S21", Price=18000000, ImageUrl="galaxy_s21.png" },
            };
    }
}
