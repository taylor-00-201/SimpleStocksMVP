using Microsoft.AspNetCore.Cors;
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

        
        [HttpPost("LoginUser")]
        public IActionResult LoginWithCredientials([FromBody] LoginRequest loginRequest)
        {
            var logInResponse = _LoginRepo.LoginWithCredentials(loginRequest);



            Console.WriteLine(loginRequest);

            if (loginRequest == null)
            {
                return NotFound();
            }

            CookieOptions cookie = new CookieOptions
            {
                HttpOnly = false,
                Secure = true,
                Expires = DateTime.UtcNow.AddDays(2),
                SameSite = SameSiteMode.None,
            };


            Console.WriteLine(loginRequest);


            var responseBody = new ResponseBody()
            {
                StatusCode = 200,
                StatusText = "OK",
                User = logInResponse
            };

            Response.Cookies.Append("authCookie", $"{loginRequest.Email}", cookie);
           


            return Ok(responseBody);
        }


        [HttpPut("Update")]
        public IActionResult UpdateCredentials(LoginRequest loginRequest)
        {
            _LoginRepo.UpdateCredentials(loginRequest);
            return Ok();
        }



    }
}
