
using SellPhoneApplication.constant;
using System.Net.Http.Headers;

namespace SellPhoneApplication.Shared;
public partial class PhonePopup : CommunityToolkit.Maui.Views.Popup
{
    private FileResult _selectedImage;

    public PhonePopup()
    {
        InitializeComponent();

        LoadColorOptions();
        LoadProductTypes();
        LoadBrands();
    }

    private void LoadColorOptions()
    {
        ColorPicker.ItemsSource = new List<string> { "BLACK", "BLUE", "PINK", "RED" };
    }

    private void LoadProductTypes()
    {
        TypePicker.ItemsSource = new List<string> { "PHONE", "ACCESSORY", "TABLET", "LAPTOP" };
    }

    private void LoadBrands()
    {
        BrandPicker.ItemsSource = new List<string> { "XIAOMI", "APPLE", "SAMSUNG", "HUAWEI" };
    }

    private async void OnPickImageClicked(object sender, EventArgs e)
    {

    }


    private async void OnSubmitClicked(object sender, EventArgs e)
    {
        try
        {


            var content = new MultipartFormDataContent();

            content.Add(new StringContent(NameEntry.Text ?? ""), "name");
            content.Add(new StringContent(DescriptionEditor.Text ?? ""), "description");
            content.Add(new StringContent(PriceEntry.Text ?? "0"), "price");
            content.Add(new StringContent(RamEntry.Text ?? "0"), "ram");
            content.Add(new StringContent(MemoryEntry.Text ?? "0"), "memory");
            content.Add(new StringContent(StockEntry.Text ?? "0"), "stock");
            content.Add(new StringContent(ColorPicker.SelectedItem?.ToString() ?? ""), "color");
            content.Add(new StringContent(TypePicker.SelectedItem?.ToString() ?? ""), "type");
            content.Add(new StringContent(BrandPicker.SelectedItem?.ToString() ?? ""), "brand");

            var token = await SecureStorage.GetAsync("auth_token");

            using var httpClient = new HttpClient();

            // gắn token
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.PostAsync($"{AppConstants.BaseApiUrl}/products", content);

            if (response.IsSuccessStatusCode)
            {
                await Application.Current.MainPage.DisplayAlert("Thành công", "Sản phẩm đã được tạo", "OK");
                Close();
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                await Application.Current.MainPage.DisplayAlert("Lỗi", $"Tạo thất bại: {error}", "OK");
            }
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Lỗi", $"Lỗi khi gửi dữ liệu: {ex.Message}", "OK");
        }
    }
}
