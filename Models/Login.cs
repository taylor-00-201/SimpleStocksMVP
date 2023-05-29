using System.Diagnostics.Contracts;

namespace SimpleStocks.Models
{
    public class Login
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }

    }
}
