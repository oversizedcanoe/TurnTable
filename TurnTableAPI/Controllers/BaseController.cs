using Microsoft.AspNetCore.Mvc;

namespace TurnTableAPI.Controllers
{

    [Route("api/[action]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            return Ok("Hello, world!");
        }
    }
}
