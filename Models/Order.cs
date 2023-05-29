namespace SimpleStocks.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string OrderStatus { get; set; }

        public decimal Total { get; set; }
    }
}
