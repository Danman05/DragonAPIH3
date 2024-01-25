using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;
namespace DragonAPIH3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SongController : Controller
    {
        [HttpGet("dragontunes")]
        [Authorize]
        public IActionResult Index()
        {
            if (System.IO.File.Exists(""))
            {
                
            }
        }


        [HttpGet("Public")]

        public IActionResult PublicEndpoint()
        {
            return Ok($"Hello {HttpContext.User.Identity.Name}");
        }
    }
}
