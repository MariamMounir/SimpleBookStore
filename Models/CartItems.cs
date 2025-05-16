namespace SimpleVookStore.Models
{
    public class CartItems
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Products Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public int CartId { get; set; }
        public Cart Cart { get; set; }

        public int? CheckoutId { get; set; }  
        public Checkout Checkout { get; set; }
    }
}
