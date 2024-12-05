using System.Text;
using TurnTableBase;
using TurnTableDomain.Games;
using TurnTableDomain.Games.LinkFour;

namespace TurnTableDomain
{
    public class GameManager
    {
        public List<Game> Games { get; set; }

        private const string GAME_CODE_CHARS = "ABCDEFGHJKMNPQRSTUVWXYZ";

        public GameManager()
        {
            this.Games = new List<Game>();
        }

        public string StartNewGame(GameType gameType, string playerOneName)
        {
            Game game;

            string gameCode = this.GenerateGameCode();
            Player player = new Player(1, playerOneName);

            switch (gameType)
            {
                case GameType.LinkFour:
                    game = new LinkFour(gameCode, player);
                    break;
                default:
                    throw new Exception($"Unknown GameType passed: {gameType}");
            }

            this.Games.Add(game);

            return gameCode;
        }

        public bool Move(string gameCode, int playerNumber, object arg1, object arg2, object arg3)
        {
            var game = this.Games.FirstOrDefault(g => g.GameCode == gameCode);

            if (game == null)
            {
                throw new GameNotFoundException();
            }

            return game.NewMove(playerNumber, arg1, arg2, arg3);
        }

        public bool IsGameActive(string gameCode)
        {
            return this.Games.Any(g => g.GameCode == gameCode);
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
            while (this.Games.Count(g => g.GameCode == gameCode) > 0);
            // In the future, could have seperate service with an available list of game codes that 
            // generates more on the fly when it gets low.

            return gameCode;
        }
    }
}
