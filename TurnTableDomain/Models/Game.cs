using System.Text.Json;
using TurnTableBase;

namespace TurnTableDomain.Models
{
    public abstract class Game
    {
        public virtual GameType GameType { get; private set; }
        public virtual object GameState { get; private set; }

        public List<Player> Players { get; private set; }
        public DateTime StartedDateTime { get; private set; }
        public DateTime LastMoveDateTime { get; private set; }
        public int CurrentPlayerTurn { get; set; }
        public int? PlayerWinner { get; set; }
        public bool GameOver { get; set; } 

        public Game(Player playerOne)
        {
            Players = new List<Player> { playerOne };
            StartedDateTime = DateTime.UtcNow;
            LastMoveDateTime = DateTime.UtcNow;
            GameState = new object();
            CurrentPlayerTurn = 1;
            PlayerWinner = null;
            GameOver = false;
        }

        public Player AddPlayer(string playerName)
        {
            Player player = new Player(Players.Count + 1, playerName);

            Players.Add(player);

            return player;
        }

        public void UpdateLastActiveDate()
        {
            LastMoveDateTime = DateTime.UtcNow;
        }

        public virtual void NewMove(int playerNumber, object arg1, object arg2, object arg3)
        {
            throw new NotImplementedException();
        }

        public virtual void Restart()
        {
            throw new NotImplementedException();
        }

        public T DeserializeArg<T>(object jsonElement)
        {
            if (jsonElement is JsonElement element)
            {
                return element.Deserialize<T>();
            }
            else
            {
                return (T)jsonElement;
            }
        }
    }
}
