namespace TurnTableDomain.Models
{
    public class Player
    {
        public int PlayerNumber { get; set; }
        public string Name { get; set; }

        public Player(int playerNumber, string name)
        {
            PlayerNumber = playerNumber;
            Name = name;
        }

        public static Player SinglePlayer => new Player(1, nameof(SinglePlayer));
    }
}
