
using Acr.UserDialogs;

using KFC.Services;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KFC.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage {
        public LoginPage() {
            InitializeComponent();
        }
        protected override bool OnBackButtonPressed() {
            return true;
        }

        private async void TapBackArrow_Tapped(object sender, System.EventArgs e) {

            await Navigation.PopModalAsync();
        }

        private async void BtnLogin_Clicked(object sender, System.EventArgs e) {

            using (UserDialogs.Instance.Loading("wait...")) {

                var response = await ApiServices.LoginUser(EntEmail.Text, EntPassword.Text);

                if (response) {
                    Application.Current.MainPage = new NavigationPage(new HomePage());
                } else {
                    await DisplayAlert("Oops", "Check your pasword", "Cancel");
                }
            }
        }
    }
}