using Microsoft.AspNetCore.Mvc;
using DragonAPIH3.Services;
using DragonAPIH3.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace DragonAPIH3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {

        private readonly IJwtService _jwtService;
        LoginService _loginService = new LoginService();

        public AuthController(IJwtService jwtService)
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
