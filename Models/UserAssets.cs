using System.Security.Policy;

namespace SimpleStocks.Models
{
    public class UserAssets
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AssetId { get; set; }
        public int Quantity { get; set; }
    }
}
