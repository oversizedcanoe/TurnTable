using TurnTableBase;

namespace TurnTableDomain.Models
{
    public abstract class Game
    {
        public virtual GameType GameType { get; private set; }
        public List<Player> Players { get; private set; }
        public string GameCode { get; private set; }
        public DateTime StartedDateTime { get; private set; }
        public DateTime LastMoveDateTime { get; private set; }
        public string UpdateEndpoint { get; private set; }

        public Game(string gameCode, Player playerOne, string updateEndpoint)
        {
            GameCode = gameCode;
            Players = new List<Player> { playerOne };
            StartedDateTime = DateTime.UtcNow;
            LastMoveDateTime = DateTime.UtcNow;
            UpdateEndpoint = updateEndpoint;
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

        public virtual MoveResultCode NewMove(int playerNumber, object arg1, object arg2, object arg3)
        {
            throw new NotImplementedException();
        }
    }
}
