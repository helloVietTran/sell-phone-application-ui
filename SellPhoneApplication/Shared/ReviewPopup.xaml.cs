namespace SellPhoneApplication.Shared;

public partial class ReviewPopup : CommunityToolkit.Maui.Views.Popup
{
    public int Rating { get; private set; } = 0;
    public string Comment => ReviewEditor.Text;

    public ReviewPopup()
    {
        InitializeComponent();
        GenerateStars();
    }

    private void GenerateStars()
    {
        StarContainer.Children.Clear();

        for (int i = 1; i <= 5; i++)
        {
            var image = new Image
            {
                Source = "yellow_star_icon.png",
                WidthRequest = 30,
                HeightRequest = 30,
                Opacity = 0.3,
            };

            var tapGesture = new TapGestureRecognizer
            {
                CommandParameter = i
            };
            tapGesture.Tapped += OnStarTapped;

            image.GestureRecognizers.Add(tapGesture);

            var frame = new Frame
            {
                Padding = 0,
                Margin = new Thickness(5, 0),
                CornerRadius = 4,
                WidthRequest = 38,
                HeightRequest = 38,
                BackgroundColor = Colors.Transparent,
                Content = image
            };

            StarContainer.Children.Add(frame);
        }
    }


    private void OnStarTapped(object sender, TappedEventArgs e)
    {
        if (e.Parameter is int starCount)
        {
            Rating = starCount;

            for (int i = 0; i < StarContainer.Children.Count; i++)
            {
                if (StarContainer.Children[i] is Frame frame &&
                    frame.Content is Image star)
                {
                    star.Opacity = i < starCount ? 1 : 0.3;
                }
            }
        }
    }

    private void OnSubmitClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(Comment) || Rating == 0)
        {

            return;
        }

        Close(this);
    }
}
