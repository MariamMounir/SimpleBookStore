using SimpleVookStore.Models;

namespace SimpleBookStore.Services.Interface
{
    public interface ICartService
    {
        public void AddToCart(int productId, int quantity = 1);
        public List<CartItems> GetCartItems();
        public void RemoveFromCart(int cartItemId);
        public void ClearCart();
        public CartItems GetCartItemById(int cartItemId);
        public void UpdateCartItem(CartItems item);



    }
}
