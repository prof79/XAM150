// NetworkViewPage.xaml.cs

namespace NetStatus
{
    using Plugin.Connectivity;
    using Plugin.Connectivity.Abstractions;
    using System;
    using System.Linq;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NetworkViewPage : ContentPage
	{
        #region Constructor

        public NetworkViewPage()
		{
			InitializeComponent();
		}

        #endregion

        #region Page Lifecycle

        protected override void OnAppearing()
        {
            base.OnAppearing();

            UpdateNetworkTypes(this, null);

            // Subscribe to event
            CrossConnectivity
                .Current
                .ConnectivityTypeChanged
                    += UpdateNetworkTypes;
        }

        protected override void OnDisappearing()
        {
            // Unsubscribe from event
            CrossConnectivity
                .Current
                .ConnectivityTypeChanged
                    -= UpdateNetworkTypes;

            base.OnDisappearing();
        }

        #endregion

        #region Helper Methods

        private void UpdateNetworkTypes(object sender, ConnectivityTypeChangedEventArgs e)
        {
            // Get and show connection methods on page
            if (CrossConnectivity.Current?.ConnectionTypes != null)
            {
                var types =
                    CrossConnectivity
                        .Current
                        .ConnectionTypes
                        .Select(t => t.ToString());

                var infoString =
                    string.Join(", ", types);

                ConnectionDetails.Text =
                    $"Connection type(s): {infoString}";
            }
        }

        #endregion
    }
}
