using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DragonAPIH3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SongController : Controller
    {

        private string _textLink = "https://raw.githubusercontent.com/Danman05/DragonAPIH3/master/DragonAPIH3/DragonMusic.txt";

        [HttpGet("dragontunes")]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            try
            {
                string result = await new HttpClient().GetStringAsync(_textLink);
                if (string.IsNullOrEmpty(result))
                {
                    return NotFound("Could not find text file");
                }
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest("Error getting playlist ;(");
            }

        }


        [HttpGet("Public")]

        public IActionResult PublicEndpoint()
        {
            return Ok($"Hello {HttpContext.User.Identity.Name}");
        }
    }
}
