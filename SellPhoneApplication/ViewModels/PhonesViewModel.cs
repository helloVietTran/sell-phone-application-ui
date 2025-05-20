

using CommunityToolkit.Mvvm.ComponentModel;
using SellPhoneApplication.Models;
using System.Collections.ObjectModel;

public class PhonesViewModel
{
    public ObservableCollection<Phone> Phones { get; set; }

    public PhonesViewModel()
    {
        Phones = new ObservableCollection<Phone>
            {
                new Phone
                {
                    Name = "iPhone 14 Pro Max",
                    Price = 29990000,
                    ImageUrl = "https://cdn2.cellphones.com.vn/insecure/rs:fill:358:358/q:90/plain/https://cellphones.com.vn/media/catalog/product/f/r/frame_166_3.png"
                },
                new Phone
                {
                    Name = "Samsung Galaxy S23 Ultra",
                    Price = 25990000,
                    ImageUrl = "https://cdn2.cellphones.com.vn/insecure/rs:fill:358:358/q:90/plain/https://cellphones.com.vn/media/catalog/product/f/r/frame_166_3.png"
                },
                 new Phone
                {
                    Name = "iPhone 14 Pro Max",
                    Price = 29990000,
                    ImageUrl = "https://cdn2.cellphones.com.vn/insecure/rs:fill:358:358/q:90/plain/https://cellphones.com.vn/media/catalog/product/f/r/frame_166_3.png"
                },
                new Phone
                {
                    Name = "Samsung Galaxy S23 Ultra",
                    Price = 25990000,
                    ImageUrl = "https://cdn2.cellphones.com.vn/insecure/rs:fill:358:358/q:90/plain/https://cellphones.com.vn/media/catalog/product/f/r/frame_166_3.png"
                }
            };
    }
}
