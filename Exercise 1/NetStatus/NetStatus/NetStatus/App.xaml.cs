// App.xaml.cs

using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace NetStatus
{
    using Xamarin.Forms;
    using Plugin.Connectivity;

    public partial class App : Application
	{
        #region Constructor

        public App()
		{
			InitializeComponent();

            NavigateBasedOnConnectivity();
        }

        #endregion

        #region Application Lifecycle

        protected override void OnStart()
		{
            // Handle when your app starts
            CrossConnectivity.Current.ConnectivityChanged +=
                (s, e) =>
                {
                    NavigateBasedOnConnectivity();
                };
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}

        #endregion

        #region Helper Methods

        private void NavigateBasedOnConnectivity()
        {
            if (!CrossConnectivity.IsSupported)
            {
                MainPage = new PluginNotSupported();
            }
            else
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    MainPage = new NetworkViewPage();
                }
                else
                {
                    MainPage = new NoNetworkPage();
                }
            }
        }

        #endregion
    }
}
