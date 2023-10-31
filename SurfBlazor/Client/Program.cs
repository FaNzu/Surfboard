using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SurfBlazor;
using SurfBlazorLibrary.Product;
using SurfBlazorLibrary.ShoppingCart;
using SurfBlazorLibrary.Storage;

namespace SurfBlazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddSingleton<IStorageService, StorageService>();
            builder.Services.AddSingleton<IShoppingCartService, ShoppingCartService>();
            builder.Services.AddTransient<IProductService, ProductService>();

            await builder.Build().RunAsync();
        }
    }
}