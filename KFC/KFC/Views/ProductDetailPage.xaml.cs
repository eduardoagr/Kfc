using KFC.Models;
using KFC.Services;

using System;

using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KFC.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductDetailPage : ContentPage {

        private int productID;

        public ProductDetailPage(int productID) {
            InitializeComponent();
            GetProductDetail(productID);

            this.productID = productID;
        }

        private async void GetProductDetail(int productID) {

            Product product = await ApiServices.GetProductById(productID);
            ImgProduct.Source = product.FullImageUrl;
            LblDetail.Text = product.detail;
            LblName.Text = product.name;
            LblPrice.Text = product.price.ToString();
            LblTotalPrice.Text = LblPrice.Text;
        }

        private void TapBack_Tapped(object sender, EventArgs e) {

            Navigation.PopModalAsync();
        }

        private void TapDecrement_Tapped(object sender, EventArgs e) {

            int i = Convert.ToInt16(LblQty.Text);
            i--;
            if (i < 1) {
                return;
            }
            LblQty.Text = i.ToString();
            LblTotalPrice.Text = (Convert.ToInt16(LblQty.Text) * Convert.ToInt16(LblPrice.Text))
                .ToString();
        }

        private void TapIncrement_Tapped(object sender, EventArgs e) {

            int i = Convert.ToInt16(LblQty.Text);
            i++;
            LblQty.Text = i.ToString();
            LblTotalPrice.Text = (Convert.ToInt16(LblQty.Text) * Convert.ToInt16(LblPrice.Text))
                .ToString();
        }

        private async void BtnAddToCart_Clicked(object sender, EventArgs e) {

            AddToCart addToCart = new AddToCart {
                Qty = LblQty.Text,
                Price = LblPrice.Text,
                TotalAmount = LblTotalPrice.Text,
                ProductId = productID,
                CustomerId = Preferences.Get("userId", 0)
            };

            bool response = await ApiServices.AddItemsToCart(addToCart);
            if (response) {
                await DisplayAlert("Congratulations", "Your item has been successfully added", "OK");
                await Navigation.PopModalAsync();
            } else {
                await DisplayAlert("Error", "Something went wrong", "OK");
            }
        }
    }
}