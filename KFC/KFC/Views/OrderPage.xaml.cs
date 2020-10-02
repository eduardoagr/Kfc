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
    public partial class OrderPage : ContentPage {

        public ObservableCollection<OrderByUser> orderByUsersCollection;
        public OrderPage() {
            InitializeComponent();
            GetOrders();
            orderByUsersCollection = new ObservableCollection<OrderByUser>();
        }

        private async void GetOrders() {
            int userID = Preferences.Get("userId", 0);
            List<OrderByUser> Orders = await ApiServices.GerOrderByUser(userID);
            orderByUsersCollection.Clear();
            foreach (var item in Orders) {

                orderByUsersCollection.Add(item);
            }

            LvOrders.ItemsSource = orderByUsersCollection;
        }

        private void TapBack_Tapped(object sender, EventArgs e) {

            Navigation.PopModalAsync();
        }

        private void LvOrders_ItemSelected(object sender, SelectedItemChangedEventArgs e) {

            OrderByUser selectedItem = e.SelectedItem as OrderByUser;

            if (selectedItem != null) {

                Navigation.PushModalAsync(new OrderDetailPage(selectedItem.id));

            }

            ((ListView)sender).SelectedItem = null;
        }
    }
}