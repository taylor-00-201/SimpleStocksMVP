namespace SimpleStocks.Models
{
    public class Asset
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }

        public decimal CurrentPrice { get; set; }
    }
}
