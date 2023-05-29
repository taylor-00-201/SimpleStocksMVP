﻿namespace SimpleStocks.Models.UserLogin
{
    public class UpdateStockUserModel
    {
         
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsAdmin { get; set; }
        public string PasswordHash { get; set; }
    
    }
}
