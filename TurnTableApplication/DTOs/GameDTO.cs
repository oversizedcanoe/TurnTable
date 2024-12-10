using TurnTableBase;
using TurnTableDomain.Models;

namespace TurnTableApplication.DTOs
{
    public class GameDTO
    {
        public required string GameCode { get; set; }

        public GameType GameType { get; set; }

        public required List<PlayerDTO> Players { get; set; }

        public required object GameState { get; set; }

        public int CurrentPlayerTurn { get; set; }

        public int? PlayerWinner { get; set; }

        public bool GameOver { get; set; }

        public static GameDTO FromGame(Game game, string gameCode)
        {
            return new GameDTO()
            {
                GameCode = gameCode,
                GameType = game.GameType,
                Players = game.Players.Select(p => PlayerDTO.FromPlayer(p)).ToList(),
                GameState = game.GameState,
                CurrentPlayerTurn = game.CurrentPlayerTurn,
                PlayerWinner = game.PlayerWinner,
                GameOver =game.GameOver
            };
        }
    }
}
