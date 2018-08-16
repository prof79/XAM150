// CustomActivityIndicatorRenderer.cs

using BookClient.CustomViews;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(CustomActivityIndicator), typeof(BookClient.UWP.CustomViews.CustomActivityIndicatorRenderer))]

namespace BookClient.UWP.CustomViews
{
    public class CustomActivityIndicatorRenderer : ViewRenderer<CustomActivityIndicator, ProgressRing>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<CustomActivityIndicator> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                return;
            }

            var foreColor =
                Color.FromArgb(
                    (byte) (e.NewElement.Color.A * 255),
                    (byte) (e.NewElement.Color.R * 255),
                    (byte) (e.NewElement.Color.G * 255),
                    (byte) (e.NewElement.Color.B * 255));

            var backColor =
                Color.FromArgb(
                    (byte) (e.NewElement.BackgroundColor.A * 255),
                    (byte) (e.NewElement.BackgroundColor.R * 255),
                    (byte) (e.NewElement.BackgroundColor.G * 255),
                    (byte) (e.NewElement.BackgroundColor.B * 255));

            var progressRing = new ProgressRing
            {
                IsActive = e.NewElement.IsRunning,
                Visibility = e.NewElement.IsVisible ? Visibility.Visible : Visibility.Collapsed,
                IsEnabled = e.NewElement.IsEnabled,
                // Set to your foreground colour
                //Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0))
                Foreground = new SolidColorBrush(foreColor),
                Background = new SolidColorBrush(backColor),
                Width = 100,
                Height = 25,
            };

            SetNativeControl(progressRing);
        }
    }
}
