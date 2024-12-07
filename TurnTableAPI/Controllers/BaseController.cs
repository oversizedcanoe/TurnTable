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

        [NonAction]
        protected string GetBackendAddress()
        {
            string requestOriginHeader = this.HttpContext.Request.Headers["Origin"]!;

            UriBuilder builder = new UriBuilder(requestOriginHeader);
            int backendPort = this.HttpContext.Connection.LocalPort;
            builder.Port = backendPort; 

            return builder.ToString();
        }

    }
}
