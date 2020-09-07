using KFC.Views;

using Xamarin.Essentials;
using Xamarin.Forms;

namespace KFC {
    public partial class App : Application {
        public App() {
            InitializeComponent();

            var token = Preferences.Get("token", string.Empty);

            if (string.IsNullOrEmpty(token)) {
                MainPage = new NavigationPage(new SignupPge());
            } else {
                MainPage = new NavigationPage(new HomePage());
            }


        }

        protected override void OnStart() {
        }

        protected override void OnSleep() {
        }

        protected override void OnResume() {
        }
    }
}
