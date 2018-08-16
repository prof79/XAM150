// App.cs

using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace BookClient
{
    using Xamarin.Forms;

    public class App : Application
	{
        #region Constructor

        public App()
		{
            // The root page of your application
            MainPage = new NavigationPage(new MainPage());
        }

        #endregion

        #region Application Lifecycle

        protected override void OnStart()
		{
			// Handle when your app starts
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
    }
}
