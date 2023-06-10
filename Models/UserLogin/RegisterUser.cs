namespace SimpleStocks.Models.UserLogin
{
    public class RegisterUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsAdmin { get; set; }
        public string? AddressLineOne { get; set; }
        public string? AddressLineTwo { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string Zip { get; set; }
        public string PasswordHash { get; set; }

        public decimal Balance { get; set; } 
    }
}
