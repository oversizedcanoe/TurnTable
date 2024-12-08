namespace TurnTableApplication.DTOs
{
    public class JoinedGameDTO
    {
        public int PlayerNumber { get; set; }

        public JoinedGameDTO(int playerNumber)
        {
            this.PlayerNumber = playerNumber;
        }
    }
}
