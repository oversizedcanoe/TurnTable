namespace TurnTableDomain
{
    public class Player
    {
        public int PlayerNumber { get; set; }
        public string Name { get; set; }

        public Player(int playerNumber, string name)
        {
            this.PlayerNumber = playerNumber;
            this.Name = name;
        }
    }
}
