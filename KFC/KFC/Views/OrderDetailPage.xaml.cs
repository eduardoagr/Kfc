using KFC.Models;
using KFC.Services;

using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KFC.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderDetailPage : ContentPage {

        public ObservableCollection<OrderDetail> orderDetailsColloction;
        public OrderDetailPage(int OrderID) {
            InitializeComponent();

            orderDetailsColloction = new ObservableCollection<OrderDetail>();
            GetOrderDetails(OrderID);
        }

        private async void GetOrderDetails(int orderID) {
            var order = await ApiServices.GetOrderDettails(orderID);
            var ordeDetail = order[0].orderDetails;
            orderDetailsColloction.Clear();
            foreach (var item in ordeDetail) {

                orderDetailsColloction.Add(item);
            }

            LvOrderDetail.ItemsSource = orderDetailsColloction;
            LblTotalPrice.Text = order[0].orderTotal + " $ ";
        }

        private void TapBack_Tapped(object sender, System.EventArgs e) {

            Navigation.PopModalAsync();
        }
    }
}