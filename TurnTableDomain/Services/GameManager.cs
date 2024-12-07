using System.Text;
using TurnTableBase;
using TurnTableDomain.Games.LinkFour;
using TurnTableDomain.Models;

namespace TurnTableDomain.Services
{
    public class GameManager
    {
        public List<Game> Games { get; set; }

        private const string GAME_CODE_CHARS = "ABCDEFGHJKMNPQRSTUVWXYZ";

        public GameManager()
        {
            Games = new List<Game>();
        }

        public string StartNewGame(GameType gameType, string playerOneName, string backendUrl)
        {
            Game game;

            string gameCode = GenerateGameCode();
            string updateEndpoint = GenerateUpdateEndpoint(backendUrl, gameCode);
            Player player = new Player(1, playerOneName);

            switch (gameType)
            {
                case GameType.LinkFour:
                    game = new LinkFour(gameCode, player, updateEndpoint);
                    break;
                default:
                    throw new Exception($"Unknown GameType passed: {gameType}");
            }

            Games.Add(game);

            return gameCode;
        }

        public string JoinGame(string gameCode, string playerName)
        {
            Game game = FindGame(gameCode);

            game.AddPlayer(playerName);

            return game.UpdateEndpoint;
        }


        public MoveResultCode Move(string gameCode, int playerNumber, object arg1, object arg2, object arg3)
        {
            Game game = FindGame(gameCode);

            return game.NewMove(playerNumber, arg1, arg2, arg3);
        }

        public bool IsGameActive(string gameCode)
        {
            return Games.Any(g => g.GameCode == gameCode);
        }

        private Game FindGame(string gameCode)
        {
            var game = Games.FirstOrDefault(g => g.GameCode == gameCode);

            if (game == null)
            {
                throw new GameNotFoundException();
            }

            return game;
        }

        private string GenerateGameCode()
        {
            Random random = new Random();
            int length = GAME_CODE_CHARS.Length;
            string gameCode = string.Empty;

            do
            {
                StringBuilder sb = new StringBuilder();

                sb.Append(GAME_CODE_CHARS[random.Next(0, length)]);
                sb.Append(GAME_CODE_CHARS[random.Next(0, length)]);
                sb.Append(GAME_CODE_CHARS[random.Next(0, length)]);
                sb.Append(GAME_CODE_CHARS[random.Next(0, length)]);

                gameCode = sb.ToString();
            }
            while (Games.Count(g => g.GameCode == gameCode) > 0);
            // In the future, could have seperate service with an available list of game codes that 
            // generates more on the fly when it gets low.

            return gameCode;
        }

        private string GenerateUpdateEndpoint(string baseBackendUrl, string gameCode)
        {
            Uri fullUri = new Uri(new Uri(baseBackendUrl, UriKind.Absolute), "/hub" + "/" + "code");
            return fullUri.ToString();
        }

    }
}
