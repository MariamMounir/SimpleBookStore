using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using SimpleBookStore.Services.Implementaion;
using SimpleBookStore.Services.Interface;
using SimpleVookStore.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SimpleBookStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService cartService;
        public CartController(ICartService _cartService)
        {
            cartService = _cartService;
        }
        public IActionResult Index()
        {
            var cartItems = cartService.GetCartItems();
            if (cartItems == null)
            {
                cartItems = new List<CartItems>(); 
            }
            return View(cartItems);
        }

        [HttpPost]
        public IActionResult AddToCart(int productId)
        {
            try
            {
                cartService.AddToCart(productId, 1);
                return Json(new { success = true, message = "Product added to cart successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error: " + ex.Message });
            }
        }

        [HttpPost]
        public IActionResult UpdateQuantity(int cartItemId, int newQuantity)
        {
            try
            {
                var item = cartService.GetCartItemById(cartItemId);
                if (item == null)
                {
                    return BadRequest(new { success = false, message = "Item not found" });
                }

                // تحديث الكمية
                item.Quantity = newQuantity;
                cartService.UpdateCartItem(item);

                return Json(new { success = true, message = "Quantity updated successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int cartItemId)
        {
            try
            {
                var item = cartService.GetCartItemById(cartItemId);
                if (item == null)
                {
                    return BadRequest(new { success = false, message = "Item not found" });
                }
                cartService.RemoveFromCart(cartItemId);
                return Json(new { success = true, message = "Item removed successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}