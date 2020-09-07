using KFC.Models;

using Newtonsoft.Json;

using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Essentials;

namespace KFC.Services {
    public static class ApiServices {

        public static async Task<bool> RegisterUser(string name, string email, string password) {

            Register register = new Register() {
                Name = name,
                Email = email,
                Password = password
            };
            using (HttpClient httpClient = new HttpClient()) {

                var userJson = JsonConvert.SerializeObject(register);
                var conten = new StringContent(userJson, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync($"{AppSettings.APIURL}api/Accounts/Register", conten);
                if (!response.IsSuccessStatusCode) {
                    return false;
                } else {
                    return true;
                }
            }
        }
        public static async Task<bool> LoginUser(string email, string password) {

            Login login = new Login() {
                Email = email,
                Password = password
            };
            using (HttpClient httpClient = new HttpClient()) {

                var userJson = JsonConvert.SerializeObject(login);
                var conten = new StringContent(userJson, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync($"{AppSettings.APIURL}api/Accounts/Login", conten);
                if (response.IsSuccessStatusCode) {
                    var JsonResult = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<Token>(JsonResult);
                    Preferences.Set("token", result.access_token);
                    Preferences.Set("userId", result.user_Id);
                    Preferences.Set("userName", result.user_name);
                    return true;
                } else {
                    return false;
                }
            }
        }
        public static async Task<List<Category>> GetCategories() {

            using (HttpClient httpClient = new HttpClient()) {

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("token", string.Empty));
                var response = await httpClient.GetStringAsync($"{AppSettings.APIURL}api/Categories");
                return JsonConvert.DeserializeObject<List<Category>>(response);
            }
        }

        public static async Task<Product> GetProductById(int productID) {

            using (HttpClient httpClient = new HttpClient()) {

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("token", string.Empty));
                var response = await httpClient.GetStringAsync($"{AppSettings.APIURL}api/Products/{productID}");
                return JsonConvert.DeserializeObject<Product>(response);
            }
        }

        public static async Task<List<ProductByCategory>> GetProductByCategory(int categoryID) {

            using (HttpClient httpClient = new HttpClient()) {

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("token", string.Empty));
                var response = await httpClient.GetStringAsync($"{AppSettings.APIURL}api/Products/ProductsByCategory/{categoryID}");
                return JsonConvert.DeserializeObject<List<ProductByCategory>>(response);
            }
        }

        public static async Task<List<PopularProduct>> GetPopularProducts() {

            using (HttpClient httpClient = new HttpClient()) {

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("token", string.Empty));
                var response = await httpClient.GetStringAsync($"{AppSettings.APIURL}api/Products/PopularProducts");
                return JsonConvert.DeserializeObject<List<PopularProduct>>(response);
            }
        }

        public static async Task<bool> AddItemsToCart(AddToCart addToCart) {

            using (HttpClient httpClient = new HttpClient()) {

                var userJson = JsonConvert.SerializeObject(addToCart);
                var conten = new StringContent(userJson, Encoding.UTF8, "application/json");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("token", string.Empty));
                var response = await httpClient.PostAsync($"{AppSettings.APIURL}api/ShoppingCartItems", conten);
                if (!response.IsSuccessStatusCode) {
                    return false;
                } else {
                    return true;
                }
            }
        }

        public static async Task<CartSubTotal> GetCartSubTotal(int userID) {

            using (HttpClient httpClient = new HttpClient()) {

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("token", string.Empty));
                var response = await httpClient.GetStringAsync($"{AppSettings.APIURL}api/ShoppingCartItems/SubTotal/{userID}");
                return JsonConvert.DeserializeObject<CartSubTotal>(response);
            }
        }

        public static async Task<List<ShppingCartItems>> GetShoppingCartItems(int userID) {

            using (HttpClient httpClient = new HttpClient()) {

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("token", string.Empty));
                var response = await httpClient.GetStringAsync($"{AppSettings.APIURL}api/ShoppingCartItems/{userID}");
                return JsonConvert.DeserializeObject<List<ShppingCartItems>>(response);
            }
        }

        public static async Task<TotalCartItems> GetTotalCartItems(int userID) {

            using (HttpClient httpClient = new HttpClient()) {

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("token", string.Empty));
                var response = await httpClient.GetStringAsync($"{AppSettings.APIURL}api/ShoppingCartItems/TotalItems/{userID}");
                return JsonConvert.DeserializeObject<TotalCartItems>(response);
            }
        }


        public static async Task<bool> ClearShoppingCart(int userID) {

            using (HttpClient httpClient = new HttpClient()) {

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("token", string.Empty));
                var response = await httpClient.DeleteAsync($"{AppSettings.APIURL}api/ShoppingCartItems/{userID}");
                if (response.IsSuccessStatusCode) {
                    return true;
                }
                return false;
            }
        }

        public static async Task<OrderResponce> PlceOrder(Order order) {

            using (HttpClient httpClient = new HttpClient()) {

                var userJson = JsonConvert.SerializeObject(order);
                var conten = new StringContent(userJson, Encoding.UTF8, "application/json");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("token", string.Empty));
                var response = await httpClient.PostAsync($"{AppSettings.APIURL}api/Orders", conten);
                var JsonResult = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<OrderResponce>(JsonResult);
            }
        }

        public static async Task<List<OrderByUser>> GerOrderByUser(int userID) {

            using (HttpClient httpClient = new HttpClient()) {

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("token", string.Empty));
                var response = await httpClient.GetStringAsync($"{AppSettings.APIURL}api/Orders/OrdersByUser/{userID}");
                return JsonConvert.DeserializeObject<List<OrderByUser>>(response);
            }
        }

        public static async Task<Order> GetOrderDettails(Order orderId) {

            using (HttpClient httpClient = new HttpClient()) {


                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("token", string.Empty));
                var response = await httpClient.GetStringAsync($"{AppSettings.APIURL}api/Orders/OrderDetails/");
                return JsonConvert.DeserializeObject<Order>(response);
            }
        }
    }
}
