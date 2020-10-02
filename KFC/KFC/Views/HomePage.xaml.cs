using KFC.Models;
using KFC.Services;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KFC.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage {

        ObservableCollection<PopularProduct> PopularProductsCollection;
        ObservableCollection<Category> CategoriesCollection;
        public HomePage() {
            InitializeComponent();
            PopularProductsCollection = new ObservableCollection<PopularProduct>();
            CategoriesCollection = new ObservableCollection<Category>();
            GetPopularProduct();
            GetCategories();
            LblUserName.Text = Preferences.Get("userName", string.Empty);
        }

        private async void GetCategories() {

            List<Category> Categories = await ApiServices.GetCategories();

            CategoriesCollection.Clear();
            foreach (var item in Categories) {

                CategoriesCollection.Add(item);
            }

            CvCategories.ItemsSource = CategoriesCollection;
        }

        private async void GetPopularProduct() {

            List<PopularProduct> products = await ApiServices.GetPopularProducts();

            PopularProductsCollection.Clear();
            foreach (var item in products) {

                PopularProductsCollection.Add(item);
            }
            CvProducts.ItemsSource = PopularProductsCollection;
        }

        protected override bool OnBackButtonPressed() {
            return true;
        }

        private async void ImgMenu_Tapped(object sender, EventArgs e) {

            GridOverlay.IsVisible = true;
            await SlMenu.TranslateTo(0, 0, 400, Easing.Linear);
        }

        private async void TapCloseMenu_Tapped(object sender, EventArgs e) {

            await CloseHamburgerMenu();
        }

        private async Task CloseHamburgerMenu() {

            await SlMenu.TranslateTo(-250, 0, 400, Easing.Linear);
            GridOverlay.IsVisible = false;
        }

        protected override async void OnAppearing() {
            base.OnAppearing();
            TotalCartItems response = await ApiServices
                .GetTotalCartItems(Preferences.Get("userId", 0));

            LblTotalItems.Text = response.totalItems.ToString();
        }
        protected override async void OnDisappearing() {
            base.OnDisappearing();

            await CloseHamburgerMenu();
        }

        private async void CvCategories_SelectionChanged(object sender, SelectionChangedEventArgs e) {

            Category selectedItem = e.CurrentSelection.FirstOrDefault() as Category;
            if (selectedItem != null) {

                await Navigation.PushModalAsync(new ProductListPage(selectedItem.id,
                    selectedItem.name));
            }
            ((CollectionView)sender).SelectedItem = null;
        }

        private async void CvProducts_SelectionChanged(object sender, SelectionChangedEventArgs e) {

            PopularProduct SelectedItem = e.CurrentSelection.FirstOrDefault() as PopularProduct;
            if (SelectedItem != null) {
                await Navigation.PushModalAsync(new ProductDetailPage(SelectedItem.id));
                ((CollectionView)sender).SelectedItem = null;
            }
        }

        private async void TapCartIcon_Tapped(object sender, EventArgs e) {

            await Navigation.PushModalAsync(new CartPage());
        }

        private void TapOrders_Tapped(object sender, EventArgs e) {

            Navigation.PushModalAsync(new OrderPage());
        }

        private void TapContact_Tapped(object sender, EventArgs e) {

            Navigation.PushModalAsync(new ContactPage());
        }
    }
}