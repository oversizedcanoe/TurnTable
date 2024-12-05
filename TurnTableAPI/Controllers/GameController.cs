using Microsoft.AspNetCore.Mvc;
using TurnTableApplication.Requests;
using TurnTableBase;
using TurnTableDomain;
using TurnTableAPI.ActionFilters;

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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<string> New([FromBody] NewGameRequest request)
        {
            try
            {
                string gameCode = _gameManager.StartNewGame(request.GameType, request.PlayerOneName);

                return Ok(gameCode);
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);

            }
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidGameCodeFilter))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<bool> Move([FromBody] MoveRequest request)
        {
            try
            {
                bool result = _gameManager.Move(request.GameCode, request.PlayerNumber, request.Arg1, request.Arg2, request.Arg3);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }
    }
}
