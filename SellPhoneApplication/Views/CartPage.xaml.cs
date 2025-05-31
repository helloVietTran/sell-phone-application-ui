namespace SellPhoneApplication.Views
{
    public partial class CartPage : ContentPage
    {
        private readonly CartViewModel _viewModel;

        public CartPage(CartViewModel vm)
        {
            InitializeComponent();
            BindingContext = _viewModel = vm;
            Shell.SetFlyoutBehavior(this, FlyoutBehavior.Disabled);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            // call api every component mounted
            await _viewModel.LoadCartItems();
        }
    }
}
