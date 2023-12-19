using Jwt.WebMvc.Models;
using Jwt.WebMvc.Models.ViewModels.ProductViewModels;
using Jwt.WebMvc.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;
using System.Diagnostics;

namespace Jwt.WebMvc.Controllers
{
    public class HomeController : Controller
    {

        private HubConnection? connection = null;
        private string url = $"{SocketEndpoints.BaseUrl}{SocketEndpoints.Product.getProducts}";

        public HomeController()
        {

            connection = new HubConnectionBuilder()
                .WithUrl(url)
                .WithAutomaticReconnect()
                .Build();

        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<GetProductResponse> _products = null;

            // Bağlantıyı sağlıyoruz
            await connection.StartAsync();

            // Burada önce event'ı dinliyoruz
            connection.On<IEnumerable<GetProductResponse>>("getProducts", (products) =>
            {
                _products = products;
            });


            // Dinlediğmiz event'i tetikliyoruz
            await connection.InvokeAsync("GetProductsSocket");

            return View(_products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}