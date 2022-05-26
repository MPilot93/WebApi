using Microsoft.AspNetCore.Mvc;
using Product.Api.SQL;
using Product.Api.Models;
using System.Diagnostics;
using Cart.Api.Models;
using System.Text.Json;
using Web.Graphic.Models;

namespace Web.Graphic.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _client;
        public string Message { get; set; }


        public HomeController(ILogger<HomeController> logger, IHttpClientFactory client)
        {
            _logger = logger;
            _client = client;
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            var client = _client.CreateClient("ProductApi");
            Message = await client.GetStringAsync("Product/List");
            var M= JsonSerializer.Deserialize<IEnumerable<ProductDto>>(Message);
            return View(M);
        }

        [HttpGet]
        public async Task<IActionResult> CartAsync()
        {
            var client = _client.CreateClient("CartApi");
            Message = await client.GetStringAsync("Cart/List");
            var M = JsonSerializer.Deserialize<IEnumerable<CartDto>>(Message);
            return View(M);
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> Add(ProductDto product)
        {
            var client = _client.CreateClient("ProductApi");

            var x  = await client.PutAsJsonAsync<ProductDto>("Product/Add",  product);
            x.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
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