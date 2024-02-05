using System.Net.Http.Json;
using APİ;

namespace APİ;

public partial class DetailPage : ContentPage
{



    public DetailPage(Product p)
    {
        InitializeComponent();

       

        this.BindingContext = p;

        Console.WriteLine(p.id);






    }


}
