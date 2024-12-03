using Microsoft.AspNetCore.Mvc;
using TurnTableApplication.Requests;
using TurnTableDomain;

namespace TurnTableAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly ILogger<GameController> _logger;
        private readonly GameManager _gameManager;

        public GameController(ILogger<GameController> logger, GameManager gameManager)
        {
            this._logger = logger;
            this._gameManager = gameManager;
        }

        [HttpPost]
        public string New([FromBody] NewGameRequest request)
        {
            return _gameManager.StartNewGame(request.GameType, request.PlayerOneName);
        }
    }
}
