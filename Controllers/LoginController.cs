using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleStocks.Interfaces;
using SimpleStocks.Models.UserLogin;

namespace SimpleStocks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly ILoginRepository _LoginRepo;

        public LoginController(ILoginRepository LoginRepo)
        {
            _LoginRepo = LoginRepo;
        }

        [HttpPost("Login")]
        public IActionResult LoginWithCredientials(LoginRequest loginRequest)
        {
            var logInUser = _LoginRepo.LoginWithCredentials(loginRequest);
            return Ok(logInUser);
        }

        [HttpPut("Update")]
        public IActionResult UpdateCredentials(LoginRequest loginRequest) 
        {
            _LoginRepo.UpdateCredentials(loginRequest);
            return Ok();
        }



    }
}
