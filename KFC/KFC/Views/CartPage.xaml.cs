using KFC.Models;
using KFC.Services;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KFC.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartPage : ContentPage {

        public ObservableCollection<ShppingCartItems> ShoppingCartItemsCollection;
        public CartPage() {

            ShoppingCartItemsCollection = new ObservableCollection<ShppingCartItems>();
            GetShoppingCartItems();
            GetTotalPrice();
            InitializeComponent();
        }

        private async void GetTotalPrice() {
            var userId = Preferences.Get("userId", 0);
            CartSubTotal total = await ApiServices.GetCartSubTotal(userId);
            LblTotalPrice.Text = total.subTotal.ToString();
        }

        private async void GetShoppingCartItems() {

            int userID = Preferences.Get("userId", 0);
            List<ShppingCartItems> CartItems = await ApiServices.GetShoppingCartItems(userID);
            ShoppingCartItemsCollection.Clear();
            foreach (var item in CartItems) {
                ShoppingCartItemsCollection.Add(item);
            }
            LvShoppingCart.ItemsSource = ShoppingCartItemsCollection;
        }

        private async void TapBack_Tapped(object sender, EventArgs e) {

            await Navigation.PopModalAsync();
        }

        private async void TapClearCart_Tapped(object sender, EventArgs e) {

            int userID = Preferences.Get("userId", 0);

            string isShoppingCartCleared = await DisplayActionSheet
                ("Question: Would you like to cleared the cart",
                 "Yes",
                 "No");

            if (isShoppingCartCleared.Equals("Yes")) {
                await ApiServices.ClearShoppingCart(userID);
                LvShoppingCart.ItemsSource = null;
                LblTotalPrice.Text = "0";
            }
        }

        private async void BtnProceed_Clicked(object sender, EventArgs e) {
            if (ShoppingCartItemsCollection.Count == 0) {

                await DisplayAlert("Error", "You do not have any items in your cart", "OK");

                return;
            }
            await Navigation.PushModalAsync(new PlaceOrderPage(Convert.ToDouble(LblTotalPrice.Text)));
        }
    }
}