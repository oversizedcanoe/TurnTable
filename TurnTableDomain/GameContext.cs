namespace TurnTableDomain
{
    public class GameContext
    {
        public string GameCode { get; set; }

        public List<Player> Players { get; set; }

        public GameContext(string gameCode, List<Player> players)
        {
            GameCode = gameCode;
            this.Players = players;
        }
    }
}
