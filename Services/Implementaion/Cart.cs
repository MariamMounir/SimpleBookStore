using SimpleVookStore.Models;
using SimpleVookStore.Repo;

namespace SimpleBookStore.Services.Implementaion
{
    public class Cart
    {
        private readonly GenericRepo<Cart> Cartrepo;
        private readonly GenericRepo<CartItems> CartItemRepo;

        public Cart(GenericRepo<CartItems> _CartItemRepo, GenericRepo<Cart> _Cartrepo)
        {
            CartItemRepo = _CartItemRepo;
            Cartrepo = _Cartrepo;
        }

    }
}
