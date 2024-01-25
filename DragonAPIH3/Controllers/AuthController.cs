using DragonAPIH3.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace DragonAPIH3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {

        private readonly JwtService _jwtService;
        LoginService _loginService = new LoginService();

        public AuthController(JwtService jwtService)
        {
            _jwtService = jwtService;
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(Account account)
        {
            try
            {
                if (string.IsNullOrEmpty(account.Username) || string.IsNullOrEmpty(account.PrivateKey))
                    return BadRequest("Username or password is not");

                bool loginResult = _loginService.VerifyLogin(account);

                if (!loginResult) { return Unauthorized(); }

                string token = _jwtService.GenerateToken(account.Username);
                return Ok(token);
            }
            catch (Exception)
            {
                return BadRequest("Failed login - try again");
            }
        }
    }
}
