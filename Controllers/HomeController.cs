using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SimpleVookStore.Models;

using SimpleVookStore.Repo;
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


        public IActionResult Index(int page = 1, string sortOrder = "")
        {
            int pageSize = 8;

            var allProducts = productService.GetAllProducts(); 
            switch (sortOrder)
            {
                case "name_desc":
                    allProducts = allProducts.OrderByDescending(p => p.Name).ToList();
                    break;
                case "price_asc":
                    allProducts = allProducts.OrderBy(p => p.Price).ToList();
                    break;
                case "price_desc":
                    allProducts = allProducts.OrderByDescending(p => p.Price).ToList();
                    break;
                default:
                    allProducts = allProducts.OrderBy(p => p.Name).ToList();
                    break;
            }

            var totalProducts = allProducts.Count;
            var paginated = allProducts
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var viewModel = new ProductListViewModel
            {
                Products = paginated,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(totalProducts / (double)pageSize)
            };

            ViewData["CurrentSort"] = sortOrder;
            return View(viewModel);
        }


        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
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
