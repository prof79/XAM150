// MainPage.xaml.cs

namespace NetStatus.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            LoadApplication(new NetStatus.App());
        }
    }
}
