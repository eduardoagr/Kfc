
using Acr.UserDialogs;

using KFC.Services;

using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KFC.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage {
        public LoginPage(string email, string password) {
            InitializeComponent();

            EntEmail.Text = email;
            EntPassword.Text = password;
        }
        protected override bool OnBackButtonPressed() {
            return true;
        }

        private async void TapBackArrow_Tapped(object sender, System.EventArgs e) {

            await Navigation.PopModalAsync();
        }

        private async void BtnLogin_Clicked(object sender, System.EventArgs e) {

            BtnLogin.IsVisible = false;
            using (UserDialogs.Instance.Loading("wait...")) {

                var response = await ApiServices.LoginUser(EntEmail.Text, EntPassword.Text);
                Preferences.Set("email", EntEmail.Text);
                Preferences.Set("pass", EntPassword.Text);

                if (response) {
                    Application.Current.MainPage = new NavigationPage(new HomePage());
                } else {
                    await DisplayAlert("Oops", "Check your pasword", "Cancel");
                    BtnLogin.IsVisible = true;
                }
            }
        }
    }
}