using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using SimpleBookStore.Services.Interface;
using SimpleVookStore.Data.Migrations;
using SimpleVookStore.Models;
using SimpleVookStore.Repo;

namespace SimpleBookStore.Services.Implementaion
{
    public class CheckoutService:ICheckoutService
    {
        private readonly GenericRepo<Checkout> checkoutRepo;
        private readonly ICartService cartItemService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public CheckoutService(GenericRepo<Checkout> _checkoutRepo, IHttpContextAccessor _httpContextAccessor,ICartService _cartService)
        {
            checkoutRepo = _checkoutRepo;
            httpContextAccessor = _httpContextAccessor;
            cartItemService = _cartService;
        }


        public int CreateCheckout(string address)
        {
            
            var cartItems = cartItemService.GetCartItems();
            if (cartItems == null || !cartItems.Any())
                throw new Exception("Cart is empty.");

            var userId = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                throw new Exception("User not found.");


            decimal totalAmount = cartItems.Sum(item => item.Price * item.Quantity);

          
            var checkout = new Checkout
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                TotalAmount = totalAmount,
                ShippingAddress = address,
                Status = "Pending",
                CartItems = cartItems.ToList()
            };


            checkoutRepo.Add(checkout);
            checkoutRepo.save();
            foreach (var item in cartItems)
            {
                item.CheckoutId = checkout.Id;
            }

            checkoutRepo.save();

            cartItemService.ClearCart();

            return checkout.Id;
        }

    }
}
