using TurnTableBase;

namespace TurnTableDomain.Games
{
    public abstract class Game
    {
        public virtual GameType GameType { get; private set; }
        public List<Player> Players { get; private set; }
        public string GameCode { get; private set; }
        public DateTime StartedDateTime { get; private set; }
        public DateTime LastMoveDateTime { get; private set; }

        public Game(string gameCode, Player playerOne)
        {
            this.GameCode = gameCode;
            this.Players = new List<Player> { playerOne };
            this.StartedDateTime = DateTime.UtcNow;
            this.LastMoveDateTime = DateTime.UtcNow;
        }

        public void AddPlayer(Player player)
        {
            this.Players.Add(player);
        }

        public virtual MoveResultCode NewMove(int playerNumber, object arg1, object arg2, object arg3)
        {
            throw new NotImplementedException();
        }
    }
}
