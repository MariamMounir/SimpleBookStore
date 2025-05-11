namespace SimpleVookStore.Models
{
    public class CartItems
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Products Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
