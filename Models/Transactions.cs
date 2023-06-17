namespace SimpleStocks.Models
{
    public class Transactions
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string TransactionType { get; set; }
        public int Quantity { get; set; }
        public int AssetId { get; set; }
        public DateTime DateTime { get; set; }
        public decimal Amount { get; set; } 

    }
}
