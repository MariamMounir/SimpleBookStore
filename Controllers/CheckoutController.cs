using Microsoft.AspNetCore.Mvc;
using SimpleBookStore.Services.Interface;

namespace SimpleBookStore.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ICheckoutService checkoutService;
        private readonly ICartService cartService;

        public CheckoutController(ICartService _cartService, ICheckoutService _checkoutService)
        {
            cartService = _cartService;
            checkoutService = _checkoutService;
        }
        public IActionResult Index()
        {
            var CartItems = cartService.GetCartItems();
            return View(CartItems);
        }
        [HttpPost("/Checkout/CreateCheckout")]
        public IActionResult CreateCheckout(string address)
        {
            if (string.IsNullOrEmpty(address))
            {
                return BadRequest("Address is required");
            }

            checkoutService.CreateCheckout(address);

            return Ok();

        }
    }
}
