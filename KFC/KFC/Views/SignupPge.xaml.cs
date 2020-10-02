using Acr.UserDialogs;

using KFC.Services;

using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KFC.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignupPge : ContentPage {
        public SignupPge() {
            InitializeComponent();
        }

        private async void BtnSignUp_Clicked(object sender, EventArgs e) {

            using (UserDialogs.Instance.Loading("wait...")) {
                if (!EntPassword.Text.Equals(EntConfirmPassword.Text)) {
                    await DisplayAlert("Password mismatch", "Please check that both passwords are the same", "Canncel");
                } else {
                    var response = await ApiServices.RegisterUser(EntName.Text, EntEmail.Text, EntPassword.Text);

                    if (response) {
                        await DisplayAlert($"Welcome {EntName.Text}", "Your account has been created", "Alright");
                        await Navigation.PushModalAsync(new LoginPage(EntEmail.Text, EntPassword.Text));
                    } else {
                        await DisplayAlert("Oops", "Something went wrong", "Cancel");
                    }
                }

            }
        }

        protected override bool OnBackButtonPressed() {
            return true;
        }

        private async void BtnLogin_Clicked(object sender, EventArgs e) {

            await Navigation.PushModalAsync(new LoginPage(string.Empty, string.Empty));
        }
    }
}