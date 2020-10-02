
using System;
using System.Diagnostics;

using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KFC.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactPage : ContentPage {
        public ContactPage() {
            InitializeComponent();
        }

        private void BtnCall_Clicked(object sender, EventArgs e) {

            string[] SupportArray = BtnCall.Text.Split(' ');

            PlacePhoneCall(SupportArray[1]);

        }

        public void PlacePhoneCall(string number) {
            try {
                PhoneDialer.Open(number);
            } catch (ArgumentNullException anEx) {
                DisplayAlert("Error", anEx.Message, "OK");
            } catch (FeatureNotSupportedException ex) {
                DisplayAlert("Error", ex.Message, "OK");
            } catch (Exception ex) {
                DisplayAlert("Error", ex.Message, "OK");

            }
        }

        private void TapBack_Tapped(object sender, EventArgs e) {

            Navigation.PopModalAsync();
        }
    }
}