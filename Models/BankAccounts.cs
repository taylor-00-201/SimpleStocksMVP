namespace SimpleStocks.Models
{
    public class BankAccounts
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Balance { get; set;}
         
        public int AccountNumber { get; set; }

        public int RoutingNuber { get; set; }

        public string BankName { get; set; }
    }
}
