using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Threading;

namespace Velo
{
    public partial class MainPage : ContentPage
    {
        CancellationTokenSource cts;

        public MainPage()
        {
            InitializeComponent();
        }


        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var location = await GetCurrentLocation();
            

            

            string text = $"Velocity: {location.Speed}";

            velocityLabel.Text = text;
        }

        public async Task<Location> GetCurrentLocation()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                cts = new CancellationTokenSource();
                var location = await Geolocation.GetLocationAsync(request, cts.Token);

                if (location == null)
                    return null;
                return location;
                
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        protected override void OnDisappearing()
        {
            if (cts != null && !cts.IsCancellationRequested)
                cts.Cancel();
            base.OnDisappearing();
        }

        private async void refreshButton_Clicked(object sender, EventArgs e)
        {
            var location = await GetCurrentLocation();

            string text = $"Velocity: {location.Speed}";

            velocityLabel.Text = text;
        }
    }
}
