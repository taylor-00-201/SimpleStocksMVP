using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleStocks.Interfaces;
using System.Net.WebSockets;
using SimpleStocks.Models.UserLogin;

namespace SimpleStocks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockUserController : ControllerBase
    {
        private readonly IStockUserRepository _StockUserRepo;

        public StockUserController (IStockUserRepository stockUserRepo)
        {
            _StockUserRepo = stockUserRepo;
        }

        [HttpGet("allusers")]
        public IActionResult GetAllStockUsers()
        {
            var returnedUseres = _StockUserRepo.GetAllStockUsers();

            return Ok(returnedUseres);
        }

        [HttpGet("getuserbyid/{Id}")]
        public IActionResult GetStockUserById(int Id) 
        {
           var returnedUserById = _StockUserRepo.GetStockUserById(Id);
           
           return Ok(returnedUserById);
        }

        [HttpPost("registeruser")]
        public IActionResult RegisterUser(RegisterUser registerUser)
        {
            _StockUserRepo.RegisterUser(registerUser);
            return NoContent();

        }


        //[HttpPut("updateuser")]
        //public IActionResult UpdateUser(UpdateStockUserModel userModel) 
        //{
        //    _StockUserRepo.UpdateUser(userModel);
        //    return Ok();
        //}


        [HttpDelete("deleteuser/{UserId}")]
        public IActionResult DeleteUserById(int UserId)
        {
            _StockUserRepo.DeleteUserById(UserId);
            return Ok();
        }
    }
}
