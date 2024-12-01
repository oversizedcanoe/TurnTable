using Microsoft.AspNetCore.Mvc;
using TurnTableApplication.Requests;

namespace TurnTableAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly ILogger<GameController> _logger;

        public GameController(ILogger<GameController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public void New([FromBody] NewGameRequest request)
        {

        }
    }
}
