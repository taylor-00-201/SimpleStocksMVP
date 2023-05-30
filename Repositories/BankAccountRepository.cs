using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleStocks.Interfaces;
using System.Net.WebSockets;
using SimpleStocks.Models;

namespace simplestocks.repositories
{
    public class bankaccountrepository
    { 
        private readonly IConfiguration _configuration;

        public bankaccountrepository(IConfiguration configuration) 
        {
            _configuration = configuration;
        }
    }
}
