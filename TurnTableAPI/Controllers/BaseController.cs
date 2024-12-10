using Microsoft.AspNetCore.Mvc;

namespace TurnTableAPI.Controllers
{

    [Route("api")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        [HttpGet]
        public virtual ActionResult<string> HelloWorld()
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
