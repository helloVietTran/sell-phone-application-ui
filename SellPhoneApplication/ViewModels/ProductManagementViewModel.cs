
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SellPhoneApplication.Models;
using System.Collections.ObjectModel;


public partial class ProductManagementViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Phone> phones;

    public ProductManagementViewModel()
    {
        // Fake data
        Phones = new ObservableCollection<Phone>
            {
                new Phone { Id = 1, Name = "iPhone 14 Pro", Price = 30000000, Ram = 6, Memory = 128, Stock = 5, ImageUrl = "mockup_phone.png" },
                new Phone { Id = 2, Name = "Samsung S23 Ultra", Price = 27000000, Ram = 8, Memory = 256, Stock = 3, ImageUrl = "mockup_phone.png" },
                new Phone { Id = 3, Name = "Xiaomi 13 Pro", Price = 20000000, Ram = 12, Memory = 256, Stock = 10, ImageUrl = "mockup_phone.png" },
            };
    }

    [RelayCommand]
    private void Edit(Phone phone)
    {

    }

    [RelayCommand]
    private void Delete(Phone phone)
    {

    }

    [RelayCommand]
    private void AddNew()
    {

    }
}


