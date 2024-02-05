using System.Collections.ObjectModel;
using System.Net.Http.Json;
using CommunityToolkit.Maui.Views;

namespace APİ;


    public partial class MainPage : ContentPage
    {
        public ObservableCollection<Product> Products { get; set; } = new ObservableCollection<Product>();

        public MainPage()
        {
            InitializeComponent();

            Task.Run(async () =>
            {
                try
                {
                    // Ürünleri API'den alıyoruz
                    var products = await GetProducts();

                    // ObservableCollection'a ekliyoruz
                    foreach (var item in products)
                    {


                        Products.Add(item);
                    }




                }
                catch (Exception ex)
                {
                    // Hataları burada ele alıyoruz
                    Console.WriteLine("Hata: " + ex.Message);
                }
            });


            BindingContext = this;


        }

        private async Task<List<Product>> GetProducts()
        {
            // API isteğinde bulunmak için bir HttpClient oluşturuyoruz
            var client = new HttpClient();

            // API'ye GET isteği gönderiyoruz
            var response = await client.GetAsync("https://fakestoreapi.com/products");



            // Cevabın JSON içeriğini ayrıştırıyoruz
            var products = await response.Content.ReadFromJsonAsync<List<Product>>();


            return products;
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is Product product)
            {
                var client = new HttpClient();

                // API'ye GET isteği gönderiyoruz
                var response = await client.GetAsync($"https://fakestoreapi.com/products/{product.id}");



                // Cevabın JSON içeriğini ayrıştırıyoruz
                var single_product = await response.Content.ReadFromJsonAsync<Product>();

                await Navigation.PushAsync(new DetailPage(single_product));
            }
        }

        private async void onDeleteClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.BindingContext is Product product)
            {
            
           
                await onDeleteSelectedItem(product.id);
            }
        }
        private async Task onDeleteSelectedItem(int id)
        {
            

            
            var client = new HttpClient();

            var response = await client.DeleteAsync($"https://fakestoreapi.com/products/{id}");

            if (response.IsSuccessStatusCode)
            {

                    var popup = new SimplePopup();
                    this.ShowPopup(popup);



        }
            else
            {
                await DisplayAlert("DURUM", "SİLME BAŞARISIZ", "TAMAM", "Çık");
            }
        }
    }
    public class Product
    {
        public int id { get; set; }
        public string title { get; set; }
        public string Description { get; set; }
        public string image { get; set; }
    }





