using KFC.Models;
using KFC.Services;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KFC.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductListPage : ContentPage {

        public ObservableCollection<ProductByCategory> productByCategoriesCollection;

        public ProductListPage(int CatId, string CatName) {
            InitializeComponent();
            productByCategoriesCollection = new ObservableCollection<ProductByCategory>();
            LblCategoryName.Text = CatName;
            GetProductByCategoryId(CatId);
        }

        private async void GetProductByCategoryId(int id) {
            List<ProductByCategory> products = await ApiServices.GetProductByCategory(id);

            productByCategoriesCollection.Clear();
            foreach (var item in products) {
                productByCategoriesCollection.Add(item);
            }
            CvProducts.ItemsSource = productByCategoriesCollection;
        }

        private async void TapBack_Tapped(object sender, EventArgs e) {

            await Navigation.PopModalAsync();
        }

        private void CvProducts_SelectionChanged(object sender, SelectionChangedEventArgs e) {

            ProductByCategory SelectedItem = e.CurrentSelection.FirstOrDefault() as ProductByCategory;
            if (SelectedItem != null) {
                Navigation.PushModalAsync(new ProductDetailPage(SelectedItem.id));
                ((CollectionView)sender).SelectedItem = null;
            }
        }
    }
}