using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using SellPhoneApplication.Services;
using SellPhoneApplication.Views;

namespace SellPhoneApplication
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()

                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.UseMauiCommunityToolkit();
            builder.Services.AddSingleton<ICartService, CartService>();
            builder.Services.AddSingleton<IReviewService, ReviewService>();
            builder.Services.AddSingleton<IAuthService, AuthService>();
            builder.Services.AddSingleton<IProductService, ProductService>();
            builder.Services.AddSingleton<IOrderService, OrderService>();
            builder.Services.AddSingleton<IFavouriteService, FavouriteService>();

            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<RegisterViewModel>();
            builder.Services.AddTransient<PhonesViewModel>();
            builder.Services.AddTransient<HomeViewModel>();
            builder.Services.AddTransient<PhoneDetailViewModel>();
            builder.Services.AddTransient<HeaderViewModel>();
            builder.Services.AddTransient<CartViewModel>();
            builder.Services.AddTransient<FavouriteViewModel>();
            builder.Services.AddTransient<ProductManagementViewModel>();
            builder.Services.AddTransient<OrderManagementViewModel>();

            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton<HomePage>();
            builder.Services.AddSingleton<RegisterPage>();
            builder.Services.AddSingleton<PhonesPage>();
            builder.Services.AddSingleton<PhoneDetailPage>();
            builder.Services.AddSingleton<CartPage>();
            builder.Services.AddSingleton<FavouritePage>();
            builder.Services.AddSingleton<ProductManagementPage>();
            builder.Services.AddSingleton<OrderManagementPage>();




#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
