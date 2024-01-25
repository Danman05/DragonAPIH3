using DragonAPIH3.Models;
using Microsoft.AspNetCore.Mvc;

namespace DragonAPIH3.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class AccountController : Controller
    {
        private readonly AccountData _accData = new AccountData();

        [HttpPost]
        public IActionResult Register(Account account)
        {
            try
            {
                // Check input
                if (string.IsNullOrEmpty(account.Username) || string.IsNullOrEmpty(account.PrivateKey))
                    return BadRequest("Values cannot be empty");

                // Attempt add account
                bool accountCreatedSuccessfully = _accData.AddAccount(account);
                if (accountCreatedSuccessfully)
                    return Ok("Account has been created");
                else
                    return BadRequest("Failed to create account - Username is taken");
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong ;)");
            }
        }
    }
}
