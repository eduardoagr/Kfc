using KFC.Models;
using KFC.Services;

using System;
using System.Linq;

using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KFC.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlaceOrderPage : ContentPage {

        private double totalPrice;

        public PlaceOrderPage(double PriceTotal) {
            InitializeComponent();

            totalPrice = PriceTotal;
        }
        private async void GetLocation_Clicked(object sender, EventArgs e) {

            try {

                Location location = await Geolocation.GetLastKnownLocationAsync();
                if (location == null) {
                    location = await Geolocation.GetLocationAsync(new GeolocationRequest() {
                        DesiredAccuracy = GeolocationAccuracy.Best,
                        Timeout = TimeSpan.FromSeconds(30)
                    });
                }

                var placemarks = await Geocoding.GetPlacemarksAsync(location.Latitude,
                    location.Longitude);

                var placemark = placemarks?.FirstOrDefault();
                if (placemark != null) {
                    var geocodeAddress =
                        $"AdminArea:       {placemark.AdminArea}\n" +
                        $"CountryCode:     {placemark.CountryCode}\n" +
                        $"CountryName:     {placemark.CountryName}\n" +
                        $"FeatureName:     {placemark.FeatureName}\n" +
                        $"Locality:        {placemark.Locality}\n" +
                        $"PostalCode:      {placemark.PostalCode}\n" +
                        $"SubAdminArea:    {placemark.SubAdminArea}\n" +
                        $"SubLocality:     {placemark.SubLocality}\n" +
                        $"SubThoroughfare: {placemark.SubThoroughfare}\n" +
                        $"Thoroughfare:    {placemark.Thoroughfare}\n";

                    EntAddress.Text = $"{placemark.Thoroughfare}" +
                        $", {placemark.Locality}" +
                        $", {placemark.CountryName}" +
                        $", {placemark.PostalCode}";
                }


            } catch (Exception ex) {

                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async void BtnPlaceOrder_Clicked(object sender, EventArgs e) {

            Order order = new Order {
                fullName = EntName.Text,
                phone = EntPhone.Text,
                address = EntAddress.Text,
                userId = Preferences.Get("userId", 0),
                orderTotal = totalPrice
            };

            OrderResponce response = await ApiServices.PlceOrder(order);
            if (response != null) {

                await DisplayAlert($"Order received. Thank you",
                    $"order # {response.orderId}", "OK");
                Application.Current.MainPage = new NavigationPage(new HomePage());
            }
        }

        private void TapBack_Tapped(object sender, EventArgs e) {

            Navigation.PopModalAsync();
        }
    }
}