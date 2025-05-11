namespace SimpleVookStore.Models
{
    public class Checkout
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string ShippingAddress { get; set; }
        public string Status { get; set; }
        public List<CartItems> CartItems { get; set; }
    }
}
