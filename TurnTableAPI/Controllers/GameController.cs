using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TurnTableAPI.ActionFilters;
using TurnTableApplication.DTOs;
using TurnTableApplication.Requests;
using TurnTableDomain.Models;
using TurnTableDomain.Services;

namespace TurnTableAPI.Controllers
{
    [Route("api/game")]
    [ApiController]
    public class GameController : BaseController
    {
        private readonly ILogger<GameController> _logger;
        private readonly GameManager _gameManager;

        public GameController(ILogger<GameController> logger, GameManager gameManager)
        {
            this._logger = logger;
            this._gameManager = gameManager;
        }

        [HttpPost("new")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<NewGameDTO> New([FromBody] NewGameRequest request)
        {
            try
            {
                string gameCode = _gameManager.StartNewGame(request.GameType, request.PlayerOneName);

                return Ok(new NewGameDTO(gameCode));
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("join")]
        [ServiceFilter(typeof(ValidGameCodeFilter))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JoinedGameDTO>> Join([FromBody] JoinGameRequest request)
        {
            try
            {
                int playerNumber = await _gameManager.JoinGame(request.GameCode, request.PlayerName);

                return Ok(new JoinedGameDTO(playerNumber));
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("move")]
        [ServiceFilter(typeof(ValidGameCodeFilter))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Move([FromBody] MoveRequest request)
        {
            try
            {
                await _gameManager.Move(request.GameCode, request.PlayerNumber, request.Arg1, request.Arg2, request.Arg3);

                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("playagain")]
        [ServiceFilter(typeof(ValidGameCodeFilter))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> PlayAgain([FromBody] PlayAgainRequest request)
        {
            try
            {
                await _gameManager.PlayAgain(request.GameCode, request.PlayerNumber);

                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{gameCode}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<GameDTO> Get([FromRoute] string gameCode)
        {
            try
            {
                Game? game = _gameManager.FindGameOrDefault(gameCode);

                if (game == null)
                {
                    return NotFound();
                }

                return Ok(GameDTO.FromGame(game, gameCode));
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [NonAction]
        public override ActionResult<string> HelloWorld()
        {
            return base.HelloWorld();
        }
    }
}
