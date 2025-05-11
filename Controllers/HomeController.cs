using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SimpleVookStore.Models;
using SimpleVookStore.Repo;
using SimpleVookStore.Services.Implementaion;
using SimpleVookStore.Services.Interface;

namespace SimpleVookStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService productService;

        public HomeController(ILogger<HomeController> logger, IProductService _productService)
        {
            _logger = logger;
            productService = _productService;
        }

        public IActionResult Index()
        {
            var products = productService.GetAllProducts();
            return View(products);
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
