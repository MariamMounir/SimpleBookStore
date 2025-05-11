namespace SimpleVookStore.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public List<CartItems> CartItems { get; set; }
    }
}
