using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using SimpleBookStore.Services.Interface;
using SimpleVookStore.Models;
using SimpleVookStore.Repo;

namespace SimpleBookStore.Services.Implementaion
{
    public class CartService:ICartService
    {
        private readonly GenericRepo<Cart> Cartrepo;
        private readonly GenericRepo<CartItems> CartItemRepo;
        private readonly GenericRepo<Products> ProductRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public CartService(GenericRepo<CartItems> _CartItemRepo, GenericRepo<Cart> _Cartrepo,GenericRepo<Products> _ProductRepo, IHttpContextAccessor httpContextAccessor)
        {
            CartItemRepo = _CartItemRepo;
            Cartrepo = _Cartrepo;
            ProductRepo = _ProductRepo;
            _httpContextAccessor = httpContextAccessor;
        }
        //add to cart
        public void AddToCart(int productId, int quantity=1)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                throw new Exception("User not found");
            }
            var cartId = Cartrepo.GetAll().FirstOrDefault(c => c.UserId == userId).Id;
            var product = ProductRepo.GetById(productId);
            if (product == null)
            {
                throw new Exception("Product not found");
            }

            var existingCartItem = CartItemRepo.GetAll().FirstOrDefault(ci => ci.CartId == cartId && ci.ProductId == productId);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity += quantity;
                existingCartItem.Price = existingCartItem.Quantity * product.Price;
                CartItemRepo.Edit(existingCartItem);
            }
            else
            {
                var cartItem = new CartItems
                {
                    ProductId = productId,
                    Quantity = quantity,
                    Price = product.Price * quantity,
                    CartId = cartId
                };
                CartItemRepo.Add(cartItem);
            }

            CartItemRepo.save();
        }
        //get cart items
        public List<CartItems> GetCartItems()
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                throw new Exception("User not found");
            }
            var cartId = Cartrepo.GetAll()?.FirstOrDefault(c => c.UserId == userId)?.Id;
            var cartItems = CartItemRepo.GetAll()
                    .Include(ci => ci.Product)
                    .Where(ci => ci.CartId == cartId)
                    .ToList();
            return cartItems;
        }
        //remove from cart
        public void RemoveFromCart(int cartItemId)
        {
            var cartItem = CartItemRepo.GetById(cartItemId);
            if (cartItem == null)
            {
                throw new Exception("Cart item not found");
            }
            CartItemRepo.Delete(cartItemId);
            CartItemRepo.save();
        }
        //clear cart
        public void ClearCart()
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                throw new Exception("User not found");
            }
            var cartId = Cartrepo.GetAll()?.FirstOrDefault(c => c.UserId == userId)?.Id;
            var cartItems = CartItemRepo.GetAll().Where(ci => ci.CartId == cartId).ToList();
            foreach (var item in cartItems)
            {
                CartItemRepo.Delete(item.Id);
            }
            CartItemRepo.save();
        }
        //getcartitembyid
        public CartItems GetCartItemById(int cartItemId)
        {
            var cartItem = CartItemRepo.GetAll()
                .Include(ci => ci.Product)
                .FirstOrDefault(ci => ci.Id == cartItemId);
            if (cartItem == null)
            {
                throw new Exception("Cart item not found");
            }
            return cartItem;
        }
        //update quantity
        public void UpdateCartItem(CartItems item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Cart item cannot be null");
            }

            var existingItem = CartItemRepo.GetById(item.Id);
            if (existingItem == null)
            {
                throw new Exception("Cart item not found");
            }

            existingItem.Quantity = item.Quantity;
            existingItem.Price = item.Quantity * existingItem.Product.Price;

            CartItemRepo.Edit(existingItem);
            CartItemRepo.save();
        }



    }
}
